/*******************************************************************
* Název projektu: IVS-Kalkulačka
* Balíček: ProjectTesting
* Soubor: CalcUnitTest.cs
* Datum: 28.03.2017
* Autor: Jaromír Franěk
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 11.04.2017
*
* Popis: Testovací třída pro testování funkcí Výpočetní jednotky
* Třída testuje správnost funkcí výpočetní jednotky CalculatorUnit.Calculation.
* Testy jsou vytvářeny ve filozofii TDD. Odpovědni za projekt jsou Jaromír Franěk a Pavel Vosyka.
*
*****************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorUnit;
using System.Globalization;

namespace ProjectTesting
{
  /// <summary>
  /// Testování funkcí výpočetní jednotky
  /// </summary>
  [TestClass]
  public class CalcUnitTest
  {

    const double PRECISION = 1e-6;

    public Calculation calcunit;
    public string argument = "";

    public CalcUnitTest()
    {
      calcunit = new Calculation(argument);
    }

    [TestMethod]
    public void TestBaseStringValue()
    {
      calcunit.Expression = "9 + 2";
      Assert.AreEqual(11, calcunit.Value, PRECISION);
      calcunit.Expression = "-9 + 11";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "-9.2 + 1.2";
      Assert.AreEqual(-8, calcunit.Value, PRECISION);
      calcunit.Expression = "8 - 3";
      Assert.AreEqual(5, calcunit.Value, PRECISION);
      calcunit.Expression = "8 - 11";
      Assert.AreEqual(-3, calcunit.Value, PRECISION);
      calcunit.Expression = "-4.25 - 11.25";
      Assert.AreEqual(-15.5, calcunit.Value, PRECISION);
      calcunit.Expression = "-2 - 3";
      Assert.AreEqual(-5, calcunit.Value, PRECISION);
      calcunit.Expression = "2 - -3";
      Assert.AreEqual(5, calcunit.Value, PRECISION);
      calcunit.Expression = "-8 - -3";
      Assert.AreEqual(-5, calcunit.Value, PRECISION);
      calcunit.Expression = "9 * 2";
      Assert.AreEqual(18, calcunit.Value, PRECISION);
      calcunit.Expression = "9.5 * 2.5";
      Assert.AreEqual(23.75, calcunit.Value, PRECISION);
      calcunit.Expression = "9 * -2";
      Assert.AreEqual(-18, calcunit.Value, PRECISION);
      calcunit.Expression = "-9 * 2";
      Assert.AreEqual(-18, calcunit.Value, PRECISION);
      calcunit.Expression = "-9 * -2";
      Assert.AreEqual(18, calcunit.Value, PRECISION);
      calcunit.Expression = "12 / 3";
      Assert.AreEqual(4, calcunit.Value, PRECISION);
      calcunit.Expression = "12 / -2";
      Assert.AreEqual(-6, calcunit.Value, PRECISION);
      calcunit.Expression = "17.5 / -2.5";
      Assert.AreEqual(-7, calcunit.Value, PRECISION);
      calcunit.Expression = "-12 / 2";
      Assert.AreEqual(-6, calcunit.Value, PRECISION);
      calcunit.Expression = "-12 / -2";
      Assert.AreEqual(6, calcunit.Value, PRECISION);
      calcunit.Expression = "5!";
      Assert.AreEqual(120, calcunit.Value, PRECISION);
      calcunit.Expression = "-5!";
      Assert.AreEqual(-120, calcunit.Value, PRECISION);
      calcunit.Expression = "L100";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "5^3";
      Assert.AreEqual(125, calcunit.Value, PRECISION);
      calcunit.Expression = "2^-3";
      Assert.AreEqual(0.125, calcunit.Value, PRECISION);
      calcunit.Expression = "-2^3";
      Assert.AreEqual(-8, calcunit.Value, PRECISION);
      calcunit.Expression = "-2^-3";
      Assert.AreEqual(-0.125, calcunit.Value, PRECISION);
      calcunit.Expression = "3@27";
      Assert.AreEqual(3, calcunit.Value, PRECISION);
      calcunit.Expression = "-3@27";
      Assert.AreEqual(-3, calcunit.Value, PRECISION);
      calcunit.Expression = "(-3)@1000";
      Assert.AreEqual(0.1, calcunit.Value, PRECISION);
      calcunit.Expression = "3@-1000";
      Assert.AreEqual(10, calcunit.Value, PRECISION);
      calcunit.Expression = "(-3)@-1000";
      Assert.AreEqual(-0.1, calcunit.Value, PRECISION);
      calcunit.Expression = "-(-3)@1000";
      Assert.AreEqual(-0.1, calcunit.Value, PRECISION);
      calcunit.Expression = "6%3";
      Assert.AreEqual(0, calcunit.Value, PRECISION);
      calcunit.Expression = "-6%3";
      Assert.AreEqual(0, calcunit.Value, PRECISION);
      calcunit.Expression = "6%-3";
      Assert.AreEqual(0, calcunit.Value, PRECISION);
      calcunit.Expression = "5%3";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "5%2.3";
      Assert.AreEqual(0.4, calcunit.Value, PRECISION);
      calcunit.Expression = "-5%3";
      Assert.AreEqual(-2, calcunit.Value, PRECISION);
      calcunit.Expression = "5%-3";
      Assert.AreEqual(2, calcunit.Value, PRECISION);

    }

    [TestMethod]
    public void TestStringPreference()
    {
      calcunit.Expression = "4 + 12 / 3";
      Assert.AreEqual(8, calcunit.Value, PRECISION);
      calcunit.Expression = "4 + (12 / 3)";
      Assert.AreEqual(8, calcunit.Value, PRECISION);
      calcunit.Expression = "(4 + 8) / 3";
      Assert.AreEqual(4, calcunit.Value, PRECISION);
      calcunit.Expression = "12 / 3 + 4";
      Assert.AreEqual(8, calcunit.Value, PRECISION);
      calcunit.Expression = "12 / (2 + 4)";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "-12 / (2 + 4)";
      Assert.AreEqual(-2, calcunit.Value, PRECISION);
      calcunit.Expression = "8 - 2 * 3";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "(8 - 2) * 3";
      Assert.AreEqual(18, calcunit.Value, PRECISION);
      calcunit.Expression = "8 - (2 * 3)";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "-2 * 3 + 8";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "-2 * 8 / 2";
      Assert.AreEqual(-8, calcunit.Value, PRECISION);
      calcunit.Expression = "-2 * (3 + 8)";
      Assert.AreEqual(-22, calcunit.Value, PRECISION);
      calcunit.Expression = "(-2) * (3 + 8)";
      Assert.AreEqual(-22, calcunit.Value, PRECISION);
      calcunit.Expression = "-2 * (-3 + 8)";
      Assert.AreEqual(-10, calcunit.Value, PRECISION);
      calcunit.Expression = "(-16 - 4) * (2 - 1)";
      Assert.AreEqual(-20, calcunit.Value, PRECISION);
      calcunit.Expression = "-16 / (3 - -1)";
      Assert.AreEqual(-4, calcunit.Value, PRECISION);
      calcunit.Expression = "(-16) / (3 - -1)";
      Assert.AreEqual(-4, calcunit.Value, PRECISION);
      calcunit.Expression = "-16 + 4 / (3 - -1)";
      Assert.AreEqual(-15, calcunit.Value, PRECISION);
      calcunit.Expression = "(-16 - 4) / (3 - -1)";
      Assert.AreEqual(-5, calcunit.Value, PRECISION);
      calcunit.Expression = "(-16 / 4) + (3 - -1)";
      Assert.AreEqual(0, calcunit.Value, PRECISION);
      calcunit.Expression = "(-16 / 4) - (3 * -1)";
      Assert.AreEqual(-1, calcunit.Value, PRECISION);
      calcunit.Expression = "(8-3)!";
      Assert.AreEqual(120, calcunit.Value, PRECISION);
      calcunit.Expression = "L(200 - 50 * 2)";
      Assert.AreEqual(2, calcunit.Value, PRECISION);
      calcunit.Expression = "(6 - 1)^(3 * 1)";
      Assert.AreEqual(125, calcunit.Value, PRECISION);
      calcunit.Expression = "(6 - 3)@(30 - 3)";
      Assert.AreEqual(3, calcunit.Value, PRECISION);
    }

    [TestMethod]
    public void TestCompStringValue()
    {
      calcunit.Expression = "(-16 / (5 - 1)) - ((2 + 1) * -1)";
      Assert.AreEqual(-1, calcunit.Value, PRECISION);
      calcunit.Expression = "(2*8 - (8 + 4)) + (12 / 3)";
      Assert.AreEqual(8, calcunit.Value, PRECISION);
      calcunit.Expression = "(-16 / (5 - 1)) - ((6 - 3)@(30 - 3) * -1)";
      Assert.AreEqual(-1, calcunit.Value, PRECISION);
      calcunit.Expression = "((7-3)! / (5 + 1)) - ((6 - 3)@(30 - 3) * -1)";
      Assert.AreEqual(7, calcunit.Value, PRECISION);
      calcunit.Expression = "(-(7-3)! / (5 + 1)) - ((6 - 3)@(30 - (L(200 - 50 * 2) + 1)) * -1)";
      Assert.AreEqual(-1, calcunit.Value, PRECISION);
    }

    [TestMethod]
    public void TestErrorNone()
    {
      calcunit.Expression = "4 + 1";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "8 - 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "8 / 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "8 * 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "8 ^ 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "2 @ 16";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "8 * 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "8!";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "15 % 5";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
      calcunit.Expression = "L10";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.IsFalse(double.IsNaN(calcunit.Value));
    }

    [TestMethod]
    public void TestErrorFuncDomain()
    {
      calcunit.Expression = "8 / 0";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.IsTrue(double.IsNaN(calcunit.Value));
      calcunit.Expression = "0 /0";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.IsTrue(double.IsNaN(calcunit.Value));
      calcunit.Expression = "2 @ (-1)";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.IsTrue(double.IsNaN(calcunit.Value));
      calcunit.Expression = "(-20)!";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.IsTrue(double.IsNaN(calcunit.Value));
      calcunit.Expression = "L(-1)";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.IsTrue(double.IsNaN(calcunit.Value));
    }

    [TestMethod]
    public void TestErrorOverFlow()
    {
      calcunit.Expression = double.MaxValue.ToString(CultureInfo.InvariantCulture) + "+ 5";
      Assert.AreEqual(CalcErrorType.DataTypeOverflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = double.MinValue.ToString(CultureInfo.InvariantCulture) + "- 5";
      Assert.AreEqual(CalcErrorType.DataTypeOverflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = double.MaxValue.ToString(CultureInfo.InvariantCulture) + "* 5";
      Assert.AreEqual(CalcErrorType.DataTypeOverflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "1" + double.MaxValue.ToString(CultureInfo.InvariantCulture) + " + 5";
      Assert.AreEqual(CalcErrorType.DataTypeOverflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "200!";
      Assert.AreEqual(CalcErrorType.DataTypeOverflow, calcunit.ErrorType);
      Assert.AreEqual(double.PositiveInfinity, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorExprFormat()
    {
      calcunit.Expression = "5 +";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "+";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5+++3";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "-12-";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5 + * 5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "*5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5 *";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5 ** 12";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5^";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "^5 ";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5^^3";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "@ 5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5 @";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "1 @@ 5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "!";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "L";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "10L ";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "L*10";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5+5a";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5, +1";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "5. + 2";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "(1+1))";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "((())";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "(/)";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = "(";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expression = ")";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorUnknownError()
    {
      calcunit.Expression = null;
      Assert.AreEqual(CalcErrorType.UnknownError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestGetAsString()
    {
      calcunit.Expression = "10";
      Assert.AreEqual("10", calcunit.GetAsString(NumSystem.Dec, ""));
      Assert.AreEqual("1010", calcunit.GetAsString(NumSystem.Bin, ""));
      Assert.AreEqual("a", calcunit.GetAsString(NumSystem.Hex, ""));
      Assert.AreEqual("12", calcunit.GetAsString(NumSystem.Oct, ""));

      calcunit.Expression = "10.23";
      Assert.AreEqual("10.23", calcunit.GetAsString(NumSystem.Dec, ""));
      Assert.AreEqual("1010.0011", calcunit.GetAsString(NumSystem.Bin, ""));
      Assert.AreEqual("a.3ae1", calcunit.GetAsString(NumSystem.Hex, ""));
      Assert.AreEqual("12.1656", calcunit.GetAsString(NumSystem.Oct, ""));

      calcunit.Expression = "AB";
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Dec, ""));
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Bin, ""));
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Hex, ""));
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Oct, ""));
    }
  }
}
