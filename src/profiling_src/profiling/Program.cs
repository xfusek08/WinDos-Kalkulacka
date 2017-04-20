using System;
using MathLib;

namespace profiling
{
  class Program
  {
    static void Main(string[] args)
    {
      CalcMath Math = new CalcMath();
      string input = Console.ReadLine();
      while (input != null && input != "")
      {
        int i = 0;
        string stringNumber = "";
        double[] number = new double[1000];
        int position = 0;
        //Zpracování vstupu
        while (i < input.Length)
        {
          char character = input[i];
          if (!char.IsDigit(character) && !char.IsWhiteSpace(character))
          {
            Console.WriteLine("Špatný vstup.");
            Console.ReadKey(true);
            Environment.Exit(0);
          }
          if (char.IsDigit(character))
            stringNumber += character;
          if ((char.IsWhiteSpace(character) || input.Length == (1 + i)) && stringNumber != "" )
          {
            number[position] = double.Parse(stringNumber);
            position++;
            stringNumber = "";
          }
          i++;
        }
        //Směrodatná odchylka
        double average = 0;
        for (int n = 0; n < position; n++)
        {
          average = Math.Add(average, number[n]);
        }
        average = Math.Divide(average, position);
        double sum = 0;
        for (int n = 0; n < position; n++)
        {
          number[n] = Math.Multipy(Math.Subtract(number[n], average), Math.Subtract(number[n], average));
          sum = Math.Add(sum, number[n]);
        }
        double result = Math.Divide(sum, position);
        result = Math.Root(result, 2);
        Console.WriteLine(result.ToString());
        input = Console.ReadLine();
      }
    }
  }
}
