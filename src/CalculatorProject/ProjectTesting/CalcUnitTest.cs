/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí Výpočetní jednotky
* Balíček: ProjectTesting
* Soubor: CalcUnitTest.cs
* Datum: 28.03.2017
* Autor: Jaromír Franěk
* Naposledy upravil: Jaromír Franěk
* Datum poslední změny: 2.04.2017
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
      Assert.AreEqual(calcunit.Value, 11);
      calcunit.Expresion = "-9 + 11";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "-9.2 + 1.2";
      Assert.AreEqual(calcunit.Value, -8);
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(calcunit.Value, 5);
      calcunit.Expresion = "8 - 11";
      Assert.AreEqual(calcunit.Value, -3);
      calcunit.Expresion = "-4.25 - 11.25";
      Assert.AreEqual(calcunit.Value, -15,5);
      calcunit.Expresion = "-2 - 3";
      Assert.AreEqual(calcunit.Value, -5);
      calcunit.Expresion = "2 - -3";
      Assert.AreEqual(calcunit.Value, 5);
      calcunit.Expresion = "-8 - -3";
      Assert.AreEqual(calcunit.Value, -5);
      calcunit.Expresion = "9 * 2";
      Assert.AreEqual(calcunit.Value, 18);
      calcunit.Expresion = "9.5 * 2.5";
      Assert.AreEqual(calcunit.Value, 23.75);
      calcunit.Expresion = "9 * -2";
      Assert.AreEqual(calcunit.Value, -18);
      calcunit.Expresion = "-9 * 2";
      Assert.AreEqual(calcunit.Value, -18);
      calcunit.Expresion = "-9 * -2";
      Assert.AreEqual(calcunit.Value, 18);
      calcunit.Expresion = "12 / 3";
      Assert.AreEqual(calcunit.Value, 4);
      calcunit.Expresion = "12 / -2";
      Assert.AreEqual(calcunit.Value, -6);
      calcunit.Expresion = "17.5 / -2.5";
      Assert.AreEqual(calcunit.Value, -7);
      calcunit.Expresion = "-12 / 2";
      Assert.AreEqual(calcunit.Value, -6);
      calcunit.Expresion = "-12 / -2";
      Assert.AreEqual(calcunit.Value, 6);
      calcunit.Expresion = "5!";
      Assert.AreEqual(calcunit.Value, 120);
      calcunit.Expresion = "5!";
      Assert.AreEqual(calcunit.Value, -120);
      calcunit.Expresion = "l100";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "5^3";
      Assert.AreEqual(calcunit.Value, 125);
      calcunit.Expresion = "2^-3";
      Assert.AreEqual(calcunit.Value, 0.125);
      calcunit.Expresion = "-2^3";
      Assert.AreEqual(calcunit.Value, -8);
      calcunit.Expresion = "-2^-3";
      Assert.AreEqual(calcunit.Value, -0.125);
      calcunit.Expresion = "27@3";
      Assert.AreEqual(calcunit.Value, 3);
      calcunit.Expresion = "-27@3";
      Assert.AreEqual(calcunit.Value, -3);
      calcunit.Expresion = "1000@-3";
      Assert.AreEqual(calcunit.Value, 0.1);
      calcunit.Expresion = "-1000@-3";
      Assert.AreEqual(calcunit.Value, -0.1);
      calcunit.Expresion = "6%3";
      Assert.AreEqual(calcunit.Value, 0);
      calcunit.Expresion = "-6%3";
      Assert.AreEqual(calcunit.Value, 0);
      calcunit.Expresion = "6%-3";
      Assert.AreEqual(calcunit.Value, 0);
      calcunit.Expresion = "5%3";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "5%2.3";
      Assert.AreEqual(calcunit.Value, 0.4);
      calcunit.Expresion = "-5%3";
      Assert.AreEqual(calcunit.Value, -2);
      calcunit.Expresion = "5%-3";
      Assert.AreEqual(calcunit.Value, -2);

    }

    [TestMethod]
    public void TestStringPreference()
    {
      calcunit.Expresion = "4 + 12 / 3";
      Assert.AreEqual(calcunit.Value, 8);
      calcunit.Expresion = "4 + (12 / 3)";
      Assert.AreEqual(calcunit.Value, 8);
      calcunit.Expresion = "(4 + 8) / 3";
      Assert.AreEqual(calcunit.Value, 4);
      calcunit.Expresion = "12 / 3 + 4";
      Assert.AreEqual(calcunit.Value, 4);
      calcunit.Expresion = "12 / (2 + 4)";
      Assert.AreEqual(calcunit.Value, 6);
      calcunit.Expresion = "-12 / (2 + 4)";
      Assert.AreEqual(calcunit.Value, -6);
      calcunit.Expresion = "8 - 2 * 3";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "(8 - 2) * 3";
      Assert.AreEqual(calcunit.Value, 18);
      calcunit.Expresion = "8 - (2 * 3)";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "-2 * 3 + 8";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "-2 * 8 / 2";
      Assert.AreEqual(calcunit.Value, -8);
      calcunit.Expresion = "-2 * (3 + 8)";
      Assert.AreEqual(calcunit.Value, -22);
      calcunit.Expresion = "(-2) * (3 + 8)";
      Assert.AreEqual(calcunit.Value, -22);
      calcunit.Expresion = "-2 * (-3 + 8)";
      Assert.AreEqual(calcunit.Value, -10);
      calcunit.Expresion = "(-16 - 4) * (2 - 1)";
      Assert.AreEqual(calcunit.Value, -20);
      calcunit.Expresion = "-16 / (3 - -1)";
      Assert.AreEqual(calcunit.Value, -4);
      calcunit.Expresion = "(-16) / (3 - -1)";
      Assert.AreEqual(calcunit.Value, -4);
      calcunit.Expresion = "-16 + 4 / (3 - -1)";
      Assert.AreEqual(calcunit.Value, -15);
      calcunit.Expresion = "(-16 - 4) / (3 - -1)";
      Assert.AreEqual(calcunit.Value, -5);
      calcunit.Expresion = "(-16 / 4) + (3 - -1)";
      Assert.AreEqual(calcunit.Value, 0);
      calcunit.Expresion = "(-16 / 4) - (3 * -1)";
      Assert.AreEqual(calcunit.Value, -1);
      calcunit.Expresion = "(8-3)!";
      Assert.AreEqual(calcunit.Value, 120);
      calcunit.Expresion = "L(200 - 50 * 2)";
      Assert.AreEqual(calcunit.Value, 2);
      calcunit.Expresion = "(6 - 1)^(3 * 1)";
      Assert.AreEqual(calcunit.Value, 125);
      calcunit.Expresion = "(30 - 3)@(6 - 3)";
      Assert.AreEqual(calcunit.Value, 3);
    }

    [TestMethod]
    public void TestCompStringValue()
    {
      calcunit.Expresion = "(-16 / (5 - 1)) - ((2 + 1) * -1)";
      Assert.AreEqual(calcunit.Value, -1);
      calcunit.Expresion = "(2*8 - (8 + 4)) + (12 / 3)";
      Assert.AreEqual(calcunit.Value, 8);
      calcunit.Expresion = "(-16 / (5 - 1)) - ((30 - 3)@(6 - 3) * -1)";
      Assert.AreEqual(calcunit.Value, -1);
      calcunit.Expresion = "((7-3)! / (5 + 1)) - ((30 - 3)@(6 - 3) * -1)";
      Assert.AreEqual(calcunit.Value, -1);
      calcunit.Expresion = "((7-3)! / (5 + 1)) - ((30 - (L(200 - 50 * 2) + 1))@(6 - 3) * -1)";
      Assert.AreEqual(calcunit.Value, -1);
    }

    [TestMethod]
    public void TestErrorNone()
    {
      calcunit.Expresion = "4 + 1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "8 / 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "8 * 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "8 ^ 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "16 @ 2";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "8 * 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "8!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "15 % 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "L10";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      Assert.AreEqual(calcunit.Value, double.NaN);
    }

    [TestMethod]
    public void TestErrorFuncDomain()
    {
      calcunit.Expresion = "8 / 0";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "0 /0";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "-1 @ 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "-1 @ 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "200!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "-20!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "L-1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      Assert.AreEqual(calcunit.Value, double.NaN);
    }

    [TestMethod]
    public void TestErrorOverFlow()
    {
      calcunit.Expresion = double.MaxValue + "+ 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = double.MinValue + "- 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = double.MaxValue + "* 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "1" + double.MaxValue + " + 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      Assert.AreEqual(calcunit.Value, double.NaN);
    }

    [TestMethod]
    public void TestErrorExprFormat()
    {
      calcunit.Expresion = "5 +";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "+";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5+++3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "-12-";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5 + * 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "*5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5 *";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5 ** 12";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5^";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "^5 ";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5^^3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5 @ ";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "@5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5 @@ 1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "L";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "10L ";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "L*10";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = null;
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5+5a";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5, +1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "5. + 2";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "(1+1))";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "((())";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "(/)";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = "(";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
      calcunit.Expresion = ")";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      Assert.AreEqual(calcunit.Value, double.NaN);
    }

    [TestMethod]
    public void TestErrorUnknownError()
    {
      calcunit.Expresion = null;
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.UnknownError);
      Assert.AreEqual(calcunit.Value, double.NaN);
    }

    [TestMethod]
    public void TestGetAsString()
    {
      calcunit.Expresion = "10";
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Dec, ""), "10");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Bin, ""), "1010");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Hex, ""), "10");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Oct, ""), "12");

      calcunit.Expresion = "10.23";
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Dec, ""), "10.23");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Bin, ""), "1010.0011");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Hex, ""), "1.475c");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Oct, ""), "12.1656");

      calcunit.Expresion = "AB";
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Dec, ""), "NaN");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Bin, ""), "NaN");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Hex, ""), "NaN");
      Assert.AreEqual(calcunit.GetAsString(NumSystem.Oct, ""), "NaN");
    }
  }
}
