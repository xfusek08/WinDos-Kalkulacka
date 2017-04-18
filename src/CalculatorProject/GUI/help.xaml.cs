/*******************************************************************
* Název projektu: Nápověda pro GUI
* Balíček: GUI
* Soubor: help.xaml.cs
* Datum: 15.04.2017
* Autor: Radim Blaha
* Naposledy upravil: Radim Blaha
* Datum poslední změny: 18.04.2017
*
* Popis: Třída interakční logiky pro okno nápovědy kalkulačky.
*
*****************************************************************/

/**
 * @brief Nápověda pro GUI
 * @file help.xaml.cs
 * @author Radim Blaha
 * @date 15.04.2017
 */

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
using System.Windows.Shapes;

namespace GUI
{
  /// <summary>
  /// Interakční logika pro help.xaml
  /// </summary>
  public partial class help : Window
  {
    public help()
    {
      InitializeComponent();
      this.Title = "Kalkulačka - nápověda";
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      this.Close();
    }
  }
}
