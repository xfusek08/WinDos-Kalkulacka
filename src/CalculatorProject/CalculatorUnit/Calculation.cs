/*******************************************************************
* Název projektu: Výpočetní jednotka určená pro vyhodnocování výrazů
* Balíček: CalculatorUnit
* Soubor: Calculation.cs
* Datum: 03.04.2017
* Autor: Petr Fusek
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 04.04.2017
*
* Popis: Třída, která zapouztřuje celý jeden matematický výpočet do jednoho objektu.
* Vyhodnocuje matematické výrazy pomocí vlastního zjednodušeného jazyka.
*
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalculatorUnit
{
  /// <summary>
  /// Druhy chyb, které mohou nastat během výpočtu.
  /// </summary>
  public enum CalcErrorType
  {
    /// <summary>Žádná chyba</summary>
    /// <description>Výpočet proběhl v pořádku.</description>
    None,
    /// <summary>Chyba definičního oboru fukce</summary>
    /// <description>Ve výrazu jsou hodnoty operátorů, které nemají definovanou hodnotu v konkrétní funkci.</description>
    FuncDomainError,
    /// <summary>Během výpočtu došlo k přetečení.</summary>
    /// <description>V případě, že se během výpočtů dostaneme za hranice rozsahu typu <b>double</b>.</description>
    DataTypeOwerflow,
    /// <summary>Chyba formátování výrazu</summary>
    /// <description>
    /// Chyba pokud vyhodnocovaný řetězec není platný matematický výraz.<br />
    /// např.:
    /// <list>
    ///   <item>Neukončené závorky</item>
    ///   <item>Operátory bez operandů</item>
    ///   <item>neznámé znaky</item>
    ///   <item>...</item>
    /// </list>
    /// </description>
    ExprFormatError,
    /// <summary>Ostatní neznámé chyby</summary>
    UnknownError
  }

  /// <summary>
  /// Typ číselné soustavy.
  /// </summary>
  public enum NumSystem
  {
    /// <summary>Desítková soustava</summary>
    Dec,
    /// <summary>Binární soustava</summary>
    Bin,
    /// <summary>Šestnáctková soustava</summary>
    Hex,
    /// <summary>Osmičková soustava</summary>
    Oct
  }

  /// <summary>
  /// Třída objektů představující jeden výpočet.
  /// </summary>
  public class Calculation
  {
    // privatni atributy
    private double d_value;
    private string s_expression;
    private CalcErrorType t_calcError;
    private string s_errMsg;
    private string s_errSubExpr;

    #region Properties

    /// <summary>
    /// Výsledek výpočtu
    /// </summary>
    /// <description>
    /// Vrací výslednou hodnotu vehodnoceného matematického výrazu v <see cref="Expresion">Expresion</see>.
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se mění pouze v případě změny výrazu a to bezprostředně.</item>
    ///   <item>
    ///     Hodnota <b>NaN</b> znamená chybu během výpočtu a její specifikace jsou ve vlastnostech:
    ///     <list>
    ///       <item><see cref="ErrorType"/> - typ chyby</item>
    ///       <item><see cref="ErrorSubExpr"/> - část výrazu, ve kterém nastala chyba</item>
    ///       <item><see cref="ErrorMessage"/> - stručný popis chyby pro uivatele</item>
    ///     </list>
    ///   </item>
    /// </list>
    /// </description>
    public double Value
    {
      get { return d_value; }
    }

    /// <summary>
    /// Matematický výraz
    /// </summary>
    /// <description>
    /// Atribut objektu udržující řetězec odpovídající matematickému výrazu, který třída vyhodnocuje.
    ///
    /// Je napsaný v běžném zápisu orzšířeném o operace z matematické knihovny.
    /// \code ((7-3)!/(5+1))-((30-3)@(6-3)*-1) \endcode
    /// příklad vis. <a href="https://github.com/xfusek08/IVS-Kalkulacka/wiki/Architektonick%C3%BD-n%C3%A1vrh#form%C3%A1t-matematick%C3%A9ho-%C5%99et%C4%9Bzce">projektová wiki</a>
    ///
    /// <b>Chování:</b>
    /// <list>
    ///   <item><b>get</b> - vrátí text výrazu</item>
    ///   <item>
    ///     <b>set</b>
    ///     <list>
    ///       <item>všechny bílé znaky jsou vymazány</item>
    ///       <item>provede vyhodnocení a aktualizuje hodnoty vlastností:
    ///         <list>
    ///           <item><see cref="Value"/></item>
    ///           <item><see cref="ErrorType"/></item>
    ///           <item><see cref="ErrorSubExpr"/></item>
    ///           <item><see cref="ErrorMessage"/></item>
    ///         </list>
    ///       </item>
    ///     </list>
    ///   </item>
    /// </list>
    /// </description>
    public string Expression
    {
      get { return s_expression; }
      set
      {
        s_expression = PreprocessExpr(value);
        if (value == "")
          d_value = double.NaN;
        else
          d_value = EvaluateExpr(value);
      }
    }

    /// <summary>
    /// Typ chyby, která nastala během výpočtu
    /// </summary>
    /// <description>
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se aktualizuje při každém výpočtu.</item>
    /// </list>
    /// </description>
    public CalcErrorType ErrorType
    {
      get { return t_calcError; }
    }

    /// <summary>
    /// Stručný popis chyby pro uživatele
    /// </summary>
    /// <description>
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se mění pouze v případě chyby.</item>
    /// </list>
    /// </description>
    public string ErrorMessage
    {
      get { return s_errMsg; }
    }

    /// <summary>
    /// Stručný popis chyby pro uživatele
    /// </summary>
    /// <description>
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se mění pouze v případě chyby.</item>
    /// </list>
    /// </description>
    public string ErrorSubExpr
    {
      get { return s_errSubExpr; }
    }

    #endregion

    // verejne metody
    /// <summary>
    /// Konstruktor objektu
    /// </summary>
    /// <description>
    /// Bere jako parametr výraz, který nastavý do vlastnosti <see cref="Expresion">
    /// </description>
    public Calculation(string expr)
    {
      d_value = double.NaN;
      t_calcError = CalcErrorType.None;
      s_errMsg = "";
      s_errSubExpr = "";
      Expression = expr;
    }

    public string GetAsString(NumSystem numbase, string format)
    {
      return "";
    }

    //privatni metody
    private string PreprocessExpr(string expr)
    {
      return expr.Replace(" ", "");
    }

    private double EvaluateExpr(string expr)
    {
      string workStr = PreprocessExpr("(" + expr + ")"); // zavorkujeme aby se spravne zpetne vyhodnotil cely vyraz

      // zasobniky
      Stack<double> operandStack = new Stack<double>();
      Stack<char> operatorStack = new Stack<char>();

      // zpracovavame vyraz po znacich
      for (int i = 0; i < workStr.Length; i++)
      {

        // Zpracovani operandu
        if (Char.IsDigit(workStr[i]) ||                                   // pokud je cislice
           (workStr[i] == '-' && i == 0) ||                               // nebo '-' a je to prvni znak v retezci
           (i > 0 && workStr[i] == '-' && !Char.IsDigit(workStr[i - 1]))) // nebo '-' a predchozi znak neni cislice (operand)
        {
          string subString = workStr[i].ToString();
          double valtmp = double.NaN;
          i++;
          while (Char.IsDigit(workStr[i]) || workStr[i] == '.')
          {
            subString += workStr[i];
            i++;
            if (i >= workStr.Length)
              break;
          }
          i--;
          if (!double.TryParse(subString, NumberStyles.Any, CultureInfo.InvariantCulture, out valtmp))
          {
            // neni platny double
            // TODO: specifikace chyby nebo exeption ... 
            return double.NaN;
          }
          else
            operandStack.Push(valtmp);

        continue;
        } // Zpracovani operandu

        // zpracovavame operator
        if ("!L^@%*/-+".Contains(workStr[i]))
        {
          // pokud je operator mensi priority nez posledni operator na zasobniku, provedem predchozi operaci
          while (
            operatorStack.Count > 0 &&
            GetOperatorPriority(operatorStack.Peek()) >= GetOperatorPriority(workStr[i])
          )
          {
            operandStack.Push(eval(operandStack.Pop(),
              operandStack.Pop(), operatorStack.Pop()));
          }
          // ulozime aktualni operand
          operatorStack.Push(workStr[i]);
          continue;
        }
        // zavorky oteviraci se ukladaji pro skonceni vyhodnoceni podvyrazu
        else if (workStr[i] == '(')
        {
          operatorStack.Push(workStr[i]);
        }
        // uzaviraci zavorka znamena vyhodnot vsechno zpetne az do oteviraci zavorky
        else if (workStr[i] == ')')
        {
          while (operatorStack.Peek() != '(')
          {
            //vyhodnoceni - pravý operand je nad levým v zásobníku
            operandStack.Push(eval(operandStack.Pop(),
              operandStack.Pop(), operatorStack.Pop()));
          }
          operatorStack.Pop();
        }
      }                                                                             
      // TODO: zkontrolovat chyby

      // na zasobniku by mela zustat jenom jedna hodnota
      return operandStack.Pop();//double.NaN;
    }

    private int GetOperatorPriority(char oper)
    {
      switch (oper)
      {
        case '!':
          return 9;
        case 'L':
          return 8;
        case '^':
          return 7;
        case '@':
          return 6;
        case '%':
          return 5;
        case '*':
          return 4;
        case '/':
          return 3;
        case '-':
          return 2;
        case '+':
          return 1;
        default:
          return 0;
      }
    }

    private bool isUnary(char op)
    {
      switch (op)
      {
        case '!':
        case 'L':
          return true;
        case '/':
        case '-':
        case '+':
        case '^':
        case '@':
        case '%':
        case '*':
          return false;
        default:
          throw new Exception();
      }
    }

    private double eval(double rightOp, double leftOp, char operand)
    {
      switch (operand)
      {
        case '+':
          return leftOp + rightOp;
        case '-':
          return leftOp - rightOp;
        case '*':
          return leftOp * rightOp;
        case '/':
          return leftOp / rightOp;
        default:
          return double.NaN;
      }
    }
  } // Calculation
}
