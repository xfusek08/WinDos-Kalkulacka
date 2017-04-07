/*******************************************************************
* Název projektu: Výpočetní jednotka určená pro vyhodnocování výrazů
* Balíček: CalculatorUnit
* Soubor: Calculation.cs
* Datum: 03.04.2017
* Autor: Petr Fusek, Pavel Vosyka
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 07.04.2017
*
* Popis: Třída, která zapouztřuje celý jeden matematický výpočet do jednoho objektu.
* Vyhodnocuje matematické výrazy pomocí vlastního zjednodušeného jazyka.
*
*****************************************************************/

/**
 * @brief Modul výpočetní jednotky
 * @file Calculation.cs
 * @author Petr Fusek
 * @author Pavel Vosyka
 * @date 07.04.2017
 * @version 0.0
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MathLib;

/**
 * @brief Modul výpočetní jednotky
 * @package CalculatorUnit
 * Modul poskytuje třídu Calculation, která představuje zapouzdření výpočtu matematického výrazu.
 * Matematické výrazy jsou předávány ve vlastním zjednodušeném výrazovém jazyce, který zahrnuje jen nutné vlastnosti Kalkulačky.
 * @author Petr Fusek
 * @author Pavel Vosyka
 */
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
  /// <description>
  /// <b>Výrazy</b>
  ///
  /// Výrazy jsou klasické řetězce znaků, představující matematický zápis. rozšířený o
  /// dodatečné operátory, představující další funkce kalkulačky. <br/>
  /// Příklad složitějšího výrazu:
  /// \code
  /// (-(7-3)!/(5+1))-((30-(L(200-50*2)+1))@(6-3)*-1)
  /// \endcode
  /// Všechny mezery ve výrazu jsou ignorovány.
  ///
  /// <b>Podporováné operátory</b>
  /// | Operátor | Výraz | Význam | Odpovídající funkce z CalcMath|
  /// | :---: | :---: | --- | --- |
  /// | ``!`` | ``x!`` | faktorial x |Fact()|
  /// | ``L`` | ``Lx`` | logaritmus z x při základu 10 |Log()|
  /// | ``^`` | ``x^y``| x umocněné na y |Pow()|
  /// | ``@`` | ``x@y``| y-tá odmocnina z x |Root()|
  /// | ``%`` | ``x%y``| x modulo y |Modulo()|
  /// | ``*`` | ``x*y``| x krát y |Multipy()|
  /// | ``/`` | ``x/y``| x děleno y |Divide()|
  /// | ``-`` | ``x-y``| x mínus y |Subtract()|
  /// | ``+`` | ``x+y``| x plus y |Add()|
  ///
  /// viz. <see cref="MathLib.CalcMath"/>
  /// </description>
  public class Calculation
  {
    // privatni atributy
    private double d_value;
    private string s_expression;
    private CalcErrorType t_calcError;
    private CalcMath o_mathLib;

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
    ///     Hodnota <b>NaN</b> znamená chybu během výpočtu a její typ je uložen v <see cref="ErrorType"/>
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
      o_mathLib = new CalcMath();
      Expression = expr;
    }

    public string GetAsString(NumSystem numbase, string format)
    {
      return "";
    }

    //privatni metody
    private string PreprocessExpr(string expr)
    {
      return "(" + expr.Replace(" ", "") + ")";
    }

    private double EvaluateExpr(string expr)
    {
      t_calcError = CalcErrorType.None;
      Char[] workStr = PreprocessExpr(expr).ToArray(); // zavorkujeme aby se spravne zpetne vyhodnotil cely vyraz

      // zasobniky
      Stack<double> operandStack = new Stack<double>();
      Stack<char> operatorStack = new Stack<char>();

      // zpracovavame vyraz po znacich
      for (int i = 0; i < workStr.Length; i++)
      {

        // Zpracovani operandu
        if (
          Char.IsDigit(workStr[i]) //||                                    // pokud je cislice
          //(workStr[i] == '-' && i == 0) ||                               // nebo '-' a je to prvni znak v retezci
          //(i > 0 && workStr[i] == '-' && !Char.IsDigit(workStr[i - 1]))  // nebo '-' a predchozi znak neni cislice (operand)
        )
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
          // minus v nekterych pripadech znamena unarni negaci
          if (workStr[i] == '-')
          {
            /*
            negace ma ruznou prioritu z ohledem na pozici ve vyrazu
            N - vyssi priorita pred @^%
            n nissi priorita pred *-
            */
            // pokud je na zacatku retezce
            if (i == 0)
              workStr[i] = 'n';
            else if (!Char.IsDigit(workStr[i - 1]) && !"!)".Contains(workStr[i - 1]))
            {
              // vyssi priorita pouze v pripadech kdy se jedna o zaporny druhy operand tj. 2^-2, 27@-3
              if ("@^%".Contains(workStr[i - 1]))
                workStr[i] = 'N';
              else
                workStr[i] = 'n';
            }
          }
          // pokud je operator mensi priority nez posledni operator na zasobniku, provedem predchozi operaci
          while (
            operatorStack.Count > 0 &&
            GetOperatorPriority(operatorStack.Peek()) >= GetOperatorPriority(workStr[i])
          )
          {
            // Pokusime se provest operaci na zasobnicich
            if (!PopOperation(operatorStack, operandStack))
              return double.NaN;
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
            // Pokusime se provest operaci na zasobnicich
            if (!PopOperation(operatorStack, operandStack))
              return double.NaN;
          }
          operatorStack.Pop();
        }
      }
      // na zasobniku by mela zustat jenom jedna hodnota
      if (operandStack.Count != 1)
      {
        t_calcError = CalcErrorType.ExprFormatError;
        return double.NaN;
      }
      return operandStack.Pop();//double.NaN;
    }

    // Pokusi se provest operaci na vrcholu zasobiku
    // pokud nesedi pocty operandu nebo operatoru => chyba formatovani
    // pokud bude chyba ve vypoctu => Domain error
    private bool PopOperation(Stack<Char> operatorStack, Stack<double> operandStack)
    {
      if (operatorStack.Count == 0)
        return false;

      double tmpVal = double.NaN;

      //vyhodnoceni - pravý operand je nad levým v zásobníku
      if (IsUnary(operatorStack.Peek())) // pop je jeden operand
      {
        if (operandStack.Count < 1)
        {
          t_calcError = CalcErrorType.ExprFormatError;
          return false;
        }
        tmpVal = eval(operandStack.Pop(), 0, operatorStack.Pop());
      }
      else
      {
        if (operandStack.Count < 2)
        {
          t_calcError = CalcErrorType.ExprFormatError;
          return false;
        }
        tmpVal = eval(operandStack.Pop(), operandStack.Pop(), operatorStack.Pop());
      }

      // osetreni chyb behem vypoctu
      if (double.IsNaN(tmpVal))
      {
        t_calcError = CalcErrorType.FuncDomainError;
        return false;
      }
      operandStack.Push(tmpVal);
      return true;
    }

    private int GetOperatorPriority(char oper)
    {
      switch (oper)
      {
        case '!':
          return 11;
        case 'L':
          return 10;
        case 'N':
          return 9;
        case '^':
          return 8;
        case '@':
          return 7;
        case '%':
          return 6;
        case 'n':
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

    private bool IsUnary(char op)
    {
      switch (op)
      {
        case '!':
        case 'L':
        case 'n':
        case 'N':
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
          return o_mathLib.Add(leftOp, rightOp);
        case '-':
          return o_mathLib.Subtract(leftOp, rightOp);
        case '*':
          return o_mathLib.Multipy(leftOp, rightOp);
        case '/':
          return o_mathLib.Divide(leftOp, rightOp);
        case '^':
          return o_mathLib.Pow(leftOp, rightOp);
        case '@':
          return o_mathLib.Root(leftOp, rightOp);
        case '%':
          return o_mathLib.Modulo(leftOp, rightOp);
        case 'L':
          return o_mathLib.Log(rightOp);
        case 'n':
        case 'N':
          return o_mathLib.Multipy(-1, rightOp);
        case '!':
          byte byteVal = 0;
          try { byteVal = Convert.ToByte(rightOp); }
          catch
          {
            // TODO: Chyba
            return double.NaN;
          }
          return o_mathLib.Fact(byteVal);
        default:
          return double.NaN;
      }
    }
  } // Calculation
}
