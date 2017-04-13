/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí matematické knihovny
* Balíček: ProjectTesting
* Soubor: MathLibTest.cs
* Datum: 06.04.2017
* Autor: Jaromír Franěk
* Naposledy upravil: Jaromír Franěk
* Datum poslední změny: 13.04.2017
*
* Popis: Matematická knihovna MathLib.
* Odpovědný za projekt je Jaromír Franěk.
*
*****************************************************************/

/**
 * @brief Matematická knihovna
 * @file CalcMath.cs
 * @author Jaromír Franěk
 * @date 13.04.2017
 */
using System;
using System.Globalization;

/**
  * @brief Matematická knihovna
  * @package MathLib
  *  
  * Matematická knihovna pro výpočet základních matematických operací
  * @author  Jaromír Franěk
  */
namespace MathLib
{
  /// <summary>Matematická knihovna</summary>
  /// <description>Třída obsahující metody základních matematických operací</description>
  public class CalcMath
  {
    /// <summary>Sčítání</summary>
    /// <description>Sečte argument a s argumentem b</description>
    /// <param name="a">Předá první čitatel typu double</param>
    /// <param name="b">Předá druhý čitatel typu double</param>
    /// <returns>Vrací součet argumentů</returns>
    public double Add(double a, double b)
    {
      return a + b;
    }

    /// <summary>Odčítání</summary>
    /// <description>Odečte argument a od argumentu b</description>
    /// <param name="a">Předá menšenec typu double</param>
    /// <param name="b">Předá menšitel typu double</param>
    /// <returns>Vrací rozdíl argumentů</returns>
    public double Subtract(double a, double b)
    {
      return a - b;
    }

    /// <summary>Dělení</summary>
    /// <description>Vydělí argument a argumentem b</description>
    /// <param name="a">Předá dělenec typu double</param>
    /// <param name="b">Předá dělitel typu double</param>
    /// <returns>Vrací podíl argumentů</returns>
    public double Divide(double a, double b)
    {
      if (b == 0)
      {
        return double.NaN;
      }
      return a / b;
    }

    /// <summary>Násobení</summary>
    /// <description>Vynásobí argument a argumentem b</description>
    /// <param name="a">Předá první činitel typu double</param>
    /// <param name="b">Předá druhý činitel typu double</param>
    /// <returns>Vrací násobek argumentů</returns>
    public double Multipy(double a, double b)
    {
      return a * b;
    }

    /// <summary>Mocnina</summary>
    /// <description>Obecná y-tá mocnina z "x"</description>
    /// <param name="x">Předá základ typu double</param>
    /// <param name="y">Předá exponent typu double</param>
    /// <returns>Vrací y-tou mocninu argumentu x</returns>
    public double Pow(double x, double y)
    {
      if ((x < 0) && y > 0 && y < 1)
        return -Math.Pow(-x, y);
      else if ((double.IsNegativeInfinity(x) && double.IsInfinity(y)) || (double.IsNegativeInfinity(y) && double.IsInfinity(x)))
        return double.NaN;
      else if (x == double.MinValue || y == double.MinValue)
        return double.NaN;
      else if (x == 0 && y < 0)
        return double.NaN;
      else if ((x < 0) && y < 0)
      {
        string str = y.ToString();
        str = str.Substring(str.Length - 1, 1);
        int OddCheck = int.Parse(str);
        if (OddCheck % 2 != 0)
          return double.NaN;
        else
        {
          if ((x < 0) && y < 0 && y < -1)
            return Math.Pow(-x, y);
          if ((x < 0) && y < 0 && y > -1)
            return -Math.Pow(-x, y);
        }
      }
      return Math.Pow(x, y);
    }

    /// <summary>Odmocnina</summary>
    /// <description>Obecná x-tá odmocnina z "a"</description>
    /// <param name="a">Předá základ typu double</param>
    /// <param name="x">Předá exponent typu double</param>
    /// <returns>Vrací x-tou odmocninu argumentu a</returns>
    public double Root(double a, double x)
    {
      if ((a < 0) && (x % 2) == 1)
        return -Math.Pow(-a, 1.0 / x);
      if (x <= 0)
        return double.NaN;
      return Math.Pow(a, 1.0 / x);
    }

    /// <summary>Factorial</summary>
    /// <description>Factorial z čísla x, o maximální hodnotě 170</description>
    /// <param name="b">Předá hodnotu factorialu typu byte</param>
    /// <returns>Pokud je x < 170, vrací factorial z čísla x, pokud je x > 170, vrací PositiveInfinity.</returns>
    public double Fact(Byte x)
    {
      if (x <= 1)
      {
        return 1;
      }
      else if (x > 170)
      {
        return double.PositiveInfinity;
      }
      double value = 1;
      for (byte i = 1; i <= x; i++)
      {
        value = i * value;
      }
      CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
      string DotString = value.ToString("F", culture);
      return Double.Parse(DotString, CultureInfo.InvariantCulture);
    }

    /// <summary>Zbytek</summary>
    /// <description>Zbytek po dělení argumentu a argumentem b</description>
    /// <param name="a">Předá dělenec typu double</param>
    /// <param name="b">Předá dělitel typu double</param>
    /// <returns>Vrací zbytek po podílu čísla a číslem b</returns>
    public double Modulo(double a, double b)
    {
      double value = a % b;
      CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
      string DotString = value.ToString("F", culture);
      return Double.Parse(DotString, CultureInfo.InvariantCulture);
    }

    /// <summary>Logaritmus</summary>
    /// <description>Logarmitmus při základu 10 argumentu a </description>
    /// <param name="a">Předá hodnotu typu double, ze které bude logaritmus vypočítán</param>
    /// <returns>Vrací výpočet logaritmu z čísla a o základu 10</returns>
    public double Log(double x)
    {
      if (x == 0)
      {
        return double.NaN;
      }
      return Math.Log(x, 10);
    }
  }
}
