using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
  /// <summary>
  /// Matematická knihovna ... doplnit komentáře - mock - bude nahrazeno skutečnou knihovnou
  /// </summary>
  public class CalcMath
  {
    // Sečte hodnoty 'a' a 'b'
    public double Add(double a, double b)
    {
      return a + b;
    }

    // Odečte hodnoty 'a' a 'b'
    public double Subtract(double a, double b)
    {
      return a - b;
    }

    // Vydělí hodnoty 'a' a 'b'
    public double Divide(double a, double b)
    {
      return a / b;
    }

    // Vynásobí hodnoty 'a' a 'b'
    public double Multipy(double a, double b)
    {
      return a * b;
    }

    // Umocní 'x' na 'y'
    public double Pow(double x, double y)
    {
      return Math.Pow(x,y);
    }

    // Umocní 'x' na 'y'
    public double Root(double a, double x)
    {
      return Math.Pow(a,1/x);
    }

    // Vypočte faktorial z 'x'
    public double Fact(Byte x)
    {
      double res = 1;
      for (int i = 1; i <= x; i++)
      {
        res = res * i;
      }
      return res;
    }

    // Najde zbytek po dělení 'a' a 'b'
    public double Modulo(double a, double b)
    {
      return a % b;
    }

    // Vypočte logaritmus z 'x' o základu 10
    public double Log(double x)
    {
      return Math.Log10(x);
    }
  }
}
