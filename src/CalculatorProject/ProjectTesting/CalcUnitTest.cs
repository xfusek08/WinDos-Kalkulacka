/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí Výpočetní jednotky
* Balíček: ProjectTesting
* Soubor: CalcUnitTest.cs
* Datum: 28.03.2017
* Autor: Jaromír Franěk
* Naposledy upravil: Jaromír Franěk
* Datum poslední změny: 28.03.2017
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
      calcunit.Expresion = "8 - 3";
      Assert.AreEqual(calcunit.Value, 5);
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
    }

    [TestMethod]
    public void TestCompStringValue()
    {
      
    }

    [TestMethod]
    public void TestErrorNone()
    {

    }

    [TestMethod]
    public void TestErrorFuncDomain()
    {

    }

    [TestMethod]
    public void TestErrorOverFlow()
    {

    }

    [TestMethod]
    public void TestErrorExprFormat()
    {

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
