/*******************************************************************
* Název projektu: IVS-Kalkulačka
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

using System.Windows;
using System.Windows.Input;

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
