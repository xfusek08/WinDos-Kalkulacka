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
    //program pro interní testování výpočetní jednotky při vývoji
    //po spustení program vyžaduje zadání číselné soustavy vstupu
    static void Main(string[] args)
    {
      Console.WriteLine("Pozor při zadávání v jiné soustavě než desítkové, nejsou podporovány výrazy, pouze jednotlivá čísla!");
      Console.Write("Enter input number system [2/8/10/16]: ");
      int numsys;
      NumSystem sys;
      int.TryParse(Console.ReadLine(), out numsys);
      switch (numsys)
      {
        case 2:
          sys = NumSystem.Bin;
          break;
        case 8:
          sys = NumSystem.Oct;
          break;
        case 10:
          sys = NumSystem.Dec;
          break;
        case 16:
          sys = NumSystem.Hex;
          break;
        default:
          sys = NumSystem.Dec;
          break;
      }
      Console.WriteLine("Zvolena soustava {0}", sys.ToString());
      do
      {
        Console.Write("Enter expresion: ");
        string expr = Console.ReadLine();
        if(sys != NumSystem.Dec)
        {
          expr = NumberConverter.ToDouble(expr, sys).ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        Calculation calculation = new Calculation(expr);
        Console.WriteLine("Result: {0}", calculation.Value);
        Console.WriteLine("GetAsString <Dec>: {0}", NumberConverter.ToString(calculation.Value, NumSystem.Dec, ""));
        Console.WriteLine("GetAsString <Bin>: {0}", NumberConverter.ToString(calculation.Value, NumSystem.Bin, ""));
        Console.WriteLine("GetAsString <Oct>: {0}", NumberConverter.ToString(calculation.Value, NumSystem.Oct, ""));
        Console.WriteLine("GetAsString <Hex>: {0}", NumberConverter.ToString(calculation.Value, NumSystem.Hex, ""));
      } while (true);
    }
  }
}
