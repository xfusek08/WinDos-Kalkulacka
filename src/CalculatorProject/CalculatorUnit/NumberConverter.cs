/*******************************************************************
* Název projektu: IVS-kalkulačka
* Balíček: CalculatorUnit
* Soubor: NumberConverter.cs
* Datum: 07.04.2017
* Autor: Pavel Vosyka
* Naposledy upravil: Pavel Vosyka
* Datum poslední změny: 17.04.2017
*
* Popis: Pomocná statická třída pro konvertování čísla na řetězec.
*
*****************************************************************/

/**
 * @brief Pomocná statická třída pro konvertování čísla na řetězec.
 * @file NumberConverter.cs
 * @author Petr Fusek
 * @author Pavel Vosyka
 * @date 13.04.2017
 */

using System;
using System.Globalization;

namespace CalculatorUnit
{
  /// <summary>
  /// Třída poskytující metody pro konverzi čísel na řetezce a naopak. Podporuje také převody mezi soustavami o základu 2, 8, 10, 16.
  /// </summary>
  public static class NumberConverter
  {

    private const int c_stringDefaultPrecision = 4;

    /// <summary>
    /// Konvertuje řetězec na číslo s pohyblivou desetinnou čárkou.
    /// </summary>
    /// <param name="input">Řetězec obsahující číslo ve specifikované číselné soustavě</param>
    /// <param name="numsystem">Číselná soustava ve které je řetězec zapsán <see cref="NumSystem"></param>
    /// 
    /// <returns>Vrací převedené číslo. Pokud je řetězec roven "NaN" vrací NaN, pokud je "INF" nebo "-INF" vrací double.PositiveInfinity nebo double.NegativeInfinity.</returns>
    /// 
    /// <exception cref="ArgumentException">Vyjímka je vyhozena, pokud vstupní řetězec obsahuje nepovolené znaky v dané soustavě. Metoda přijímá desetinnou tečku ".", nikoliv čárku ",".</exception>
    public static double ToDouble(string input, NumSystem numsystem)
    {
      if (input == "NaN")
        return double.NaN;
      if (input == "INF")
        return double.PositiveInfinity;
      if (input == "-INF")
        return double.NegativeInfinity;

      double result = 0;
      int charVal;
      int dotPos = input.IndexOf('.');
      int power;
      bool negativFlag = false;
      if (dotPos != -1)
      {
        power = dotPos - 1;
      }
      else
      {
        power = input.Length-1;
      }
      if(input[0] == '-')//pokud je cislo zaporne je nastaven flag a power aby se spravne umocnovalo
      {
        negativFlag = true;
        power--;
      }
      for (int i = negativFlag ? 1 : 0; i < input.Length; i++)
      {
        if (i == dotPos)
          continue;
        if (!digitToInt(input[i], out charVal, (int)numsystem))
        {
          throw new ArgumentException("Argument neobsahuje validní znaky", "input");
        }
        result += charVal * Math.Pow((int)numsystem, power);
        power--;
      }
      if (negativFlag)//pokud byl flag nastaven vraci se vysledek zaporny
        return -result;
      return result;
    }

