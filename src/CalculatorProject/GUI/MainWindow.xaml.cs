/*******************************************************************
* Název projektu: GUI
* Balíček: GUI
* Soubor: MainWindow.xaml.cs
* Datum: 05.04.2017
* Autor: Radim Blaha
* Naposledy upravil: Radim Blaha
* Datum poslední změny: 06.04.2017
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
    public MainWindow()
    {
      InitializeComponent();
    }


    //===== Eventy spoštějící se při najetí a opuštění na tlačítka odmocniny =====
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

    //===== Eventy spoštějící se při najetí a opuštění na tlačítka mocniny =====
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
  }
}
