/*******************************************************************
* Název projektu: IVS-kalkulačka
* Balíček: CalculatorUnit
* Soubor: Calculation.cs
* Datum: 03.04.2017
* Autor: Petr Fusek
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 11.04.2017
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
 * @date 11.04.2017
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MathLib;

/**
 * @brief Modul výpočetní jednotky
 * @package CalculatorUnit
 *
 * Modul představuje mezivrstvu mezi uživatelským rozhraním a matematickou knihovnou.
 * Obsahuje prostředky pro vyhodnocování matematických výrazů, pomocí funkcí z matematické knihovny <see cref="MathLib.CalcMath"/>.
 * @author Petr Fusek
 * @author Pavel Vosyka
 */
namespace CalculatorUnit
{
  /// <summary>
  /// Objekt představující jeden výpočet.
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
    /// <summary>
    /// Konstanta nastavuje maximální počet desetinných míst pro výstup z metody GetAsString().
    /// </summary>
    private const int getAsStringDefaultPrecision = 4;

    #region Properties

    /// <summary>
    /// Výsledek výpočtu
    /// </summary>
    /// <description>
    /// Vrací výslednou hodnotu vyhodnoceného matematického výrazu v <see cref="Expresion">Expresion</see>.
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
        if (value == null)
        {
          t_calcError = CalcErrorType.UnknownError;
          d_value = double.NaN;
        }
        else
        {
          s_expression = PreprocessExpr(value);
          if (value == "")
            d_value = double.NaN;
          else
            d_value = EvaluateExpr(value);
        }
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
    /// Inicializace objektu
    /// </description>
    /// <param name="expr">
    ///   matematický výraz, který je nastaven do vlastnosti <see cref="Expression" />,
    ///   a jeho vyhodnocení je v <see cref="Value" />
    /// </param>
    public Calculation(string expr)
    {
      d_value = double.NaN;
      t_calcError = CalcErrorType.None;
      o_mathLib = new CalcMath();
      Expression = expr;
    }

    /// <summary>
    /// Vrací Value jako řetězec.
    /// </summary>
    /// <description>
    /// Metoda vrací výsledek výrazu Expression v podobě formátovaného řetězce v zadané číselné soustavě.
    /// </description>
    /// <param name="numbase">Číselná soustava ve které bude výsledek vypsán. Definuje se: <see cref="NumSystem"></param>
    ///
    /// <param name="format">Formátovací řetězec, nemá žádný efekt.</param>
    /// <returns>Výsledný řetězec</returns>
    public string GetAsString(NumSystem numbase, string format)
    {
      string result = NumberConverter.ToString(Value, (int)numbase, getAsStringDefaultPrecision);
      return result;
    }