    /// <summary>
    /// Konvertuje číslo na řetězec.
    /// </summary>
    /// <description>
    /// Konvertuje číslo na řetězec v zadané číselné soustavě na počet zadaných desetinných míst. 
    /// Převod o jiných soustav než desítkových je limitován velikostí integer, v takovém případě vrací "NaN".
    /// </description>
    /// <param name="value">Hodnota, která ma být prevedena na číslo</param>
    /// <param name="numbase">Základ soustavy ve které se vypíše viz. <see cref="NumSystem"></param>
    /// 
    /// <param name="format">
    ///   Formátovací řetězec
    ///   <list>
    ///     <item>Výchozí nastavení formátu je na 4 desetinná místa.</item>
    ///     <item>
    ///       pokud: numbase je NumSystem.Dec (desítková soustava)
    ///       * Očekává formátovací řetězec dle standardu <a href="https://msdn.microsoft.com/en-us//library/dwhawy9k(v=vs.110).aspx">.net</a>
    ///       * Pokud je prázdný řetězec použije se výchozí nastavení.
    ///     </item>
    ///     <item>
    ///       jinak: numbase je jiná soustava
    ///       * Očekává číslo, které představuje počet desetinných míst.
    ///       * Pokud nebude validní, použije se výchozí nastavení.
    ///     </item>
    ///   </list>
    /// </param>
    /// 
    /// <returns>
    ///     číslo v podobě řetězce v požadované číselné soustavě.
    ///     * pokud je <c>value > 'int max'</c> a numbase není NumSystem.Dec potom rací "NaN".
    /// </returns>
    public static string ToString(double value, NumSystem numbase, string format)
    {
      if (double.IsNaN(value))
        return "NaN";

      if (double.IsInfinity(value) || double.IsPositiveInfinity(value))
        return "INF";

      if (double.IsNegativeInfinity(value))
        return "-INF";

      NumberFormatInfo f = new NumberFormatInfo();
      f.NumberGroupSeparator = "";
      if ((int)numbase == 10) //v případě, že je soustava desítková, lze vypsat hodnotu přes standartní metodu
      {
        if (format != "")
          return value.ToString(format, f);
        else
          return value.ToString(f);
      }

      if (value > int.MaxValue || value < int.MinValue)
        return "NaN";

      string resultstr = "";
      if (value < 0) //pokud je číslo záporné, vloží se mínus a zbytek se převede jako kladné
      {
        value = Math.Abs(value);
        resultstr = "-";
      }
      //převáděné číslo se rozloží na celou část a desetinnou
      double intvalue = extractInt(value); //celá část
      double fractionalDigits = value - intvalue; //desetinná část
      int digit; //proměnná pro mezivýsledky, vždy jeden znak
      resultstr += Convert.ToString((int)intvalue, (int)numbase); //celá část čísla se převede pomocí standartní metody
      if (fractionalDigits == 0)
      {
        return resultstr;
      }
      resultstr += "."; //vložení desetinné tečky
      for (int i = 0; i < c_stringDefaultPrecision; i++) //převod desetinné části s požadovanou přesností
      {
        fractionalDigits *= (int)numbase;
        if (fractionalDigits == 0) //pokud je zbytek nulový, není důvod pokračovat
          break;
        digit = (int)extractInt(fractionalDigits);
        resultstr += PrintDigit(digit);
        fractionalDigits -= digit;
      }
      return postprocessStr(resultstr);
    }

    /// <summary>
    /// Odstraní nuly na konci v desetinných číslech, případně desetinnou tečku.
    /// <param name="input">desetinné číslo</param>
    /// </summary>
    /// <returns></returns>
    private static string postprocessStr(string input)
    {
      int end = input.Length - 1;
      for (; end > 0; end--)
      {
        if (input[end] != '0')
        {
          break;
        }
      }
      if (input[end] == '.')
        end--;
      return input.Remove(end + 1, input.Length - end - 1);
    }

    private static bool digitToInt(char digit, out int value, int numberSystem)
    {
      if (int.TryParse(digit.ToString(), out value))
      {
        if (value >= numberSystem)
        {
          return false;
        }
        return true;
      }
      switch (digit)
      {
        case 'a':
        case 'A':
          value = 10;
          break;
        case 'b':
        case 'B':
          value = 11;
          break;
        case 'c':
        case 'C':
          value = 12;
          break;
        case 'd':
        case 'D':
          value = 13;
          break;
        case 'e':
        case 'E':
          value = 14;
          break;
        case 'f':
        case 'F':
          value = 15;
          break;
        default:
          return false;
      }
      if (value >= numberSystem)
      {
        return false;
      }
      else
      {
        return true;
      }

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
      }
      else
      {
        return Math.Ceiling(value);
      }
    }
  }
}
