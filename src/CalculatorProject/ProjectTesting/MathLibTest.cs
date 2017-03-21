/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí matematické knihovny
* Balíček: ProjectTesting
* Soubor: MathLibTest.cs
* Datum: 21.03.2017
* Autor: Petr Fusek
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 21.03.2017
*
* Popis: Třída testuje funkce matematického objektu MathLib.CalcMath.
* Testy jsou vytvářeny ve filozofii TDD. Odpovědni za projekt jsou Petr Fusek a Radim Blaha.
*
*****************************************************************/

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLib;

namespace ProjectTesting
{
  /// <summary>
  /// Testování funkcí matematické knihovny
  /// </summary>
  [TestClass]
  public class MathLibTest
  {

    public CalcMath mathclass;

    public MathLibTest()
    {
      mathclass = new CalcMath();
    }

    [TestMethod]
    public void TestAdd()
    {
      Assert.AreEqual(0, mathclass.Add(0, 0));
      Assert.AreEqual(-1, mathclass.Add(1, -2));
      Assert.AreEqual(1, mathclass.Add(0.5, 0.5));
      Assert.AreEqual(double.PositiveInfinity, mathclass.Add(double.PositiveInfinity, 5));
      Assert.AreEqual(double.NegativeInfinity, mathclass.Add(double.NegativeInfinity, 5));
      Assert.IsTrue(double.IsNaN(mathclass.Add(double.NaN, 5)));
      Assert.IsTrue(double.IsNaN(mathclass.Add(double.PositiveInfinity, double.NegativeInfinity)));
    }

    [TestMethod]
    public void TestSubtract()
    {
      Assert.AreEqual(0, mathclass.Subtract(0, 0));
      Assert.AreEqual(3, mathclass.Subtract(1, -2));
      Assert.AreEqual(-5, mathclass.Subtract(3, 8));
      Assert.AreEqual(0, mathclass.Subtract(0.5, 0.5));
      Assert.AreEqual(-1.5, mathclass.Subtract(0.5, 2));
      Assert.AreEqual(double.PositiveInfinity, mathclass.Subtract(double.PositiveInfinity, 5));
      Assert.AreEqual(double.NegativeInfinity, mathclass.Subtract(double.NegativeInfinity, 5));
      Assert.AreEqual(double.PositiveInfinity, mathclass.Subtract(0, double.NegativeInfinity));
      Assert.AreEqual(double.NegativeInfinity, mathclass.Subtract(0, double.PositiveInfinity));
      Assert.IsTrue(double.IsNaN(mathclass.Subtract(double.NaN, 5)));
      Assert.IsTrue(double.IsNaN(mathclass.Subtract(double.PositiveInfinity, double.NegativeInfinity)));
    }
  }
}
