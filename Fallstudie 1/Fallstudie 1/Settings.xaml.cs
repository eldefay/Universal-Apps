using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Fallstudie_1
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }


        private void cbx_boardSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Windows.Storage.ApplicationData.Current.RoamingSettings.Values["boardSize"] = (int)cbx_boardSize.SelectedItem;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cbx_boardSize.ItemsSource = new List<int> { 8, 9, 10 };

            cbx_boardSize.SelectedItem = Windows.Storage.ApplicationData.Current.RoamingSettings.Values["boardSize"];

        }
    }
}