    //privatni metody
    private string PreprocessExpr(string expr)
    {
      // tady mozna neignorovat mezery, protoze 55 55 55 se bude rovnat 555555 ...
      return "(" + expr.ToUpper().Replace(" ", "") + ")";
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
        if (Char.IsDigit(workStr[i])) // pokud je cislice
        {
          double valtmp = double.NaN;
          t_calcError = ProcessOperand(workStr, ref i, out valtmp);
          if (t_calcError == CalcErrorType.None)
            operandStack.Push(valtmp);
          else
            return valtmp;

          continue; // operand je zpracovan a index posunut na jeho konec ... opakujeme cyklus.
        }

        // Zpracovani operatoru
        if ("!L^@%*/-+".Contains(workStr[i]))
        {
          // zkontrolujeme, jestli se jedna o platny unarni operator ... pokud nebude vyhodnocen jako unarni, tak metoda vraci true
          if (!ProcessOperator(ref workStr, i))
          {
            t_calcError = CalcErrorType.ExprFormatError;
            return double.NaN;
          }
          // pokud je operator mensi priority nez posledni operator na zasobniku, provedem predchozi operaci
          while (
            operatorStack.Count > 0 &&
            GetOperatorPriority(operatorStack.Peek()) >= GetOperatorPriority(workStr[i])
          )
          {
            // Pokusime se provest operaci na zasobnicich
            t_calcError = PopOperation(operatorStack, operandStack);
            if (t_calcError != CalcErrorType.None)
              return operandStack.Pop(); // vysledek (inf/NaN) je na vrcholu
          }

          // ulozime aktualni operator
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
          if (operatorStack.Count < 1)
          {
            t_calcError = CalcErrorType.ExprFormatError;
            return double.NaN;
          }
          while (operatorStack.Peek() != '(')
          {
            // Pokusime se provest operaci na zasobnicich
            t_calcError = PopOperation(operatorStack, operandStack);
            if (t_calcError != CalcErrorType.None)
              return operandStack.Pop(); // vysledek (inf/NaN) je na vrcholu
          }
          operatorStack.Pop();
        }
        else // neznamy znak
        {
          t_calcError = CalcErrorType.ExprFormatError;
          return double.NaN;
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

    // Pokusi se prevedst operand (cislo) v poli expr na double,
    // pocita s tim, ze cislo zacina na indexu index, ktery zmeni na hodnotu kde cislo konci.
    // vysledne cislo ulozi do value
    // v pripade chyby index ukazuje, tam kde nastala
    // vrati CalcErrorType, ktery specifikuje chybu v operandu (none, format, oweflow, unknown)
    private CalcErrorType ProcessOperand(Char[] expr, ref int index, out double value)
    {
      value = double.NaN;
      string subString = "";
      while (Char.IsDigit(expr[index]) || ".E".Contains(expr[index]))
      {
        if (".E".Contains(expr[index]) && index == 0)
          return CalcErrorType.ExprFormatError;

        subString += expr[index];
        if (index + 1 >= expr.Length)
          break;

        //zvalidujeme specialni pripad zadavani v exponencialni podobe
        if (expr[index] == 'E')
        {
          if (!("-+".Contains(expr[index + 1])))
            return CalcErrorType.ExprFormatError;

          index++;
          subString += expr[index]; // jinak by "+-" nesplnilo podminku dalsiho pruchodu
        }
        else if (expr[index] == '.')
        {
          if (index >= expr.Length || !Char.IsDigit(expr[index + 1]))
            return CalcErrorType.ExprFormatError;
        }
        index++;
      }
      index--;

      // pokusime se retezec prevest na cislo
      double tmpval = double.NaN;
      try
      {
        tmpval = double.Parse(subString, CultureInfo.InvariantCulture);
      }
      catch (OverflowException)
      {
        return CalcErrorType.DataTypeOverflow;
      }
      catch (FormatException)
      {
        return CalcErrorType.ExprFormatError;
      }
      catch (ArgumentNullException)
      {
        return CalcErrorType.UnknownError;
      }

      value = tmpval;
      return CalcErrorType.None;
    }

    // Zvaliduje operator predany na indexu v poli expr
    // Vraci (true/false) jestli se jedna o valiodni umisteni unarniho operatoru
    // rozhodne pokud se jedná o speciální případ "-" (unarni negace) kde nahradi jeho znak v
    //   poli za "n" nebo "N" podle toho, jestli ma to vetsi nebo mensi prioritu.
    private bool ProcessOperator(ref Char[] expr, int index)
    {
      // zkontrolujeme, pokud je operator
      if (GetOperatorPriority(expr[index]) == 0)
        return false;

      // minus v nekterych pripadech znamena unarni negaci
      if (expr[index] == '-')
      {
        /*
        negace ma ruznou prioritu z ohledem na pozici ve vyrazu
        N - vyssi priorita pred @^%
        n - nissi priorita pred *-
        */
        // pokud je na zacatku retezce
        if (index == 0)
          expr[index] = 'n';
        else if (!Char.IsDigit(expr[index - 1]) && !"!)".Contains(expr[index - 1]))
        {
          // vyssi priorita pouze v pripadech kdy se jedna o zaporny druhy operand tj. 2^-2, 27@-3
          if ("@^%".Contains(expr[index - 1]))
            expr[index] = 'N';
          else
            expr[index] = 'n';
        }
      }
      else if (expr[index] == 'L')
      {
        if (index + 1 >= expr.Length)
          return false;
        if (!Char.IsDigit(expr[index + 1]) && !"-(".Contains(expr[index + 1]))
          return false;
      }
      else if (expr[index] == '!')
      {
        if (index == 0)
          return false;
        if (!Char.IsDigit(expr[index - 1]) && !"-)".Contains(expr[index - 1]))
          return false;
      }
      return true;
    }

    // Pokusi se provest operaci na vrcholu zasobiku
    // pokud nesedi pocty operandu nebo operatoru => chyba formatovani
    // pokud bude chyba ve vypoctu (NaN) => Domain error
    // pokud pretece double (INF) => Overflow
    // vysledek bude vzdy na vrcholu operandStack
    private CalcErrorType PopOperation(Stack<Char> operatorStack, Stack<double> operandStack)
    {
      if (operatorStack.Count == 0)
      {
        operandStack.Push(double.NaN);
        return CalcErrorType.ExprFormatError;
      }

      double tmpVal = double.NaN;

      //vyhodnoceni - pravý operand je nad levým v zásobníku
      if (IsUnary(operatorStack.Peek())) // pop jen jeden operand
      {
        if (operandStack.Count < 1)
        {
          operandStack.Push(double.NaN);
          return CalcErrorType.ExprFormatError;
        }
        tmpVal = eval(operandStack.Pop(), 0, operatorStack.Pop());
      }
      else
      {
        if (operandStack.Count < 2)
        {
          operandStack.Push(double.NaN);
          return CalcErrorType.ExprFormatError;
        }
        tmpVal = eval(operandStack.Pop(), operandStack.Pop(), operatorStack.Pop());
      }

      // osetreni chyb behem vypoctu
      operandStack.Push(tmpVal);
      if (double.IsNaN(tmpVal))
      {
        return CalcErrorType.FuncDomainError;
      }
      if (double.IsInfinity(tmpVal))
      {
        return CalcErrorType.DataTypeOverflow;
      }
      return CalcErrorType.None;
    }

    // vrati ciselnou hodnotu priority operatoru
    // 0 - neni operator
    // 1 a vic je operator, kde cim vetsi cislo tim vetsi priorita
    // lze pouzit i na zjisteni, jestli se jedna o operator
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
