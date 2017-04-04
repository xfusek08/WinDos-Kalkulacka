using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorUnit;

namespace CalcUnitParser
{
  class Program
  {
    static void Main(string[] args)
    {
      do
      {
        Console.Write("Expresion: ");
        string expr = Console.ReadLine();

        Calculation calculation = new Calculation(expr);
        Console.WriteLine("Result: {0}", calculation.Value);
      } while (true);
    }
  }
}
