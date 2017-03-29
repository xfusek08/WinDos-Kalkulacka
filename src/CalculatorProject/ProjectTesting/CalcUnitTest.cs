/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí Výpočetní jednotky
* Balíček: ProjectTesting
* Soubor: CalcUnitTest.cs
* Datum: 28.03.2017
* Autor: Jaromír Franěk
* Naposledy upravil: Jaromír Franěk
* Datum poslední změny: 29.03.2017
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
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(calcunit.Value, 5);
      calcunit.Expresion = "8 - 11";
      Assert.AreEqual(calcunit.Value, -3);
      calcunit.Expresion = "-2 - 3";
      Assert.AreEqual(calcunit.Value, -5);
      calcunit.Expresion = "2 - -3";
      Assert.AreEqual(calcunit.Value, 5);
      calcunit.Expresion = "-8 - -3";
      Assert.AreEqual(calcunit.Value, -5);
      calcunit.Expresion = "9 * 2";
      Assert.AreEqual(calcunit.Value, 18);
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
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "8 / 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "8 * 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "8 ^ 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "16 @ 2";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "8 * 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "8!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "15 % 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
      calcunit.Expresion = "L10";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.None);
    }

    [TestMethod]
    public void TestErrorFuncDomain()
    {
      calcunit.Expresion = "8 / 0";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      calcunit.Expresion = "0 /0";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      calcunit.Expresion = "-1 @ 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      calcunit.Expresion = "-1 @ 3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      calcunit.Expresion = "200!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      calcunit.Expresion = "-20!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
      calcunit.Expresion = "L-1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.FuncDomainError);
    }

    [TestMethod]
    public void TestErrorOverFlow()
    {
      calcunit.Expresion = double.MaxValue + "+ 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      calcunit.Expresion = double.MinValue + "- 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      calcunit.Expresion = double.MaxValue + "* 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
      calcunit.Expresion = "1" + double.MaxValue + " + 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.DataTypeOwerflow);
    }

    [TestMethod]
    public void TestErrorExprFormat()
    {
      calcunit.Expresion = "5 +";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "+";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5+++3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "-12-";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5 + * 5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "*5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5 *";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5 ** 12";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5^";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "^5 ";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5^^3";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5 @ ";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "@5";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5 @@ 1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "!";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "L";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "10L ";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "L*10";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = null;
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5+5a";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5, +1";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "5. + 2";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "(1+1))";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "((())";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "(/)";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = "(";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
      calcunit.Expresion = ")";
      Assert.AreEqual(calcunit.ErrorType, CalcErrorType.ExprFormatError);
    }

    [TestMethod]
    public void TestErrorUnknownError()
    {                                                                 

    }

    [TestMethod]
    public void TestGetAsString()
    {

    }
  }
}
