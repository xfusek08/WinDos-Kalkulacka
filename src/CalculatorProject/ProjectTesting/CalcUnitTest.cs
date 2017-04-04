/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí Výpočetní jednotky
* Balíček: ProjectTesting
* Soubor: CalcUnitTest.cs
* Datum: 28.03.2017
* Autor: Jaromír Franěk
* Naposledy upravil: Pavel Vosyka
* Datum poslední změny: 3.04.2017
*
* Popis: Třída testuje správnost funkcí výpočetní jednotky CalculatorUnit.Calculation. 
* Testy jsou vytvářeny ve filozofii TDD. Odpovědni za projekt jsou Jaromír Franěk a Pavel Vosyka.
*
*****************************************************************/

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorUnit;

namespace ProjectTesting
{
  /// <summary>
  /// Testování funkcí výpočetní jednotky
  /// </summary>
  [TestClass]
  public class CalcUnitTest
  {

    public Calculation calcunit;
    public string argument = "";  

    public CalcUnitTest()
    {
      calcunit = new Calculation(argument);
    }

    [TestMethod]
    public void TestBaseStringValue()
    {
      calcunit.Expresion = "9 + 2";
      Assert.AreEqual(11, calcunit.Value);
      calcunit.Expresion = "-9 + 11";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "-9.2 + 1.2";
      Assert.AreEqual(-8, calcunit.Value);
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(5, calcunit.Value);
      calcunit.Expresion = "8 - 11";
      Assert.AreEqual(-3, calcunit.Value);
      calcunit.Expresion = "-4.25 - 11.25";
      Assert.AreEqual(-15,5, calcunit.Value);
      calcunit.Expresion = "-2 - 3";
      Assert.AreEqual(-5, calcunit.Value);
      calcunit.Expresion = "2 - -3";
      Assert.AreEqual(5, calcunit.Value);
      calcunit.Expresion = "-8 - -3";
      Assert.AreEqual(-5, calcunit.Value);
      calcunit.Expresion = "9 * 2";
      Assert.AreEqual(18, calcunit.Value);
      calcunit.Expresion = "9.5 * 2.5";
      Assert.AreEqual(23.75, calcunit.Value);
      calcunit.Expresion = "9 * -2";
      Assert.AreEqual(-18, calcunit.Value);
      calcunit.Expresion = "-9 * 2";
      Assert.AreEqual(-18, calcunit.Value);
      calcunit.Expresion = "-9 * -2";
      Assert.AreEqual(18, calcunit.Value);
      calcunit.Expresion = "12 / 3";
      Assert.AreEqual(4, calcunit.Value);
      calcunit.Expresion = "12 / -2";
      Assert.AreEqual(-6, calcunit.Value);
      calcunit.Expresion = "17.5 / -2.5";
      Assert.AreEqual(-7, calcunit.Value);
      calcunit.Expresion = "-12 / 2";
      Assert.AreEqual(-6, calcunit.Value);
      calcunit.Expresion = "-12 / -2";
      Assert.AreEqual(6, calcunit.Value);
      calcunit.Expresion = "5!";
      Assert.AreEqual(120, calcunit.Value);
      calcunit.Expresion = "5!";
      Assert.AreEqual(-120, calcunit.Value);
      calcunit.Expresion = "l100";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "5^3";
      Assert.AreEqual(125, calcunit.Value);
      calcunit.Expresion = "2^-3";
      Assert.AreEqual(0.125, calcunit.Value);
      calcunit.Expresion = "-2^3";
      Assert.AreEqual(-8, calcunit.Value);
      calcunit.Expresion = "-2^-3";
      Assert.AreEqual(-0.125, calcunit.Value);
      calcunit.Expresion = "27@3";
      Assert.AreEqual(3, calcunit.Value);
      calcunit.Expresion = "-27@3";
      Assert.AreEqual(-3, calcunit.Value);
      calcunit.Expresion = "1000@-3";
      Assert.AreEqual(0.1, calcunit.Value);
      calcunit.Expresion = "-1000@-3";
      Assert.AreEqual(-0.1, calcunit.Value);
      calcunit.Expresion = "6%3";
      Assert.AreEqual(0, calcunit.Value);
      calcunit.Expresion = "-6%3";
      Assert.AreEqual(0, calcunit.Value);
      calcunit.Expresion = "6%-3";
      Assert.AreEqual(0, calcunit.Value);
      calcunit.Expresion = "5%3";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "5%2.3";
      Assert.AreEqual(0.4, calcunit.Value);
      calcunit.Expresion = "-5%3";
      Assert.AreEqual(-2, calcunit.Value);
      calcunit.Expresion = "5%-3";
      Assert.AreEqual(-2, calcunit.Value);

    }

