/*******************************************************************
* Název projektu: Testovací třída pro testování funkcí matematické knihovny
* Balíček: ProjectTesting
* Soubor: MathLibTest.cs
* Datum: 21.03.2017
* Autor: Petr Fusek
* Naposledy upravil: Petr Fusek
* Datum poslední změny: 28.03.2017
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
      Assert.AreEqual(-8, mathclass.Divide(-80, 10)); // (-80)/10=-8
      Assert.AreEqual(-4, mathclass.Divide(40, -10)); // 40/(-10)=-4
      Assert.AreEqual(60, mathclass.Divide(-300, -5)); // (-300)/(-5)=60

      Assert.AreEqual(250000, mathclass.Divide(1, 0.000004)); // 1/0.000004=250000
      Assert.AreEqual(-250000, mathclass.Divide(-1, 0.000004)); // (-1)/0.000004=-250000
      Assert.AreEqual(-250000, mathclass.Divide(1, -0.000004)); // 1/(-0.000004)=-250000
      Assert.AreEqual(250000, mathclass.Divide(-1, -0.000004)); // (-1)/(-0.000004)=250000

      Assert.AreEqual(0.0000005, mathclass.Divide(0.000004, 8)); // 0.000004/8=0.0000005
      Assert.AreEqual(-0.0000005, mathclass.Divide(-0.000004, 8)); // (-0.000004)/8=-0.0000005
      Assert.AreEqual(-0.0000005, mathclass.Divide(0.000004, -8)); // 0.000004/(-8)=-0.0000005
      Assert.AreEqual(0.0000005, mathclass.Divide(-0.000004, -8)); // (-0.000004)/(-8)=0.0000005

      Assert.AreEqual(0.125, mathclass.Divide(0.0000005, 0.000004)); // 0.0000005/0.000004=0.125

      Assert.AreEqual(1, mathclass.Divide(0.0000005, 0.0000005)); // 0.0000005/0.0000005=1

      Assert.AreEqual(0, mathclass.Divide(0, 5)); // 0/5=0
      Assert.AreEqual(0, mathclass.Divide(0, -5)); // 0/(-5)=0
      Assert.AreEqual(0, mathclass.Divide(0, double.PositiveInfinity)); // 0/INF=0
      Assert.AreEqual(0, mathclass.Divide(5, double.PositiveInfinity)); // 5/INF=0

      Assert.IsTrue(double.IsNaN(mathclass.Divide(0,0))); // 0/0=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Divide(0, 0))); // 0/INF=0
      Assert.IsTrue(double.IsNaN(mathclass.Divide(0, double.PositiveInfinity))); // INF/0=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Divide(double.PositiveInfinity, double.PositiveInfinity)));  // INF/INF=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Divide(double.NaN, 5)));  //NaN/5=NaN
    }

    [TestMethod]
    public void TestMultiply()
    {
      Assert.AreEqual(0, mathclass.Multipy(0, 0));  // 0*0=0
      Assert.AreEqual(0, mathclass.Multipy(9, 0));  // 9*0=0
      Assert.AreEqual(0, mathclass.Multipy(0, 5));  // 0*5=0
      Assert.AreEqual(0, mathclass.Multipy(-5, 0));  // (-5)*0=0
      Assert.AreEqual(0, mathclass.Multipy(0, -9));  // 0*(-9)=0

      Assert.AreEqual(350, mathclass.Multipy(14, 25));  // 14*25=350
      Assert.AreEqual(-112, mathclass.Multipy(-14, 8));  // (-14)*8=-112
      Assert.AreEqual(-168, mathclass.Multipy(6, -42));  // 6*(-42)=-168
      Assert.AreEqual(936, mathclass.Multipy(-52, -18));  // (-52)*(-18)=936

      Assert.AreEqual(double.PositiveInfinity, mathclass.Multipy(double.PositiveInfinity, double.PositiveInfinity));  // INF*INF=INF
      Assert.AreEqual(double.NegativeInfinity, mathclass.Multipy(double.PositiveInfinity, double.NegativeInfinity));  // INF*(-INF)=-INF
      Assert.AreEqual(double.PositiveInfinity, mathclass.Multipy(double.NegativeInfinity, double.NegativeInfinity));  // (-INF)*(-INF)=INF
      Assert.AreEqual(double.PositiveInfinity, mathclass.Multipy(double.PositiveInfinity, 5));  // INF*5=INF
      Assert.AreEqual(double.NegativeInfinity, mathclass.Multipy(-3, double.PositiveInfinity));  // (-3)*INF=-INF
      Assert.AreEqual(double.PositiveInfinity, mathclass.Multipy(-3, double.NegativeInfinity));  // (-3)*(-INF)=INF

      //Testy maximálního rozsahu
      Assert.IsTrue(double.IsNaN(mathclass.Multipy(double.MaxValue, double.MaxValue)));
      Assert.IsTrue(double.IsNaN(mathclass.Multipy(double.MinValue, double.MaxValue)));
      Assert.IsTrue(double.IsNaN(mathclass.Multipy(double.MaxValue, double.MinValue)));
      Assert.IsTrue(double.IsNaN(mathclass.Multipy(double.MinValue, double.MinValue)));

      Assert.IsTrue(double.IsNaN(mathclass.Multipy(double.PositiveInfinity, 0))); // INF*0=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Multipy(double.NaN, 5)));  //NaN*5=NaN
    }

    [TestMethod]
    public void TestPow()
    {
      Assert.AreEqual(1, mathclass.Pow(2, 0));  // 2^0=1
      Assert.AreEqual(0, mathclass.Pow(0, 5));  // 0^5=0
      Assert.AreEqual(390625, mathclass.Pow(5, 8));  // 5^8=390625
      Assert.AreEqual(-390625, mathclass.Pow(-5, 8));  // (-5)^8=-390625
      Assert.AreEqual(0.00000256, mathclass.Pow(5, -8));  // 5^(-8)=0.00000256
      Assert.AreEqual(0.04, mathclass.Pow(-5, -2));  // (-5)^(-2)=0.04

      Assert.AreEqual(1525.87890625, mathclass.Pow(2.5, 8));  // 2.5^8=1525.87890625
      Assert.AreEqual(-1525.87890625, mathclass.Pow(-2.5, 8));  // (-2.5)^8=-1525.87890625
      Assert.AreEqual(0.00065536, mathclass.Pow(2.5, -8));  // 2.5^(-8)=0.00065536
      Assert.AreEqual(0.00065536, mathclass.Pow(-2.5, -8));  // (-2.5)^(-8)=0.00065536

      Assert.AreEqual(9, mathclass.Pow(59049, 0.2));  // 59049^0.2=9
      Assert.AreEqual(-9, mathclass.Pow(-59049, 0.2));  // (-59049)^0.2=9
      Assert.AreEqual(0.4352752816, Math.Round(mathclass.Pow(64, -0.2)), 10);  // 64^(-0.2)~0.4352752816
      Assert.AreEqual(-0.4352752816, Math.Round(mathclass.Pow(-64, -0.2)), 10);  // (-64)^(-0.2)~(-0.4352752816)

      Assert.AreEqual(1872.1207090519, Math.Round(mathclass.Pow(15.8, 2.73)), 10);  // 15.8^2.73~1872.1207090519
      Assert.AreEqual(0.0227950102, Math.Round(mathclass.Pow(15.8, -1.37)), 10);  // 15.8^(-1.37)~0.0227950102

      Assert.AreEqual(double.PositiveInfinity, mathclass.Pow(2, double.PositiveInfinity));  // 2^INF=INF
      Assert.AreEqual(double.PositiveInfinity, mathclass.Pow(double.PositiveInfinity, 2));  // INF^2=INF
      Assert.AreEqual(double.PositiveInfinity, mathclass.Pow(double.PositiveInfinity, 3));  // INF^3=INF

      //Testy maximálního rozsahu
      Assert.IsTrue(double.IsNaN(mathclass.Pow(double.MaxValue, double.MaxValue)));
      Assert.IsTrue(double.IsNaN(mathclass.Pow(double.MinValue, double.MaxValue)));
      Assert.IsTrue(double.IsNaN(mathclass.Pow(double.MaxValue, double.MinValue)));
      Assert.IsTrue(double.IsNaN(mathclass.Pow(double.MinValue, double.MinValue)));

      Assert.IsTrue(double.IsNaN(mathclass.Pow(5, double.NaN)));  // 5^NaN=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Pow(double.NaN, 5)));  // NaN^5=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Pow(double.NaN, double.NaN)));  // NaN^NaN=NaN

      Assert.IsTrue(double.IsNaN(mathclass.Pow(0, -5)));  // 0^(-5)=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Pow(-64, -0.3)));  // (-64)^(0.3)=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Pow(-15.8, 2.73)));  // (-15.8)^2.73=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Pow(-15.8, -2.73)));  // (-15.8)^(-2.73)=NaN
    }

    [TestMethod]
    public void TestRoot()
    {
      Assert.AreEqual(0, mathclass.Root(2, 0));
      Assert.AreEqual(1.5341274046, Math.Round(mathclass.Root(7, 20)), 10);  //zaokrouhlená sedmá odmocnina z dvaceti
      Assert.AreEqual(-19, mathclass.Root(3, -6859));
      Assert.AreEqual(16, mathclass.Root(8, 4294967296));

      Assert.AreEqual(double.PositiveInfinity, mathclass.Root(2, double.PositiveInfinity));

      Assert.IsTrue(double.IsNaN(mathclass.Root(2, -8)));
      Assert.IsTrue(double.IsNaN(mathclass.Root(0, 8)));
      Assert.IsTrue(double.IsNaN(mathclass.Root(-2, -4)));
      Assert.IsTrue(double.IsNaN(mathclass.Root(-3, -4)));
    }

    [TestMethod]
    public void TestFact()
    {
      Assert.AreEqual(1.0, mathclass.Fact(0));  // 0! = 1
      Assert.AreEqual(1.0, mathclass.Fact(Byte.MinValue));  // Byte.MinValue = 0 => 0! = 1
      Assert.AreEqual(1.0, mathclass.Fact(1));  // 1! = 1
      Assert.AreEqual(6.0, mathclass.Fact(3));  // 3! = 6
      Assert.AreEqual(3628800.0, mathclass.Fact(10)); // 10! = 3628800
      Assert.AreEqual(double.PositiveInfinity, mathclass.Fact(Byte.MaxValue)); // 255! > double.MaxValue
      Assert.AreEqual(double.PositiveInfinity, mathclass.Fact(171)); // 171! > double.MaxValue
      Assert.AreEqual("7.257E+306", mathclass.Fact(170).ToString("E3")); // 170! = "7.257E+306"
    }

    [TestMethod]
    public void TestModulo()
    {
      Assert.AreEqual(0, mathclass.Modulo(10, 5)); // 10 % 5 = 0
      Assert.AreEqual(-8, mathclass.Modulo(-80, 9)); // (-80) % 9 = -8
      Assert.AreEqual(4, mathclass.Modulo(40, -12)); // 40 % (-12) = 4
      Assert.AreEqual(-2, mathclass.Modulo(-200, -3)); // (-200) % (-3) = -2

      Assert.AreEqual(0.42, mathclass.Modulo(14, 0.97)); // 14 % 0.97 = 0.42
      Assert.AreEqual(-0.42, mathclass.Modulo(-14, 0.97)); // (-14) % 0.97 = -0.42
      Assert.AreEqual(0.42, mathclass.Modulo(14, -0.97)); // 14 % (-0.97) = 0.42
      Assert.AreEqual(-0.42, mathclass.Modulo(-14, -0.97)); // (-14) % (-0.97) = -0.42

      Assert.AreEqual(10.42, mathclass.Modulo(38.42, 14));  // 38.42 % 14 = 10.42
      Assert.AreEqual(-10.42, mathclass.Modulo(-38.42, 14));  // (-38.42) % 14 = -10.42
      Assert.AreEqual(10.42, mathclass.Modulo(38.42, -14)); // 38.42 % (-14) = 10.42
      Assert.AreEqual(-10.42, mathclass.Modulo(-38.42, -14)); // (-38.42) % (-14) = -10.42

      Assert.AreEqual(0.02, mathclass.Modulo(0.08, 0.03)); // 0.08 % 0.03 = 0.02

      Assert.AreEqual(0, mathclass.Modulo(0.0000005, 0.0000005)); // 0.0000005 % 0.0000005 = 0

      Assert.IsTrue(double.IsNaN(mathclass.Modulo(double.NaN, 5))); // NaN % 5=NaN
      Assert.IsTrue(double.IsNaN(mathclass.Modulo(double.NaN, double.NaN))); // NaN % NaN = NaN
      Assert.IsTrue(double.IsNaN(mathclass.Modulo(0, 0))); // 0 % 0 = NaN
      Assert.IsTrue(double.IsNaN(mathclass.Modulo(7, 0))); // 7 % 0 = NaN
    }

    [TestMethod]
    public void TestLog()
    {
      Assert.AreEqual(0, mathclass.Log(1)); // log(1) = 0
      Assert.AreEqual(0.3010299957, Math.Round(mathclass.Log(2)), 10);  //log(2) ~ 0.3010299957
      Assert.AreEqual(2.9995654882, Math.Round(mathclass.Log(999)), 10);  //log(999) ~ 2.9995654882
      Assert.AreEqual(-0.5228787453, Math.Round(mathclass.Log(0.3)), 10); //log(0.3) ~ -0.5228787453

      Assert.IsTrue(double.IsNaN(mathclass.Log(0)));  // log(0) = NaN
      Assert.IsTrue(double.IsNaN(mathclass.Log(-10)));  // log(-10) = NaN
      Assert.IsTrue(double.IsNaN(mathclass.Log(double.NaN))); // log(NaN) = NaN
    }
  }
}
