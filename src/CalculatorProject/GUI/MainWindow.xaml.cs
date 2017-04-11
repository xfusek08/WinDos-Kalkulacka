/*******************************************************************
* Název projektu: GUI
* Balíček: GUI
* Soubor: MainWindow.xaml.cs
* Datum: 05.04.2017
* Autor: Radim Blaha
* Naposledy upravil: Radim Blaha
* Datum poslední změny: 07.04.2017
*
* Popis: Třída, která ovládá grafické prvky hlavního okna aplikace a
*        reaguje na příchozí uživatelské události.
*
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private int leftBracketsCount = 0;
    private int rightBracketsCount = 0;
    private bool dotFlag = true; //příznak, operátoru - využívá se při výpisu desetinných teček (čárek)
    private string unicodePlus = "\u002B";
    private string unicodeMinus = "\u2212";
    private string unicodeMultiply = "\u00D7";
    private string unicodeDivision = "\u00F7";

    public MainWindow()
    {
      InitializeComponent();
    }

    private void printNumber(byte number)
    {
      string lastChar = getLastChar();

      if (tbExpression.Text == "0")  //V případě, že je obsah oblast pro výraz rovna "0", tak...
      {
        tbExpression.Text = "";  //...smaže oblast pro výraz
      }
      else if (lastChar == ")" || lastChar == "!")  //jinak když je poslední znak závorka...
      {
        tbExpression.Text = tbExpression.Text + unicodeMultiply;  //připojí na konec *
        dotFlag = true;
      }

      switch (number)
      {
        case 0:
          if (tbExpression.Text != "0")
          {
            tbExpression.Text = tbExpression.Text + "0";
          }
          break;
        case 1:
          tbExpression.Text = tbExpression.Text + "1";
          break;
        case 2:
          tbExpression.Text = tbExpression.Text + "2";
          break;
        case 3:
          tbExpression.Text = tbExpression.Text + "3";
          break;
        case 4:
          tbExpression.Text = tbExpression.Text + "4";
          break;
        case 5:
          tbExpression.Text = tbExpression.Text + "5";
          break;
        case 6:
          tbExpression.Text = tbExpression.Text + "6";
          break;
        case 7:
          tbExpression.Text = tbExpression.Text + "7";
          break;
        case 8:
          tbExpression.Text = tbExpression.Text + "8";
          break;
        case 9:
          tbExpression.Text = tbExpression.Text + "9";
          break;
      }
    }

    private void printMathOperator(string mathOperator)
    {
      string lastChar = getLastChar(); //původní poslední znak
      string mountingChar = lastChar; //znak pro připojení k řetězci
      string secondCharFromRight = ""; //proměnná pro předposlední znak

      if (tbExpression.Text.Length > 1)
      {
        secondCharFromRight = tbExpression.Text.Substring(tbExpression.Text.Length - 2, 1); //předposlední znak
      }
      
      //Smaže poslední znak a uloží nový poslední znak v případě, že nějaký operátor už na posledním místě výrazu existuje
      if ((lastChar == unicodePlus) || (lastChar == unicodeMinus) || (lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%"))
      {
        tbExpression.Text = tbExpression.Text.Substring(0, tbExpression.Text.Length - 1);
        lastChar = secondCharFromRight;
      }else if ((lastChar != unicodePlus) || (lastChar != unicodeMinus) || (lastChar != unicodeMultiply) || (lastChar != unicodeDivision) || (lastChar != "%")) //pokud není poslední znak operátor
      {
        mountingChar = "";  //vymaž přípojný znak
      }

      if (lastChar != ",") //pokud není poslední znak čárka
      {
        if(lastChar == "(" && (mathOperator == unicodePlus || mathOperator == unicodeMinus))
        {
          tbExpression.Text = tbExpression.Text + mathOperator;
          dotFlag = true;
        }
        else if (lastChar == "(" && (mathOperator == unicodeMultiply || mathOperator == unicodeDivision || mathOperator == "%"))
        {
          tbExpression.Text = tbExpression.Text + mountingChar;
        }
        else if (lastChar != "(" && (mathOperator == unicodeMultiply || mathOperator == unicodeDivision || mathOperator == "%"))
        {
          tbExpression.Text = tbExpression.Text + mathOperator;
          dotFlag = true;
        }
        else
        {
          tbExpression.Text = tbExpression.Text + mathOperator;
          dotFlag = true;
        }
      }
    }

    private void printDot()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      if (dotFlag && isLastCharNumber)  //Pokud v oblasti pro výpočty není čárka a současně je poslední znak číslo, tak...
      {
        tbExpression.Text = tbExpression.Text + ",";  //...vytiskne desetinnou čárku
        dotFlag = false;
      }
      else if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%")  //Pokud je poslední znak operátor, tak...
      {
        tbExpression.Text = tbExpression.Text + "0,";  //...vytiskne nulu a desetinnou čárku
        dotFlag = false;
      }
    }

    private void printLeftBracket()
    {
      string lastChar = "";
      bool isLastCharNumber = false;

      if (tbExpression.Text == "0")  //V případě, že je oblast pro výraz rovna "0", tak...
      {
        tbExpression.Text = "";  //...smaže oblast pro výraz
      }

      //zjistí poslední znak a to, zda se jedná o číslo
      lastChar = getLastChar();
      isLastCharNumber = isCharNumber(lastChar);

      if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%" || tbExpression.Text == "" || lastChar == "(")
      {
        tbExpression.Text = tbExpression.Text + "(";
        leftBracketsCount++;
      }
      else if (lastChar == ")" || isLastCharNumber || lastChar == "!")
      {
        tbExpression.Text = tbExpression.Text + unicodeMultiply + "(";  //připojí na konec *(
        leftBracketsCount++;
        dotFlag = true;
      }

      resetBracketsCounterIfEqual();
    }

    private void printRightBracket()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      if ((isLastCharNumber || lastChar == ")" || lastChar == "!") && (tbExpression.Text != "0") && (leftBracketsCount > rightBracketsCount))
      {
        tbExpression.Text = tbExpression.Text + ")";
        rightBracketsCount++;
      }
      else if (((lastChar == unicodePlus) || (lastChar == unicodeMinus)) && (leftBracketsCount > rightBracketsCount))
      {
        tbExpression.Text = tbExpression.Text + "0)";
        rightBracketsCount++;
      }
      else if (((lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%")) && (leftBracketsCount > rightBracketsCount))
      {
        tbExpression.Text = tbExpression.Text + "1)";
        rightBracketsCount++;
      }

      resetBracketsCounterIfEqual();
    }

    private void printLog()
    {
      string lastChar = "";
      bool isLastCharNumber = false;

      if (tbExpression.Text == "0")  //V případě, že je oblast pro výraz rovna "0", tak...
      {
        tbExpression.Text = "";  //...smaže oblast pro výraz
      }

      //zjistí poslední znak a to, zda se jedná o číslo
      lastChar = getLastChar();
      isLastCharNumber = isCharNumber(lastChar);

      if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%" || tbExpression.Text == "" || lastChar == "(")
      {
        tbExpression.Text = tbExpression.Text + "log(";
        leftBracketsCount++;
      }
      else if (lastChar == ")" || isLastCharNumber || lastChar == "!")
      {
        tbExpression.Text = tbExpression.Text + unicodeMultiply + "log(";  //připojí na konec *log(
        leftBracketsCount++;
        dotFlag = true;
      }

      resetBracketsCounterIfEqual();
    }

    private void printFact()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      if ((lastChar == ")") || isLastCharNumber)
      {
        tbExpression.Text += "!";
      }
    }

    private void printRoot()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      if (isLastCharNumber || lastChar == "!" || lastChar == ")")
      {
        tbExpression.Text += "\u221A";
        dotFlag = true;
      }
    }

    private void printPow()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      if (isLastCharNumber || lastChar == "!" || lastChar == ")")
      {
        tbExpression.Text += "^";
        dotFlag = true;
      }
    }


    //===== Pomocné funkce =====
    private string getLastChar()
    {
      if (tbExpression.Text.Length > 0)
      {
        return tbExpression.Text.Substring(tbExpression.Text.Length - 1, 1);
      }

      return "";
    }

    private bool isCharNumber(string c)
    {
      int num;
      return int.TryParse(c, out num);
    }

    private void resetBracketsCounterIfEqual()
    {
      if (leftBracketsCount == rightBracketsCount)
      {
        leftBracketsCount = 0;
        rightBracketsCount = 0;
      }
    }

    private void closeBrackets()
    {
      while (leftBracketsCount > 0)
      {
        tbExpression.Text = tbExpression.Text + ")";
        leftBracketsCount--;
      }
    }

    //===== Události spouštějící se při najetí a opuštění tlačítka odmocniny =====
    private void grdRootBtn_MouseEnter(object sender, MouseEventArgs e)
    {
      btnRoot.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x5C, 0x6b, 0xC0));
    }

    private void grdRootBtn_MouseLeave(object sender, MouseEventArgs e)
    {
      btnRoot.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3F, 0x51, 0xB5));
    }

    private void btnRoot_MouseEnter(object sender, MouseEventArgs e)
    {
      btnRoot.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x5C, 0x6b, 0xC0));
    }

    private void btnRoot_MouseLeave(object sender, MouseEventArgs e)
    {
      btnRoot.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3F, 0x51, 0xB5));
    }

    //===== Události spouštějící se při najetí a opuštění tlačítka mocniny =====
    private void grdPowBtn_MouseEnter(object sender, MouseEventArgs e)
    {
      btnPow.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x5C, 0x6b, 0xC0));
    }

    private void grdPowBtn_MouseLeave(object sender, MouseEventArgs e)
    {
      btnPow.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3F, 0x51, 0xB5));
    }

    private void btnPow_MouseEnter(object sender, MouseEventArgs e)
    {
      btnPow.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x5C, 0x6b, 0xC0));
    }

    private void btnPow_MouseLeave(object sender, MouseEventArgs e)
    {
      btnPow.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x3F, 0x51, 0xB5));
    }

    //===== Události spouštějící se při kliknutí na tlačítka 0-9 =====
    private void btnZero_Click(object sender, RoutedEventArgs e)
    {
      printNumber(0);
    }

    private void btnOne_Click(object sender, RoutedEventArgs e)
    {
      printNumber(1);
    }

    private void btnTwo_Click(object sender, RoutedEventArgs e)
    {
      printNumber(2);
    }

    private void btnThree_Click(object sender, RoutedEventArgs e)
    {
      printNumber(3);
    }

    private void btnFour_Click(object sender, RoutedEventArgs e)
    {
      printNumber(4);
    }

    private void btnFive_Click(object sender, RoutedEventArgs e)
    {
      printNumber(5);
    }

    private void btnSix_Click(object sender, RoutedEventArgs e)
    {
      printNumber(6);
    }

    private void btnSeven_Click(object sender, RoutedEventArgs e)
    {
      printNumber(7);
    }

    private void btnEight_Click(object sender, RoutedEventArgs e)
    {
      printNumber(8);
    }

    private void btnNine_Click(object sender, RoutedEventArgs e)
    {
      printNumber(9);
    }

    //===== Události spouštějící se při kliknutí na tlačítko desetinné čárky (tečky) =====
    private void btnDot_Click(object sender, RoutedEventArgs e)
    {
      printDot();
    }

    //===== Události spouštějící se při kliknutí na tlačítka závorek =====
    private void btnLeftBracket_Click(object sender, RoutedEventArgs e)
    {
      printLeftBracket();
    }

    private void btnRightBracket_Click(object sender, RoutedEventArgs e)
    {
      printRightBracket();
    }

    private void btnMultiply_Click(object sender, RoutedEventArgs e)
    {
      printMathOperator(unicodeMultiply);
    }

    private void btnDivide_Click(object sender, RoutedEventArgs e)
    {
      printMathOperator(unicodeDivision);
    }

    private void btnPlus_Click(object sender, RoutedEventArgs e)
    {
      printMathOperator(unicodePlus);
    }

    private void btnMinus_Click(object sender, RoutedEventArgs e)
    {
      printMathOperator(unicodeMinus);
    }

    private void btnPow_Click(object sender, RoutedEventArgs e)
    {
      printPow();
    }

    private void grdPowBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      printPow();
    }

    private void btnRoot_Click(object sender, RoutedEventArgs e)
    {
      printRoot();
    }

    private void lblRootBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      printRoot();
    }

    private void btnFact_Click(object sender, RoutedEventArgs e)
    {
      printFact();
    }

    private void btnLog_Click(object sender, RoutedEventArgs e)
    {
      printLog();
    }

    private void btnMod_Click(object sender, RoutedEventArgs e)
    {
      printMathOperator("%");
    }

    private void btnDel_Click(object sender, RoutedEventArgs e)
    {

    }

    private void btnAc_Click(object sender, RoutedEventArgs e)
    { 
      tbExpression.Text = "0";
      leftBracketsCount = 0;
      rightBracketsCount = 0;
      dotFlag = true;
    }

    private void btnCount_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