    [TestMethod]
    public void TestStringPreference()
    {
      calcunit.Expresion = "4 + 12 / 3";
      Assert.AreEqual(8, calcunit.Value);
      calcunit.Expresion = "4 + (12 / 3)";
      Assert.AreEqual(8, calcunit.Value);
      calcunit.Expresion = "(4 + 8) / 3";
      Assert.AreEqual(4, calcunit.Value);
      calcunit.Expresion = "12 / 3 + 4";
      Assert.AreEqual(4, calcunit.Value);
      calcunit.Expresion = "12 / (2 + 4)";
      Assert.AreEqual(6, calcunit.Value);
      calcunit.Expresion = "-12 / (2 + 4)";
      Assert.AreEqual(-6, calcunit.Value);
      calcunit.Expresion = "8 - 2 * 3";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "(8 - 2) * 3";
      Assert.AreEqual(18, calcunit.Value);
      calcunit.Expresion = "8 - (2 * 3)";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "-2 * 3 + 8";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "-2 * 8 / 2";
      Assert.AreEqual(-8, calcunit.Value);
      calcunit.Expresion = "-2 * (3 + 8)";
      Assert.AreEqual(-22, calcunit.Value);
      calcunit.Expresion = "(-2) * (3 + 8)";
      Assert.AreEqual(-22, calcunit.Value);
      calcunit.Expresion = "-2 * (-3 + 8)";
      Assert.AreEqual(-10, calcunit.Value);
      calcunit.Expresion = "(-16 - 4) * (2 - 1)";
      Assert.AreEqual(-20, calcunit.Value);
      calcunit.Expresion = "-16 / (3 - -1)";
      Assert.AreEqual(-4, calcunit.Value);
      calcunit.Expresion = "(-16) / (3 - -1)";
      Assert.AreEqual(-4, calcunit.Value);
      calcunit.Expresion = "-16 + 4 / (3 - -1)";
      Assert.AreEqual(-15, calcunit.Value);
      calcunit.Expresion = "(-16 - 4) / (3 - -1)";
      Assert.AreEqual(-5, calcunit.Value);
      calcunit.Expresion = "(-16 / 4) + (3 - -1)";
      Assert.AreEqual(0, calcunit.Value);
      calcunit.Expresion = "(-16 / 4) - (3 * -1)";
      Assert.AreEqual(-1, calcunit.Value);
      calcunit.Expresion = "(8-3)!";
      Assert.AreEqual(120, calcunit.Value);
      calcunit.Expresion = "L(200 - 50 * 2)";
      Assert.AreEqual(2, calcunit.Value);
      calcunit.Expresion = "(6 - 1)^(3 * 1)";
      Assert.AreEqual(125, calcunit.Value);
      calcunit.Expresion = "(30 - 3)@(6 - 3)";
      Assert.AreEqual(3, calcunit.Value);
    }

    [TestMethod]
    public void TestCompStringValue()
    {
      calcunit.Expresion = "(-16 / (5 - 1)) - ((2 + 1) * -1)";
      Assert.AreEqual(-1, calcunit.Value);
      calcunit.Expresion = "(2*8 - (8 + 4)) + (12 / 3)";
      Assert.AreEqual(8, calcunit.Value);
      calcunit.Expresion = "(-16 / (5 - 1)) - ((30 - 3)@(6 - 3) * -1)";
      Assert.AreEqual(-1, calcunit.Value);
      calcunit.Expresion = "((7-3)! / (5 + 1)) - ((30 - 3)@(6 - 3) * -1)";
      Assert.AreEqual(-1, calcunit.Value);
      calcunit.Expresion = "((7-3)! / (5 + 1)) - ((30 - (L(200 - 50 * 2) + 1))@(6 - 3) * -1)";
      Assert.AreEqual(-1, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorNone()
    {
      calcunit.Expresion = "4 + 1";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "8 / 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "8 * 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "8 ^ 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "16 @ 2";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "8 * 3";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "8!";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "15 % 5";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "L10";
      Assert.AreEqual(CalcErrorType.None, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorFuncDomain()
    {
      calcunit.Expresion = "8 / 0";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "0 /0";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "-1 @ 3";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "-1 @ 3";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "200!";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "-20!";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "L-1";
      Assert.AreEqual(CalcErrorType.FuncDomainError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorOverFlow()
    {
      calcunit.Expresion = double.MaxValue + "+ 5";
      Assert.AreEqual(CalcErrorType.DataTypeOwerflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = double.MinValue + "- 5";
      Assert.AreEqual(CalcErrorType.DataTypeOwerflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = double.MaxValue + "* 5";
      Assert.AreEqual(CalcErrorType.DataTypeOwerflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "1" + double.MaxValue + " + 5";
      Assert.AreEqual(CalcErrorType.DataTypeOwerflow, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorExprFormat()
    {
      calcunit.Expresion = "5 +";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "+";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5+++3";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "-12-";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5 + * 5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "*5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5 *";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5 ** 12";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5^";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "^5 ";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5^^3";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5 @ ";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "@5";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5 @@ 1";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "!";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "L";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "10L ";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "L*10";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = null;
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5+5a";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5, +1";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "5. + 2";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "(1+1))";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "((())";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "(/)";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = "(";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
      calcunit.Expresion = ")";
      Assert.AreEqual(CalcErrorType.ExprFormatError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestErrorUnknownError()
    {
      calcunit.Expresion = null;
      Assert.AreEqual(CalcErrorType.UnknownError, calcunit.ErrorType);
      Assert.AreEqual(double.NaN, calcunit.Value);
    }

    [TestMethod]
    public void TestGetAsString()
    {
      calcunit.Expresion = "10";
      Assert.AreEqual("10", calcunit.GetAsString(NumSystem.Dec, ""));
      Assert.AreEqual("1010", calcunit.GetAsString(NumSystem.Bin, ""));
      Assert.AreEqual("10", calcunit.GetAsString(NumSystem.Hex, ""));
      Assert.AreEqual("12", calcunit.GetAsString(NumSystem.Oct, ""));

      calcunit.Expresion = "10.23";
      Assert.AreEqual("10.23", calcunit.GetAsString(NumSystem.Dec, ""));
      Assert.AreEqual("1010.0011", calcunit.GetAsString(NumSystem.Bin, ""));
      Assert.AreEqual("1.475c", calcunit.GetAsString(NumSystem.Hex, ""));
      Assert.AreEqual("12.1656", calcunit.GetAsString(NumSystem.Oct, ""));

      calcunit.Expresion = "AB";
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Dec, ""));
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Bin, ""));
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Hex, ""));
      Assert.AreEqual("NaN", calcunit.GetAsString(NumSystem.Oct, ""));
    }
  }
}
