/*******************************************************************
* Název projektu: IVS-Kalkulačka
* Balíček: GUI
* Soubor: help.xaml.cs
* Datum: 15.04.2017
* Autor: Radim Blaha
* Naposledy upravil: Radim Blaha
* Datum poslední změny: 19.04.2017
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
using System.IO;

namespace GUI
{
  /// <summary>
  /// Interakční logika pro help.xaml
  /// </summary>
  public partial class help : Window
  {
    public help()
    {
      string fullPath = Path.GetFullPath("doc/index.html"); //překlad relativní cesty na absolutní
      InitializeComponent();
      this.Title = "Kalkulačka - nápověda";
      webBrowser.Navigate(fullPath);  //navigování na soubor s nápovědou
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Escape)
        this.Close();
    }
  }
}
