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
    private int leftBracketCount = 0;
    private int rightBracketCount = 0;
    string unicodePlus = "\u002B";
    string unicodeMinus = "\u2212";
    string unicodeMultiply = "\u00D7";
    string unicodeDivision = "\u00F7";

    public MainWindow()
    {
      InitializeComponent();
    }

    private void printNumber(byte number)
    {
      string lastChar = tbCalculation.Text.Substring(tbCalculation.Text.Length - 1, 1);

      if (tbCalculation.Text == "0")  //V případě, že je obsah oblast pro výraz rovna "0", tak...
      {
        tbCalculation.Text = "";  //...smaže oblast pro výraz
      }else if (lastChar == ")")  //jinak když je poslední znak závorka...
      {
        tbCalculation.Text = tbCalculation.Text + unicodeMultiply;  //připojí na konec *
      }

      switch (number)
      {
        case 0:
          if (tbCalculation.Text != "0")
          {
            tbCalculation.Text = tbCalculation.Text + "0";
          }
          break;
        case 1:
          tbCalculation.Text = tbCalculation.Text + "1";
          break;
        case 2:
          tbCalculation.Text = tbCalculation.Text + "2";
          break;
        case 3:
          tbCalculation.Text = tbCalculation.Text + "3";
          break;
        case 4:
          tbCalculation.Text = tbCalculation.Text + "4";
          break;
        case 5:
          tbCalculation.Text = tbCalculation.Text + "5";
          break;
        case 6:
          tbCalculation.Text = tbCalculation.Text + "6";
          break;
        case 7:
          tbCalculation.Text = tbCalculation.Text + "7";
          break;
        case 8:
          tbCalculation.Text = tbCalculation.Text + "8";
          break;
        case 9:
          tbCalculation.Text = tbCalculation.Text + "9";
          break;
      }
    }

    private void printMathOperator(string mathOperator)
    {
      string lastChar = tbCalculation.Text.Substring(tbCalculation.Text.Length - 1, 1); //původní poslední znak
      string mountingChar = lastChar; //znak pro připojení k řetězci
      string secondCharFromRight = tbCalculation.Text.Substring(tbCalculation.Text.Length - 2, 1); //předposlední znak
      
      //Smaže poslední znak a uloží nový poslední znak v případě, že nějaký operátor už na posledním místě výrazu existuje
      if ((lastChar == unicodePlus) || (lastChar == unicodeMinus) || (lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%"))
      {
        tbCalculation.Text = tbCalculation.Text.Substring(0, tbCalculation.Text.Length - 1);
        lastChar = secondCharFromRight;
      }else if ((lastChar != unicodePlus) || (lastChar != unicodeMinus) || (lastChar != unicodeMultiply) || (lastChar != unicodeDivision) || (lastChar != "%")) //pokud není poslední znak operátor
      {
        mountingChar = "";  //vymaž přípojný znak
      }

      if (lastChar != ",") //pokud není poslední znak čárka
      {
        if(lastChar == "(" && (mathOperator == unicodePlus || mathOperator == unicodeMinus))
        {
          tbCalculation.Text = tbCalculation.Text + mathOperator;
        }else if (lastChar == "(" && (mathOperator == unicodeMultiply || mathOperator == unicodeDivision || mathOperator == "%"))
        {
          tbCalculation.Text = tbCalculation.Text + mountingChar;
        }else if (lastChar != "(" && (mathOperator == unicodeMultiply || mathOperator == unicodeDivision || mathOperator == "%"))
        {
          tbCalculation.Text = tbCalculation.Text + mathOperator;
        }else
        {
          tbCalculation.Text = tbCalculation.Text + mathOperator;
        }
      }
    }

    private void printDot()
    {
      int num;
      string lastChar = tbCalculation.Text.Substring(tbCalculation.Text.Length - 1, 1);

      //Pokud v oblasti pro výpočty není čárka a současně je poslední znak číslo, tak...
      if (!tbCalculation.Text.Contains(",") && int.TryParse(lastChar, out num))
      {
        tbCalculation.Text = tbCalculation.Text + ",";  //...vytiskne desetinnou čárku
      }
    }

    private void printLeftBracket()
    {
      int num;
      string lastChar = "";
      bool isLastCharNumber = false;

      if (tbCalculation.Text == "0")  //V případě, že je obsah oblast pro výraz rovna "0", tak...
      {
        tbCalculation.Text = "";  //...smaže oblast pro výraz
      }

      //Pokud není oblast pro výraz prázdná, tak zjistí poslední znak a to, zda se jedná o číslo
      if (tbCalculation.Text.Length != 0)
      {
        lastChar = tbCalculation.Text.Substring(tbCalculation.Text.Length - 1, 1);
        isLastCharNumber = int.TryParse(lastChar, out num);
      }

      if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%" || tbCalculation.Text == "" || lastChar == "(")
      {
        tbCalculation.Text = tbCalculation.Text + "(";
        leftBracketCount++;
      }else if (lastChar == ")" || isLastCharNumber || lastChar == "!")
      {
        tbCalculation.Text = tbCalculation.Text + unicodeMultiply + "(";  //připojí na konec *(
        leftBracketCount++;
      }

      resetBracketsCounter();
    }

    private void printRightBracket()
    {
      int num;
      string lastChar = "";
      bool isLastCharNumber = false;

      //Pokud není oblast pro výraz prázdná, tak zjistí poslední znak a to, zda se jedná o číslo
      if (tbCalculation.Text.Length != 0)
      {
        lastChar = tbCalculation.Text.Substring(tbCalculation.Text.Length - 1, 1);
        isLastCharNumber = int.TryParse(lastChar, out num);
      }

      if ((isLastCharNumber || lastChar == ")") && (tbCalculation.Text != "0") && (leftBracketCount > rightBracketCount))
      {
        tbCalculation.Text = tbCalculation.Text + ")";
        rightBracketCount++;
      }else if (((lastChar == unicodePlus) || (lastChar == unicodeMinus) || (lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%")) && (leftBracketCount > rightBracketCount))
      {
        tbCalculation.Text = tbCalculation.Text + "0)";
        rightBracketCount++;
      }

      resetBracketsCounter();
    }

    //===== Pomocné funkce =====
    private void resetBracketsCounter()
    {
      if (leftBracketCount == rightBracketCount)
      {
        leftBracketCount = 0;
        rightBracketCount = 0;
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

    }

    private void grdPowBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {

    }

    private void btnRoot_Click(object sender, RoutedEventArgs e)
    {

    }

    private void lblRootBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {

    }

    private void btnFact_Click(object sender, RoutedEventArgs e)
    {

    }

    private void btnLog_Click(object sender, RoutedEventArgs e)
    {

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
      tbCalculation.Text = "0";
    }

    private void btnCount_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}
