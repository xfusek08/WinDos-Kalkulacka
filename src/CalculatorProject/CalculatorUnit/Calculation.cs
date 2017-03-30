using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorUnit
{
  /// <summary>
  /// Výpočetní jednotka
  /// </summary>
  public class Calculation
  {
    public double Value { get; set; }
    public string Expresion { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorSubExpr { get; set; }
    public CalcErrorType ErrorType { get; set; }

    public Calculation(string expr)
    {
    }

    public string GetAsString(NumSystem numbase, string format)
    {
      return "";
    }
  }

  public enum CalcErrorType
  {
    None, FuncDomainError, DataTypeOwerflow, ExprFormatError, UnknownError
  }

  public enum NumSystem
  {
    Dec, Bin, Hex, Oct
  }
}
