/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí matematické knihovny
* Balíček: ProjectTesting
* Soubor: MathLibTest.cs
* Datum: 21.03.2017
* Autor: Petr Fusek
* Naposledy upravil: Radim Blaha
* Datum poslední změny: 24.03.2017
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

    [TestMethod]
    public void TestDivide()
    {
      Assert.AreEqual(5, mathclass.Divide(10, 5)); // 10/5=2
      Assert.AreEqual(-8, mathclass.Divide(-80, 10)); // -80/10=-8
      Assert.AreEqual(-4, mathclass.Divide(40, -10)); // 40/-10=-4
      Assert.AreEqual(60, mathclass.Divide(-300, -5)); // -300/-5=60

      Assert.AreEqual(250000, mathclass.Divide(1, 0.000004)); // 1/0.000004=250000
      Assert.AreEqual(-250000, mathclass.Divide(-1, 0.000004)); // -1/0.000004=-250000
      Assert.AreEqual(-250000, mathclass.Divide(1, -0.000004)); // 1/-0.000004=-250000
      Assert.AreEqual(250000, mathclass.Divide(-1, -0.000004)); // -1/-0.000004=250000

      Assert.AreEqual(0.0000005, mathclass.Divide(0.000004, 8)); // 0.000004/8=0.0000005
      Assert.AreEqual(-0.0000005, mathclass.Divide(-0.000004, 8)); // -0.000004/8=-0.0000005
      Assert.AreEqual(-0.0000005, mathclass.Divide(0.000004, -8)); // 0.000004/-8=-0.0000005
      Assert.AreEqual(0.0000005, mathclass.Divide(-0.000004, -8)); // -0.000004/-8=0.0000005

      Assert.AreEqual(0.125, mathclass.Divide(0.0000005, 0.000004)); // 0.0000005/0.000004=0.125

      Assert.AreEqual(1, mathclass.Divide(0.0000005, 0.0000005)); // 0.0000005/0.0000005=1

      Assert.AreEqual(0, mathclass.Divide(0, 5)); // 0/5=0
      Assert.AreEqual(0, mathclass.Divide(0, -5)); // 0/-5=0
      //Assert.AreEqual(0, mathclass.Divide(0, double.PositiveInfinity)); // 0/INF=0 - ANO nebo NE?
      Assert.AreEqual(0, mathclass.Divide(5, double.PositiveInfinity)); // 5/INF=0

      Assert.IsTrue(double.IsNaN(mathclass.Divide(0,0))); // 0/0=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Divide(0, 0))); // 0/INF=0
      //Assert.IsTrue(double.IsNaN(mathclass.Divide(0, double.PositiveInfinity))); // INF/0=NaN - ANO nebo NE? 2. moznost: INF/0=INF
      Assert.IsTrue(double.IsNaN(mathclass.Divide(double.PositiveInfinity, double.PositiveInfinity)));  // INF/INF=NaN
    }
  }
}
