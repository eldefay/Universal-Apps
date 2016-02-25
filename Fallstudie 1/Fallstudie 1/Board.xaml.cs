using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
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
        int boardSize = (int)Windows.Storage.ApplicationData.Current.RoamingSettings.Values["boardSize"];
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer rollDice = new DispatcherTimer();   
        int dice;
        bool rolling = false;
        int toFinish;

        public Board()
        {
            this.InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += MovePlayer_Tick;

            rollDice.Interval = TimeSpan.FromMilliseconds(25);
            rollDice.Tick += RollDice_Tick;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            toFinish = boardSize * boardSize - 1;

            var boardScale = ActualWidth;

            if (ActualWidth > ActualHeight)
            {
                boardScale = ActualHeight;
            }

            g_board.Width = g_board.Height = boardScale;

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
                    //Random rand = new Random(Guid.NewGuid().ToString().GetHashCode()); ;
                    //byte[] colorBytes = new byte[3];
                    //rand.NextBytes(colorBytes);
                    //Color randomColor = Color.FromArgb(255, colorBytes[0], colorBytes[1], colorBytes[2]);
                    //rect.Fill = new SolidColorBrush(randomColor);

                    Color rectColor = Color.FromArgb(255, 78, 205, 196);

                    if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                    {
                        rectColor = Color.FromArgb(255, 255, 107, 107);
                    }

                    rect.Fill = new SolidColorBrush(rectColor);
                    rect.Stroke = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)); ;

                    g_board.Children.Add(rect);

                    rect.SetValue(Grid.ColumnProperty, j);
                    rect.SetValue(Grid.RowProperty, i);
                }
            }

            el_player.Width = el_player.Height = boardScale/boardSize * 0.75;
            el_player.SetValue(Canvas.ZIndexProperty, 1000);
            el_player.SetValue(Grid.RowProperty, boardSize - 1);

            Random rnd = new Random();
            int numb = rnd.Next(0, 6);
            string str = "\\u268" + numb;
            b_dice.Content = System.Text.RegularExpressions.Regex.Unescape(str);
        }

        private void b_dice_Click(object sender, RoutedEventArgs e)
        {
            if (rolling)
            {
                rollDice.Stop();
                rolling = false;
                

                if(toFinish >= dice)
                {
                    b_dice.IsEnabled = false;
                    timer.Start();
                }                
            }
            else
            {
                rolling = true;
                rollDice.Start();
            }
            
        }

        private void RollDice_Tick(object sender, object e)
        {
            Random rnd = new Random();
            dice = rnd.Next(1, 7);
            string str = "\\u268" + (dice - 1);
            b_dice.Content = System.Text.RegularExpressions.Regex.Unescape(str);
        }

        private async void MovePlayer_Tick(object sender, object e)
        {
            if(dice > 0)
            {
                var posR = (int)el_player.GetValue(Grid.RowProperty);
                var inc = (boardSize - posR) % 2 == 0 ? -1 : 1;
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
                toFinish--;
                dice--;
            }
            else
            {
                timer.Stop();
                if (toFinish > 0)
                {
                    uc_question.Visibility = Visibility.Visible;
                    b_dice.IsEnabled = true;
                }
                else
                {
                    MessageDialog winMessage = new MessageDialog("Win!");
                    await winMessage.ShowAsync();
                }
            }
            
        }
    }
}
