using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Fallstudie_1
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Board : Page
    {
        public Board()
        {
            this.InitializeComponent();
        }

        int boardSize = (int)Windows.Storage.ApplicationData.Current.RoamingSettings.Values["boardSize"];

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            g_board.Width = g_board.Height = ActualWidth;            

            for (int i = 0; i < boardSize; i++)
            {
                g_board.ColumnDefinitions.Add(new ColumnDefinition());
                g_board.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Rectangle rect = new Rectangle();
                    Random rand = new Random(Guid.NewGuid().ToString().GetHashCode()); ;
                    byte[] colorBytes = new byte[3];
                    rand.NextBytes(colorBytes);
                    Color randomColor = Color.FromArgb(255, colorBytes[0], colorBytes[1], colorBytes[2]);
                    rect.Fill = new SolidColorBrush(randomColor);

                    g_board.Children.Add(rect);

                    rect.SetValue(Grid.ColumnProperty, j);
                    rect.SetValue(Grid.RowProperty, i);
                }
            }

            el_player.SetValue(Canvas.ZIndexProperty, 1000);
            el_player.SetValue(Grid.RowProperty, boardSize-1);

            Random rnd = new Random();
            int numb = rnd.Next(0, 6);
            string str = "\\u268"+numb;
            b_dice.Content = System.Text.RegularExpressions.Regex.Unescape(str);
        }

        private void b_dice_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int dice = rnd.Next(0, 6);
            string str = "\\u268" + dice;
            b_dice.Content = System.Text.RegularExpressions.Regex.Unescape(str);

            var currentC = (int)el_player.GetValue(Grid.ColumnProperty);

            for (int i = 0; i <= dice; i++)
            {
                var posR = (int)el_player.GetValue(Grid.RowProperty);
                var inc = posR % 2 == 0 ? -1 : 1;
                var posC = (int)el_player.GetValue(Grid.ColumnProperty) + inc;

                if (posC < 0 || posC > boardSize - 1)
                {
                    posC -= inc;
                    posR -= 1;
                }
                if (posR >= 0)
                {
                    el_player.SetValue(Grid.ColumnProperty, posC);
                    el_player.SetValue(Grid.RowProperty, posR);
                }
                else
                {
                    el_player.SetValue(Grid.ColumnProperty, currentC);
                }
            }
            
        }
    }
}
