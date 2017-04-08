/*******************************************************************
* Název projektu: Výpočetní jednotka určená pro vyhodnocování výrazů
* Balíček: CalculatorUnit
* Soubor: NumberConverter.cs
* Datum: 07.04.2017
* Autor: Pavel Vosyka
* Naposledy upravil: Pavel Vosyka
* Datum poslední změny: 08.04.2017
*
* Popis: Pomocná statická třída pro konvertování čísla na řetězec.
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
  /// Třída poskytující metody pro konverzi čísel na řetezece
  /// </summary>
  static class NumberConverter
  {
    /// <summary>
    /// Konvertuje číslo na řetězec.
    /// </summary>
    /// <description>
    /// Konvertuje číslo na řetězec v zadané číselné soustavě na počet zadaných desetinných míst.
    /// Známé chyby:
    ///   - Při konverzi do jiné soustavy než desítkové vždy vypisuje desetinnou tečku. 
    ///   - Pokud je desetinná část čísla příliš malá, vypisuje nuly.
    ///     Například: 10.000004 ==> 10.0000
    /// </description>
    /// <param name="value">číslo pro konvertování</param>
    /// <param name="numbase">Základ soustavy. Muže být pouze 2, 8, 10 nebo 16</param>
    /// <param name="precision">Maximální počet desetinných míst na která se číslo výpíše.</param>
    /// <returns>číslo v podobě řetězce</returns>
    public static string ToString(double value, int numbase, int precision)
    {
      if (numbase == 10)
      {
        value = Math.Round(value, precision);
        return value.ToString(new System.Globalization.CultureInfo("en-US"));
      }
      string resultstr = "";
      if (value < 0)
      {
        value = Math.Abs(value);
        resultstr = "-";
      }
      double intvalue = extractInt(value);
      double fractionalDigits = value - intvalue;
      int digit;
      resultstr += Convert.ToString((int)intvalue, numbase);
      resultstr += ".";
      for (int i = 0; i < precision; i++)
      {
        fractionalDigits *= numbase;
        if (fractionalDigits == 0)
          break;
        digit = (int)extractInt(fractionalDigits);
        resultstr += PrintDigit(digit);
        fractionalDigits -= digit;
      }
      return resultstr;
    }

    /// <summary>
    /// Vrátí číslo jako hexadecimální znak.
    /// </summary>
    /// <param name="digit">číslo od 0 do 15</param>
    /// <returns></returns>
    private static string PrintDigit(int digit)
    {
      if (digit < 10)
        return digit.ToString();
      switch (digit)
      {
        case 10:
          return "a";
        case 11:
          return "b";
        case 12:
          return "c";
        case 13:
          return "d";
        case 14:
          return "e";
        case 15:
          return "f";
        default:
          return "";
      }
    }

    /// <summary>
    /// Vrátí pouze celou část desetinného čísla.
    /// </summary>
    /// <param name="value">Zadané číslo</param>
    /// <returns>Číslo zaokrouhlené dolů.</returns>
    private static double extractInt(double value)
    {
      if (value >= 0)
      {
        return Math.Floor(value);
      }else{
        return Math.Ceiling(value);
      }
    }
  }
}
