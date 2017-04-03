/*******************************************************************
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
  /// Třída objektů představující jeden výpočet.
  /// </summary>
  public class Calculation
  {
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
    public double Value { get; }

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
    public string Expresion { get; set; }

    /// <summary>
    /// Typ chyby, která nastala během výpočtu
    /// </summary>
    /// <description>
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se aktualizuje při každém výpočtu.</item>
    /// </list>
    /// </description>
    public CalcErrorType ErrorType { get; }

    /// <summary>
    /// Stručný popis chyby pro uživatele
    /// </summary>
    /// <description>
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se mění pouze v případě chyby.</item>
    /// </list>
    /// </description>
    public string ErrorMessage { get; }
    /// <summary>
    /// Stručný popis chyby pro uživatele
    /// </summary>
    /// <description>
    /// <list>
    ///   <item>Read-only vlastnost.</item>
    ///   <item>Hodnota se mění pouze v případě chyby.</item>
    /// </list>
    /// </description>
    public string ErrorSubExpr { get; }

    public Calculation(string expr)
    {
    }

    public string GetAsString(NumSystem numbase, string format)
    {
      return "";
    }
  }

  public enum CalcErrorType
  {
    None, FuncDomainError, DataTypeOwerflow, ExprFormatError, UnknownError
  }

  public enum NumSystem
  {
    Dec, Bin, Hex, Oct
  }
}
