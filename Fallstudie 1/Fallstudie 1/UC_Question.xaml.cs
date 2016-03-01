using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Fallstudie_1
{
    public sealed partial class UC_Question : UserControl
    {
        public UC_Question()
        {
            this.InitializeComponent();
        }

        private async void answers_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var awnser = lb_awnsers.SelectedItem as Awnser;
            string message = "";
            if (awnser.right)
            {
                message = "Antwort " + awnser.text + " war richtig!";
            }
            else
            {
                var rightA = (from a in lb_awnsers.Items where (a as Awnser).right == true select (a as Awnser).text).FirstOrDefault();
                message = "Antwort war falsch!\n Richtige Antwort ist "+ rightA;
            }
            MessageDialog md = new MessageDialog(message);
            await md.ShowAsync();
            this.Visibility = Visibility.Collapsed;
            var sp = (this.Parent as Grid).FindName("sp_menu") as StackPanel;
            sp.IsHitTestVisible = true;
        }
    }
}
