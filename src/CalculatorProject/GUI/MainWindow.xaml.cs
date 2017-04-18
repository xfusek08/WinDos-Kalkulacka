/*******************************************************************
* Název projektu: IVS-Kalkulačka
* Balíček: GUI
* Soubor: MainWindow.xaml.cs
* Datum: 05.04.2017
* Autor: Radim Blaha
* Naposledy upravil: Radim Blaha
* Datum poslední změny: 18.04.2017
*
* Popis: Třída, která ovládá grafické prvky hlavního okna aplikace a
*        reaguje na příchozí uživatelské události.
*
*****************************************************************/

/**
 * @brief Grafické uživatelské rozhraní
 * @file MainWindow.xaml.cs
 * @author Radim Blaha
 * @date 5.04.2017
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CalculatorUnit;

namespace GUI
{
  /// <summary>
  /// Interakční logika pro MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private int leftBracketsCount = 0;
    private int rightBracketsCount = 0;
    //příznak desetinné tečky - využívá se při výpisu desetinných teček (čárek) --> true = může se vypsat tečka; false = není možné vypsat tečku
    private bool isDotPrintable = true;
    private string unicodePlus = "\u002B";
    private string unicodeMinus = "\u2212";
    private string unicodeMultiply = "\u00D7";
    private string unicodeDivision = "\u00F7";
    private string unicodeRoot = "\u221A";
    private NumSystem currentNumSys = NumSystem.Dec; //výchozí číselná soustava je desítková
    private string lastResult;  //Proměnná pro uložení posledního výsledku

    public MainWindow()
    {
      InitializeComponent();
      this.Title = "Kalkulačka";
      switchNumeralSystem(0); //spuštění v režimu desítkové soustavy
      enable_disableButtons();
    }

    //===== Funkce pro tisk znaků =====

    /// <summary>
    /// Funkce, která tiskne znaky
    /// </summary>
    /// <param name="number">Znak pro vytisknutí.</param>
    private void printNumber(byte number)
    {
      string lastChar = getLastChar();

      //Pokud existuje výsledek a zadává se číslo, tak se kalkulačka vymaže
      if (tbResult.Text.Length > 0)
      {
        resetCalc(true);
      }

      if (tbExpression.Text == "0")  //V případě, že je obsah oblast pro výraz rovna "0", tak...
      {
        tbExpression.Text = "";  //...smaže oblast pro výraz
      }/*
      else if (lastChar == ")" || lastChar == "!")  //jinak když je poslední znak závorka...
      {
        isDotPrintable = true;
        tbExpression.Text = tbExpression.Text + unicodeMultiply;  //připojí na konec *
      }*/

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
        case 10:
          tbExpression.Text = tbExpression.Text + "A";
          break;
        case 11:
          tbExpression.Text = tbExpression.Text + "B";
          break;
        case 12:
          tbExpression.Text = tbExpression.Text + "C";
          break;
        case 13:
          tbExpression.Text = tbExpression.Text + "D";
          break;
        case 14:
          tbExpression.Text = tbExpression.Text + "E";
          break;
        case 15:
          tbExpression.Text = tbExpression.Text + "F";
          break;
      }
    }

    /// <summary>
    /// Funkce, která tiskne matematický operátor
    /// </summary>
    /// <param name="mathOperator">Matematický operátor, který se má vytisknout.</param>
    private void printMathOperator(string mathOperator)
    {
      string lastChar = getLastChar(); //původní poslední znak

      //Smaže poslední znak v případě, že nějaký operátor už na posledním místě výrazu existuje
      if ((lastChar == unicodePlus) || (lastChar == unicodeMinus) || (lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%"))
      {
        removeFromBack();
      }
      isDotPrintable = true;
      tbExpression.Text = tbExpression.Text + mathOperator; //vytiskne operátor
    }

    /// <summary>
    /// Funkce, která tiskne desetinnou tečku, pokud je to možné
    /// </summary>
    private void printDot()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      //Pokud existuje výsledek, tak se kalkulačka vymaže
      if (tbResult.Text.Length > 0)
      {
        resetCalc(true);
      }

      if (isDotPrintable && isLastCharNumber)  //Pokud v oblasti pro výpočty není čárka a současně je poslední znak číslo, tak...
      {
        isDotPrintable = false;
        tbExpression.Text = tbExpression.Text + ".";  //...vytiskne desetinnou čárku
      }
      else if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%")  //Pokud je poslední znak operátor, tak...
      {
        isDotPrintable = false;
        tbExpression.Text = tbExpression.Text + "0.";  //...vytiskne nulu a desetinnou čárku
      }
    }

    /// <summary>
    /// Funkce, která tiskne levou závorku
    /// </summary>
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

      if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%" || tbExpression.Text == "" || lastChar == "(" || lastChar == unicodeRoot || lastChar == "^")
      { //vytiskne pouze levou závorku
        leftBracketsCount++;
        tbExpression.Text = tbExpression.Text + "(";
      }
      else if (lastChar == ")" || isLastCharNumber || lastChar == "!")
      { //vytiskne * a levou závorku
        leftBracketsCount++;
        isDotPrintable = true;
        tbExpression.Text = tbExpression.Text + unicodeMultiply + "(";  //připojí na konec *(
      }

      resetBracketsCounterIfEqual();
    }

    /// <summary>
    /// Funkce, která tiskne pravou závorku
    /// </summary>
    private void printRightBracket()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);

      if ((isLastCharNumber || lastChar == ")" || lastChar == "!") && (tbExpression.Text != "0"))
      { //vytiskne pouze závorku
        rightBracketsCount++;
        tbExpression.Text = tbExpression.Text + ")";
      }
      else if (((lastChar == unicodePlus) || (lastChar == unicodeMinus) || (lastChar == "(")))
      { //vytiskne 0 a závorku
        rightBracketsCount++;
        tbExpression.Text = tbExpression.Text + "0)";
      }
      else if (((lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%")))
      { //vytiskne 1 a závorku
        rightBracketsCount++;
        tbExpression.Text = tbExpression.Text + "1)";
      }

      resetBracketsCounterIfEqual();
    }

    /// <summary>
    /// Funkce, která tiskne logaritmus
    /// </summary>
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

      if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%" || lastChar == unicodeRoot || lastChar == "^" || tbExpression.Text == "" || lastChar == "(")
      { //tiskne log(
        leftBracketsCount++;
        tbExpression.Text = tbExpression.Text + "log(";
      }
      else if (lastChar == ")" || isLastCharNumber || lastChar == "!")
      { //tiskne *log(
        leftBracketsCount++;
        isDotPrintable = true;
        tbExpression.Text = tbExpression.Text + unicodeMultiply + "log(";  //připojí na konec *log(
      }

      resetBracketsCounterIfEqual();
    }

    /// <summary>
    /// Funkce, která vytiskne faktoriál
    /// </summary>
    private void printFact()
    {
      tbExpression.Text += "!";
    }

    /// <summary>
    /// Funkce, která tiskne mocninu a nastavuje příznak desetinné tečky
    /// </summary>
    private void printRoot()
    {
      isDotPrintable = true;
      tbExpression.Text += unicodeRoot;
    }

    /// <summary>
    /// Funkce, která tiskne odmocninu a nastavuje příznak desetinné tečky
    /// </summary>
    private void printPow()
    {
      isDotPrintable = true;
      tbExpression.Text += "^";
    }

    //===== Pomocné funkce =====
    /// <summary>
    /// Funkce, která vrací poslední znak v oblasti pro výraz
    /// </summary>
    /// <returns>Poslední znak v oblasti pro výraz.</returns>
    private string getLastChar()
    {
      if (tbExpression.Text.Length > 0)
      {
        return tbExpression.Text.Substring(tbExpression.Text.Length - 1, 1);
      }

      return "";
    }

    /// <summary>
    /// Funkce, která zjišťuje, zda je znak číslo
    /// </summary>
    /// <param name="c">Znak pro test konverze na cislo</param>
    /// <returns>Vrací true, když je znak číslo a false, když znak není číslo.</returns>
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

    /// <summary>
    /// Funkce, která správně ukončuje výraz
    /// </summary>
    /// <description>
    /// Podle počtu otevíracích závorek doplní počet závorek uzavíracích.
    /// V případě, že je na konci operátor, vhodně výraz doplní.
    /// </description>
    private void closeExpr()
    {
      string lastChar = getLastChar();

      //Doplnění nuly, když je poslední znak "("
      if (lastChar == "(" || lastChar == unicodePlus || lastChar == unicodeMinus)
      {
        tbExpression.Text = tbExpression.Text + "0";
      }
      else if (lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%" || lastChar == unicodeRoot || lastChar == "^")
      {
        tbExpression.Text = tbExpression.Text + "1";
      }

      //Uzavírá závorky
      while (rightBracketsCount < leftBracketsCount)
      {
        rightBracketsCount++;
        tbExpression.Text = tbExpression.Text + ")";
      }
    }

    /// <summary>
    /// Funkce, která vypočítá výraz
    /// </summary>
    /// <description>
    /// Založí instanci objektu Calculation, která za pomoci NumberConvertor vypočítá výraz a hodnotu uloží
    /// do textboxu určeného pro výsledek.
    /// </description>
    private void countExpr()
    {
      Calculation calc;
      string expr;

      closeExpr();  //uzavření závorek, aby nevznikla chyba

      expr = formatExpr(); //správně zformátuje řetezec pro použití ve výpočetní jednotce

      //Pokud se nejedná o desítkovou soustavu, tak zkonvertuje na desítkovou
      if (currentNumSys != NumSystem.Dec)
      {
        expr = NumberConverter.ToString(NumberConverter.ToDouble(expr, currentNumSys), NumSystem.Dec, "");
      }

      calc = new Calculation(expr);

      switch (calc.ErrorType)
      {
        case CalcErrorType.None:
          lastResult = NumberConverter.ToString(calc.Value, currentNumSys, "");
          tbResult.Text = lastResult;
          break;
        case CalcErrorType.FuncDomainError:
          tbResult.Text = "Chybný definiční obor funkce.";
          break;
        case CalcErrorType.DataTypeOverflow:
          tbResult.Text = "Přetečení.";
          break;
        case CalcErrorType.ExprFormatError:
          tbResult.Text = "Chybný výraz.";
          break;
        case CalcErrorType.UnknownError:
          tbResult.Text = "Nespecifikovaná chyba.";
          break;
      }

      //Po následném zmáčknutí desetinné tečky se výraz maže - povolení zmáčkout tečku a zadat "0."
      isDotPrintable = true;
      enable_disableButtons();
    }

    //===== Ostatní funkce =====

    /// <summary>
    /// Funkce odstraňující znaky od konce
    /// </summary>
    /// <description>
    /// Odstraňuje znaky z konce a zároveň nastavuje příznak desetinné tečky a mění proměnnou početu závorek,
    /// kvůli korektnímu opakovanému tisku znaků.
    /// </description>
    private void removeFromBack()
    {
      string lastChar = getLastChar();

      if (tbExpression.Text.Length > 3)
      {
        if (tbExpression.Text.Substring(tbExpression.Text.Length - 4, 4) == "log(")
        {
          tbExpression.Text = tbExpression.Text.Remove(tbExpression.Text.Length - 3); // !!!čtvrtý znak se smaže ve funkci později!!!
        }
      }

      //Když je mazaný znak levá závorka, tak sníží počet levých závorek
      if (lastChar == "(")
        leftBracketsCount--;
      //Když je mazaný znak pravá závorka, tak sníží počet pravých závorek
      if (lastChar == ")")
        rightBracketsCount--;
      //Když je mazaný znak +,-,*,/,%, odmocnina nebo mocnina, tak nastaví příznak desetinné čárky na false
      if ((lastChar == unicodePlus) || (lastChar == unicodeMinus) || (lastChar == unicodeMultiply) || (lastChar == unicodeDivision) || (lastChar == "%") || (lastChar == unicodeRoot) || (lastChar == "^"))
        isDotPrintable = false;
      //Když je mazaný znak desetinná čárka, tak nastaví příznak desetinné čárky
      if (lastChar == ".")
        isDotPrintable = true;
      tbExpression.Text = tbExpression.Text.Remove(tbExpression.Text.Length - 1);
      if (tbExpression.Text.Length == 0)
      {
        tbExpression.Text += "0";
      }
    }

    /// <summary>
    /// Funkce, která nastavuje kalkulačku do výchozího stavu.
    /// </summary>
    /// <param name="all">Když je true, tak resetuje i oblast pro výsledek.</param>
    private void resetCalc(bool all)
    {
      isDotPrintable = true;
      leftBracketsCount = 0;
      rightBracketsCount = 0;
      tbExpression.Text = "0";
      if (all == true)
        resetResult();
    }

    /// <summary>
    /// Funkce, která nuluje oblast pro výsledek
    /// </summary>
    private void resetResult()
    {
      tbResult.Text = "";
    }

    /// <summary>
    /// Funkce pro konverzi výsledku
    /// </summary>
    /// <description>
    /// Pomocí třídy NumberConverter konvertuje výsledek.
    /// </description>
    /// <param name="numSys">Soustava, do které se má převádět.</param>
    private void convertResult(NumSystem numSys)
    {
      if (tbResult.Text != "")  //Pokud má co převádět, tak převede
      {
        lastResult = NumberConverter.ToString(NumberConverter.ToDouble(lastResult, currentNumSys), numSys, "");
        tbResult.Text = lastResult;
      }
    }

    /// <summary>
    /// Funkce, která nahradí unicode znaky +, -, *, / a odmocnina za ASCII znaky a log za L,
    /// z důvodu správného vyhodnocení výpočetní jednotkou.
    /// </summary>
    /// <returns>Vrací řetezec s výrazem ve formátu požadovaném výpočetní jednotkou.</returns>
    private string formatExpr()
    {
      string expr = tbExpression.Text;

      expr = expr.Replace(unicodePlus, "+").Replace(unicodeMinus, "-").Replace(unicodeMultiply, "*").Replace(unicodeDivision, "/");
      expr = expr.Replace(unicodeRoot, "@").Replace("log", "L");

      return expr;
    }

    /// <summary>
    /// Funkce pro ovládání přepínání mezi soustavami
    /// </summary>
    /// <desctiption>
    /// Zakazuje a povoluje tlačítka pro počítání v dané soustavě.
    /// Konvertuje výsledek. Nastavuje aktuální číselnou soustavu.
    /// </desctiption>
    /// <param name="numeralSystem">Vyjadřuje číselnou soustavu (0 - DEC, 1 - BIN, 2 - HEX, 3 - OCT)</param>
    private void switchNumeralSystem(byte numeralSystem)
    {
      resetCalc(false);  //smaže oblast pro výraz při přepnutí do jiné soustavy - aby zde nezůstávali neplatné znaky pro danou soustavu
      switch (numeralSystem)
      {
        case 0: //desítkova soustava
          convertResult(NumSystem.Dec);
          currentNumSys = NumSystem.Dec;  //současná soustava je desítková
          //tlačítka A-F
          btnA.IsEnabled = false;
          btnB.IsEnabled = false;
          btnC.IsEnabled = false;
          btnD.IsEnabled = false;
          btnE.IsEnabled = false;
          btnF.IsEnabled = false;
          //tlačítka 0-9
          btnZero.IsEnabled = true;
          btnOne.IsEnabled = true;
          btnTwo.IsEnabled = true;
          btnThree.IsEnabled = true;
          btnFour.IsEnabled = true;
          btnFive.IsEnabled = true;
          btnSix.IsEnabled = true;
          btnSeven.IsEnabled = true;
          btnEight.IsEnabled = true;
          btnNine.IsEnabled = true;
          //tlačítka závorek
          btnLeftBracket.IsEnabled = true;

          if (leftBracketsCount > rightBracketsCount)
          {
            btnRightBracket.IsEnabled = true;
          }
          else
          {
            btnRightBracket.IsEnabled = false;
          }

          //tačítko desetinné tečka
          btnDot.IsEnabled = true;
          //tlačítka operací
          btnMultiply.IsEnabled = true;
          btnDivide.IsEnabled = true;
          btnPlus.IsEnabled = true;
          btnMinus.IsEnabled = true;
          btnMod.IsEnabled = true;
          //tlačítka faktoriálu a logaritmu
          btnFact.IsEnabled = true;
          btnLog.IsEnabled = true;
          //tlačítka mocniny a odmocniny
          btnPow.IsEnabled = true;
          grdPowBtn.IsEnabled = true;
          lblPowBtn.IsEnabled = true;
          btnRoot.IsEnabled = true;
          grdRootBtn.IsEnabled = true;
          lblRootBtn.IsEnabled = true;
          grdRootBtn.IsEnabled = true;
          //tlačítka DEL, AC a "="
          btnDel.IsEnabled = true;
          btnAc.IsEnabled = true;
          btnCount.IsEnabled = true;
          break;
        case 1: //binarni soustava
          convertResult(NumSystem.Bin);
          currentNumSys = NumSystem.Bin;  //současná soustava je dvojková
          //tlačítka A-F
          btnA.IsEnabled = false;
          btnB.IsEnabled = false;
          btnC.IsEnabled = false;
          btnD.IsEnabled = false;
          btnE.IsEnabled = false;
          btnF.IsEnabled = false;
          //tlačítka 0-9
          btnZero.IsEnabled = true;
          btnOne.IsEnabled = true;
          btnTwo.IsEnabled = false;
          btnThree.IsEnabled = false;
          btnFour.IsEnabled = false;
          btnFive.IsEnabled = false;
          btnSix.IsEnabled = false;
          btnSeven.IsEnabled = false;
          btnEight.IsEnabled = false;
          btnNine.IsEnabled = false;
          //tlačítka závorek
          btnLeftBracket.IsEnabled = false;
          btnRightBracket.IsEnabled = false;
          //tačítko desetinné tečka
          btnDot.IsEnabled = false;
          //tlačítka operací
          btnMultiply.IsEnabled = false;
          btnDivide.IsEnabled = false;
          btnPlus.IsEnabled = false;
          btnMinus.IsEnabled = false;
          btnMod.IsEnabled = false;
          //tlačítka faktoriálu a logaritmu
          btnFact.IsEnabled = false;
          btnLog.IsEnabled = false;
          //tlačítka mocniny a odmocniny
          btnPow.IsEnabled = false;
          grdPowBtn.IsEnabled = false;
          lblPowBtn.IsEnabled = false;
          btnRoot.IsEnabled = false;
          grdRootBtn.IsEnabled = false;
          lblRootBtn.IsEnabled = false;
          //tlačítka DEL, AC a "="
          btnDel.IsEnabled = true;
          btnAc.IsEnabled = true;
          btnCount.IsEnabled = true;
          break;
        case 2: //hexadecimalni soustava
          convertResult(NumSystem.Hex);
          currentNumSys = NumSystem.Hex;  //současná soustava je hexadecimální
          //tlačítka A-F
          btnA.IsEnabled = true;
          btnB.IsEnabled = true;
          btnC.IsEnabled = true;
          btnD.IsEnabled = true;
          btnE.IsEnabled = true;
          btnF.IsEnabled = true;
          //tlačítka 0-9
          btnZero.IsEnabled = true;
          btnOne.IsEnabled = true;
          btnTwo.IsEnabled = true;
          btnThree.IsEnabled = true;
          btnFour.IsEnabled = true;
          btnFive.IsEnabled = true;
          btnSix.IsEnabled = true;
          btnSeven.IsEnabled = true;
          btnEight.IsEnabled = true;
          btnNine.IsEnabled = true;
          //tlačítka závorek
          btnLeftBracket.IsEnabled = false;
          btnRightBracket.IsEnabled = false;
          //tačítko desetinné tečka
          btnDot.IsEnabled = false;
          //tlačítka operací
          btnMultiply.IsEnabled = false;
          btnDivide.IsEnabled = false;
          btnPlus.IsEnabled = false;
          btnMinus.IsEnabled = false;
          btnMod.IsEnabled = false;
          //tlačítka faktoriálu a logaritmu
          btnFact.IsEnabled = false;
          btnLog.IsEnabled = false;
          //tlačítka mocniny a odmocniny
          btnPow.IsEnabled = false;
          grdPowBtn.IsEnabled = false;
          lblPowBtn.IsEnabled = false;
          btnRoot.IsEnabled = false;
          grdRootBtn.IsEnabled = false;
          lblRootBtn.IsEnabled = false;
          //tlačítka DEL, AC a "="
          btnDel.IsEnabled = true;
          btnAc.IsEnabled = true;
          btnCount.IsEnabled = true;
          break;
        case 3: //osmičková soustava
          convertResult(NumSystem.Oct);
          currentNumSys = NumSystem.Oct;  //současná soustava je osmičková
          //tlačítka A-F
          btnA.IsEnabled = false;
          btnB.IsEnabled = false;
          btnC.IsEnabled = false;
          btnD.IsEnabled = false;
          btnE.IsEnabled = false;
          btnF.IsEnabled = false;
          //tlačítka 0-9
          btnZero.IsEnabled = true;
          btnOne.IsEnabled = true;
          btnTwo.IsEnabled = true;
          btnThree.IsEnabled = true;
          btnFour.IsEnabled = true;
          btnFive.IsEnabled = true;
          btnSix.IsEnabled = true;
          btnSeven.IsEnabled = true;
          btnEight.IsEnabled = false;
          btnNine.IsEnabled = false;
          //tlačítka závorek
          btnLeftBracket.IsEnabled = false;
          btnRightBracket.IsEnabled = false;
          //tačítko desetinné tečka
          btnDot.IsEnabled = false;
          //tlačítka operací
          btnMultiply.IsEnabled = false;
          btnDivide.IsEnabled = false;
          btnPlus.IsEnabled = false;
          btnMinus.IsEnabled = false;
          btnMod.IsEnabled = false;
          //tlačítka faktoriálu a logaritmu
          btnFact.IsEnabled = false;
          btnLog.IsEnabled = false;
          //tlačítka mocniny a odmocniny
          btnPow.IsEnabled = false;
          grdPowBtn.IsEnabled = false;
          lblPowBtn.IsEnabled = false;
          btnRoot.IsEnabled = false;
          grdRootBtn.IsEnabled = false;
          lblRootBtn.IsEnabled = false;
          //tlačítka DEL, AC a "="
          btnDel.IsEnabled = true;
          btnAc.IsEnabled = true;
          btnCount.IsEnabled = true;
          break;
      }
    }

    /// <summary>
    /// Funkce, která na základě předchozích zadaných znaků zakazuje a povoluje tlačítka
    /// </summary>
    private void enable_disableButtons()
    {
      string lastChar = getLastChar();
      bool isLastCharNumber = isCharNumber(lastChar);
      string secondCharFromRight = ""; //proměnná pro předposlední znak

      if (tbExpression.Text.Length > 1)
      {
        secondCharFromRight = tbExpression.Text.Substring(tbExpression.Text.Length - 2, 1); //předposlední znak
      }

      //tlačítka závorek
      btnLeftBracket.IsEnabled = false;
      btnRightBracket.IsEnabled = false;
      //tačítko desetinné tečky
      btnDot.IsEnabled = false;
      //tlačítka operací
      btnMultiply.IsEnabled = false;
      btnDivide.IsEnabled = false;
      btnPlus.IsEnabled = false;
      btnMinus.IsEnabled = false;
      btnMod.IsEnabled = false;
      //tlačítka faktoriálu a logaritmu
      btnFact.IsEnabled = false;
      btnLog.IsEnabled = false;
      //tlačítka mocniny a odmocniny
      btnPow.IsEnabled = false;
      grdPowBtn.IsEnabled = false;
      lblPowBtn.IsEnabled = false;
      btnRoot.IsEnabled = false;
      grdRootBtn.IsEnabled = false;
      lblRootBtn.IsEnabled = false;
      //tlačítko 0
      btnZero.IsEnabled = false;

      //Zajišťuje povolení stisku tlačítka 0 ve správné situaci
      if (!isDotPrintable || (isCharNumber(secondCharFromRight) && isCharNumber(lastChar)) || (!isCharNumber(secondCharFromRight) && isCharNumber(lastChar) && lastChar != "0") || !isCharNumber(lastChar))
      {
        btnZero.IsEnabled = true;
      }

      if (lastChar == "(")
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;
        btnRightBracket.IsEnabled = true;
        //tačítko desetinné tečky
        btnDot.IsEnabled = false;
        //tlačítka operací
        btnMultiply.IsEnabled = false;
        btnDivide.IsEnabled = false;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = false;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = false;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = false;
        grdPowBtn.IsEnabled = false;
        lblPowBtn.IsEnabled = false;
        btnRoot.IsEnabled = false;
        grdRootBtn.IsEnabled = false;
        lblRootBtn.IsEnabled = false;
      }
      else if ((secondCharFromRight == "(" || secondCharFromRight == unicodeRoot || secondCharFromRight == "^") && (lastChar == unicodePlus || lastChar == unicodeMinus))
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;
        btnRightBracket.IsEnabled = false;
        //tačítko desetinné tečky
        btnDot.IsEnabled = false;
        //tlačítka operací
        btnMultiply.IsEnabled = false;
        btnDivide.IsEnabled = false;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = false;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = false;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = false;
        grdPowBtn.IsEnabled = false;
        lblPowBtn.IsEnabled = false;
        btnRoot.IsEnabled = false;
        grdRootBtn.IsEnabled = false;
        lblRootBtn.IsEnabled = false;
      }
      else if (lastChar == ".")
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = false;
        btnRightBracket.IsEnabled = false;
        //tačítko desetinné tečky
        btnDot.IsEnabled = false;
        //tlačítka operací
        btnMultiply.IsEnabled = false;
        btnDivide.IsEnabled = false;
        btnPlus.IsEnabled = false;
        btnMinus.IsEnabled = false;
        btnMod.IsEnabled = false;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = false;
        btnLog.IsEnabled = false;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = false;
        grdPowBtn.IsEnabled = false;
        lblPowBtn.IsEnabled = false;
        btnRoot.IsEnabled = false;
        grdRootBtn.IsEnabled = false;
        lblRootBtn.IsEnabled = false;
      }
      else if (lastChar == ")")
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;

        if (leftBracketsCount > rightBracketsCount)
        {
          btnRightBracket.IsEnabled = true;
        }
        else
        {
          btnRightBracket.IsEnabled = false;
        }

        //tačítko desetinné tečky
        btnDot.IsEnabled = false;
        //tlačítka operací
        btnMultiply.IsEnabled = true;
        btnDivide.IsEnabled = true;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = true;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = true;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = true;
        grdPowBtn.IsEnabled = true;
        lblPowBtn.IsEnabled = true;
        btnRoot.IsEnabled = true;
        grdRootBtn.IsEnabled = true;
        lblRootBtn.IsEnabled = true;
      }
      else if (isLastCharNumber)
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;

        if (leftBracketsCount > rightBracketsCount)
        {
          btnRightBracket.IsEnabled = true;
        }
        else
        {
          btnRightBracket.IsEnabled = false;
        }

        //tačítko desetinné tečky

        if (isDotPrintable)
          btnDot.IsEnabled = true;

        //tlačítka operací
        btnMultiply.IsEnabled = true;
        btnDivide.IsEnabled = true;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = true;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = true;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = true;
        grdPowBtn.IsEnabled = true;
        lblPowBtn.IsEnabled = true;
        btnRoot.IsEnabled = true;
        grdRootBtn.IsEnabled = true;
        lblRootBtn.IsEnabled = true;
      }
      else if (lastChar == "!")
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;

        if (leftBracketsCount > rightBracketsCount)
        {
          btnRightBracket.IsEnabled = true;
        }
        else
        {
          btnRightBracket.IsEnabled = false;
        }

        //tlačítko desetinné tečky
        btnDot.IsEnabled = false;
        //tlačítka operací
        btnMultiply.IsEnabled = true;
        btnDivide.IsEnabled = true;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = true;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = false;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = true;
        grdPowBtn.IsEnabled = true;
        lblPowBtn.IsEnabled = true;
        btnRoot.IsEnabled = true;
        grdRootBtn.IsEnabled = true;
        lblRootBtn.IsEnabled = true;
      }
      else if (lastChar == unicodePlus || lastChar == unicodeMinus || lastChar == unicodeMultiply || lastChar == unicodeDivision || lastChar == "%")
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;
        btnRightBracket.IsEnabled = false;
        //tačítko desetinné tečky

        if (isDotPrintable)
          btnDot.IsEnabled = true;

        //tlačítka operací
        btnMultiply.IsEnabled = true;
        btnDivide.IsEnabled = true;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = true;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = false;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = false;
        grdPowBtn.IsEnabled = false;
        lblPowBtn.IsEnabled = false;
        btnRoot.IsEnabled = false;
        grdRootBtn.IsEnabled = false;
        lblRootBtn.IsEnabled = false;
      }
      else if (lastChar == unicodeRoot || lastChar == "^")
      {
        //tlačítka závorek
        btnLeftBracket.IsEnabled = true;
        btnRightBracket.IsEnabled = false;
        //tačítko desetinné tečky
        btnDot.IsEnabled = false;
        //tlačítka operací
        btnMultiply.IsEnabled = false;
        btnDivide.IsEnabled = false;
        btnPlus.IsEnabled = true;
        btnMinus.IsEnabled = true;
        btnMod.IsEnabled = false;
        //tlačítka faktoriálu a logaritmu
        btnFact.IsEnabled = false;
        btnLog.IsEnabled = true;
        //tlačítka mocniny a odmocniny
        btnPow.IsEnabled = false;
        grdPowBtn.IsEnabled = false;
        lblPowBtn.IsEnabled = false;
        btnRoot.IsEnabled = false;
        grdRootBtn.IsEnabled = false;
        lblRootBtn.IsEnabled = false;
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
      btnCount.Focus();
      printNumber(0);
    }

    private void btnOne_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(1);
    }

    private void btnTwo_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(2);
    }

    private void btnThree_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(3);
    }

    private void btnFour_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(4);
    }

    private void btnFive_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(5);
    }

    private void btnSix_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(6);
    }

    private void btnSeven_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(7);
    }

    private void btnEight_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(8);
    }

    private void btnNine_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(9);
    }

    //===== Události spouštějící se při kliknutí na tlačítka A-F =====
    private void btnA_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(10);
    }

    private void btnB_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(11);
    }

    private void btnC_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(12);
    }

    private void btnD_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(13);
    }

    private void btnE_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(14);
    }

    private void btnF_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printNumber(15);
    }

    //===== Události spouštějící se při kliknutí na tlačítko desetinné čárky (tečky) =====
    private void btnDot_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printDot();
    }

    //===== Události spouštějící se při kliknutí na tlačítka závorek =====
    private void btnLeftBracket_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printLeftBracket();
    }

    private void btnRightBracket_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printRightBracket();
    }

    //===== Události spouštějící se při kliknutí na tlačítka operátorů =====
    private void btnMultiply_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printMathOperator(unicodeMultiply);
    }

    private void btnDivide_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printMathOperator(unicodeDivision);
    }

    private void btnPlus_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printMathOperator(unicodePlus);
    }

    private void btnMinus_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printMathOperator(unicodeMinus);
    }

    private void btnMod_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printMathOperator("%");
    }

    //===== Události spouštějící se při kliknutí na tlačítka mocniny a odmocniny =====
    private void btnPow_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printPow();
    }

    private void grdPowBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      btnCount.Focus();
      printPow();
    }

    private void btnRoot_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printRoot();
    }

    private void grdRootBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      btnCount.Focus();
      printRoot();
    }

    //===== Události spouštějící se při kliknutí na tlačítka faktoriálu a logaritmu =====
    private void btnFact_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printFact();
    }

    private void btnLog_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      printLog();
    }

    //===== Události spouštějící se při kliknutí na tlačítka DEL, AC a "=" =====
    private void btnDel_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      removeFromBack();
    }

    private void btnAc_Click(object sender, RoutedEventArgs e)
    {
      btnCount.Focus();
      resetCalc(true);  //smaže i oblast pro výsledek
    }

    private void btnCount_Click(object sender, RoutedEventArgs e)
    {
      countExpr();
    }

    //===== Události spouštějící se při kliknutí na přepínače pro změnu soutstavy =====
    private void rbDec_Click(object sender, RoutedEventArgs e)
    {
      switchNumeralSystem(0);
    }

    private void rbBin_Click(object sender, RoutedEventArgs e)
    {
      switchNumeralSystem(1);
    }

    private void rbHex_Click(object sender, RoutedEventArgs e)
    {
      switchNumeralSystem(2);
    }

    private void rbOct_Click(object sender, RoutedEventArgs e)
    {
      switchNumeralSystem(3);
    }

    //Při změně textu povoluje a zakazuje tlačítka, která lze a nelze zmáčknout
    private void tbExpression_TextChanged(object sender, TextChangedEventArgs e)
    {
      resetResult();  //vymaže oblast pro výsledek
      // v případě, že se nejedná o desítkovou soustavu, tak budou povolena pouze tlačítka vybranéné soustavy
      if (currentNumSys == NumSystem.Dec)
      {
        enable_disableButtons();
      }
    }

    //===== Obsluha událostí po kliknutí na klávesy =====
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      //MessageBox.Show(e.Key.ToString());
      if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D6)  //SHIFT + D6 --> ^
      {
        if (btnPow.IsEnabled)
          btnPow.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D2) //SHIFT + D2 --> @
      {
        if (btnRoot.IsEnabled)
          btnRoot.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D8) //SHIFT + D8 --> *
      {
        if (btnMultiply.IsEnabled)
          btnMultiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D5) //SHIFT + D5 --> %
      {
        if (btnMod.IsEnabled)
          btnMod.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D9) //SHIFT + D9 --> (
      {
        if (btnLeftBracket.IsEnabled)
          btnLeftBracket.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D0) //SHIFT + D0 --> )
      {
        if (btnRightBracket.IsEnabled)
          btnRightBracket.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) && e.Key == Key.D1) //SHIFT + D1 --> !
      {
        if (btnFact.IsEnabled)
          btnFact.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.D) //CTRL + D --> DEC
      {
        if (rbDec.IsEnabled)
        {
          rbDec.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
          rbDec.IsChecked = true;
        }
      }
      else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.B) //CTRL + B --> BIN
      {
        if (rbBin.IsEnabled)
        {
          rbBin.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
          rbBin.IsChecked = true;
        }
      }
      else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.H) //CTRL + H --> HEX
      {
        if (rbHex.IsEnabled)
        {
          rbHex.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
          rbHex.IsChecked = true;
        }
      }
      else if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && e.Key == Key.O) //CTRL + O --> OCT
      {
        if (rbOct.IsEnabled)
        {
          rbOct.RaiseEvent(new RoutedEventArgs(RadioButton.ClickEvent));
          rbOct.IsChecked = true;
        }
      }
      else if (e.Key == Key.OemQuestion) // /
      {
        if (btnDivide.IsEnabled)
          btnDivide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.OemPlus) // +
      {
        if (btnPlus.IsEnabled)
          btnPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.OemMinus) // -
      {
        if (btnMinus.IsEnabled)
          btnMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad0 || e.Key == Key.D0) // 0
      {
        if (btnZero.IsEnabled)
          btnZero.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad1 || e.Key == Key.D1)
      { // 1
        if (btnOne.IsEnabled)
          btnOne.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad2 || e.Key == Key.D2) // 2
      {
        if (btnTwo.IsEnabled)
          btnTwo.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad3 || e.Key == Key.D3) // 3
      {
        if (btnThree.IsEnabled)
          btnThree.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad4 || e.Key == Key.D4) // 4
      {
        if (btnFour.IsEnabled)
          btnFour.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad5 || e.Key == Key.D5) // 5
      {
        if (btnFive.IsEnabled)
          btnFive.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad6 || e.Key == Key.D6) // 6
      {
        if (btnSix.IsEnabled)
          btnSix.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad7 || e.Key == Key.D7) // 7
      {
        if (btnSeven.IsEnabled)
          btnSeven.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad8 || e.Key == Key.D8) // 8
      {
        if (btnEight.IsEnabled)
          btnEight.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.NumPad9 || e.Key == Key.D9) // 9
      {
        if (btnNine.IsEnabled)
          btnNine.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.A)  // A
      {
        if (btnA.IsEnabled)
          btnA.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.B)  // B
      {
        if (btnB.IsEnabled)
          btnB.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.C)  // C
      {
        if (btnC.IsEnabled)
          btnC.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.D)  // D
      {
        if (btnD.IsEnabled)
          btnD.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.E)  // E
      {
        if (btnE.IsEnabled)
          btnE.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.F)  // F
      {
        if (btnF.IsEnabled)
          btnF.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.Multiply)  // *
      {
        if (btnMultiply.IsEnabled)
          btnMultiply.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.Divide)  // /
      {
        if (btnDivide.IsEnabled)
          btnDivide.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.Add) // +
      {
        if (btnPlus.IsEnabled)
          btnPlus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.Subtract)  // -
      {
        if (btnMinus.IsEnabled)
          btnMinus.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)  // .
      {
        if (btnDot.IsEnabled)
          btnDot.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.L)  // log(
      {
        if (btnLog.IsEnabled)
          btnLog.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.Back)  // DEL
      {
        if (btnDel.IsEnabled)
          btnDel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((e.Key == Key.Delete) || (e.Key == Key.Escape))  // AC
      {
        if (btnAc.IsEnabled)
          btnAc.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.Enter)  // Enter
      {
        if (btnCount.IsEnabled)
          btnCount.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if (e.Key == Key.F1) // F1
      {
        if (btnHelp.IsEnabled)
          btnHelp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
    }

    private void tbExpression_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Back)  // DEL
      {
        if (btnDel.IsEnabled)
          btnDel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((e.Key == Key.Delete) || (e.Key == Key.Escape))  // AC
      {
        if (btnAc.IsEnabled)
          btnAc.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
    }

    private void tbResult_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Back)  // DEL
      {
        if (btnDel.IsEnabled)
          btnDel.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
      else if ((e.Key == Key.Delete) || (e.Key == Key.Escape))  // AC
      {
        if (btnAc.IsEnabled)
          btnAc.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
      }
    }

    //===== Otevření okna s nápovědou =====
    private void btnHelp_Click(object sender, RoutedEventArgs e)
    {
      help window = new help();
      window.Show();
    }
  }
}
