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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Studienleistung
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sp_main.DataContext = App.meeting;
            tbl_cpm.Text = App.cpm.ToString("C") + " per minute";
        }

        private void ab_setting_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }

        private void ab_remove_Click(object sender, RoutedEventArgs e)
        {
            if(App.meeting.participants > 1)
            {
                App.meeting.participants--;
                tbl_cpm.Text = App.cpm.ToString("C") + " per minute";
            }
        }

        private void ab_add_Click(object sender, RoutedEventArgs e)
        {
            App.meeting.participants++;
            tbl_cpm.Text = App.cpm.ToString("C") + " per minute";
        }

        private void b_start_Click(object sender, RoutedEventArgs e)
        {
            if (App.running)
            {
                App.timer.Stop();
                App.running = false;
                Frame.Navigate(typeof(MeetingEndPage));
            }
            else
            {
                App.running = true;
                App.timer.Start();
            }
        }
    }
}
