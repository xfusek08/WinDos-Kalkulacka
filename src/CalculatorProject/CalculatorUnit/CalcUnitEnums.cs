/*******************************************************************
* Název projektu: IVS-kalkulačka
* Balíček: CalculatorUnit
* Soubor: CalcUnitEnums.cs
* Datum: 03.04.2017
* Autor: Petr Fusek
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 11.04.2017
*
* Popis: Soubor združující výčtové typy používané v Namespace CalculatorUnit
*
*****************************************************************/

/**
 * @brief Soubor združující výčtové typy používané v Namespace CalculatorUnit
 * @file CalcUnitEnums.cs
 * @author Petr Fusek
 * @author Pavel Vosyka
 * @date 11.04.2017
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
    DataTypeOverflow,
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
    Dec = 10,
    /// <summary>Binární soustava</summary>
    Bin = 2,
    /// <summary>Šestnáctková soustava</summary>
    Hex = 16,
    /// <summary>Osmičková soustava</summary>
    Oct = 8
  }
}
