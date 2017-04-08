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
        Console.WriteLine("GetAsString <Dec>: {0}", calculation.GetAsString(NumSystem.Dec, ""));
        Console.WriteLine("GetAsString <Bin>: {0}", calculation.GetAsString(NumSystem.Bin, ""));
        Console.WriteLine("GetAsString <Oct>: {0}", calculation.GetAsString(NumSystem.Oct, ""));
        Console.WriteLine("GetAsString <Hex>: {0}", calculation.GetAsString(NumSystem.Hex, ""));
      } while (true);
    }
  }
}
