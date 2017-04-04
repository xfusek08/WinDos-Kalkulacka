﻿/*******************************************************************
* Název projektu: Výpočetní jednotka určená pro vyhodnocování výrazů
* Balíček: CalculatorUnit
* Soubor: Calculation.cs
* Datum: 03.04.2017
* Autor: Petr Fusek
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 03.04.2017
*
* Popis: Třída, která zapouztřuje celý jeden matematický výpočet do jednoho objektu.
* Vyhodnocuje matematické výrazy pomocí vlastního zjednodušeného jazyka.
*
*****************************************************************/

using System;
using System.Collections.Generic;
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
        // TODO: logika spracovani
        s_expression = PreprocessExpr(value);
        
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
      s_expression = expr; 
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
      string workStr = PreprocessExpr(expr);
      if (workStr[0] == "(")
      {
      }
      for (int i = 0; i < workStr.Length; i++)
      {
        if (workStr[i] == '+')
        {
          return 
            EvaluateExpr(workStr.Substring(0, i)) + 
            EvaluateExpr(workStr.Substring(i, workStr.Length - 1));
        }
      }
      return double.NaN;
    }
  } // Calculation
}
