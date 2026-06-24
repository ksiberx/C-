using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Threading;

namespace Pyatnashki
{
    public partial class MainWindow : Window
    {
        private int[] board = new int[16];
        private Button[] buttons = new Button[16];

        private Random rnd = new Random();
        private int moves = 0;
        private DispatcherTimer timer;
        private int seconds = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();


            CreateButtons();
            NewGame();
        }

        // кнопки
        private void CreateButtons()
        {
            for (int i = 0; i < 16; i++)
            {
                Button btn = new Button();
                btn.Background = System.Windows.Media.Brushes.LightGoldenrodYellow;
                btn.FontSize = 28;
                btn.Tag = i;
                btn.Click += Tile_Click;
                
                buttons[i] = btn;
                GameGrid.Children.Add(btn);
            }
        }
        // сохранение
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Файл пятнашек (*.p15)|*.p15";
            saveDialog.DefaultExt = ".p15";

            if (saveDialog.ShowDialog() == true)
            {
                string data =
                    string.Join(" ", board) +
                    Environment.NewLine +
                    moves +
                    Environment.NewLine +
                    seconds;

                File.WriteAllText(saveDialog.FileName, data);

                MessageBox.Show("Игра сохранена.");

            }
        }

        // открытие
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Файл пятнашек (*.p15)|*.p15";

            if (openDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openDialog.FileName);

                board = lines[0]
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                moves = int.Parse(lines[1]);
                seconds = int.Parse(lines[2]);

                MovesText.Text = $"Ходы: {moves}";

                TimeSpan time = TimeSpan.FromSeconds(seconds);

                TimeText.Text =
                    $"Время: {time.Minutes:D2}:{time.Seconds:D2}";

                UpdateBoard();

                MessageBox.Show("Игра загружена.");
            }
        }

        // правила
        private void Rules_Click(object sender, RoutedEventArgs e)
        {
            RulesWindow rulesWindow = new RulesWindow();

            rulesWindow.Owner = this;
            rulesWindow.ShowDialog();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Закрыть приложение?",
                "Выход",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question)
                == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++;

            TimeSpan time = TimeSpan.FromSeconds(seconds);

            TimeText.Text =
                $"Время: {time.Minutes:D2}:{time.Seconds:D2}";
        }

        private void NewGame()
        {
            board = new int[]
            {
                1,2,3,4,
                5,6,7,8,
                9,10,11,12,
                13,14,15,0
            };

            int previousEmpty = -1;

            for (int i = 0; i < 100; i++)
            {
                int empty = Array.IndexOf(board, 0);

                List<int> variants = new List<int>();

                int row = empty / 4;
                int col = empty % 4;

                if (row > 0 && empty - 4 != previousEmpty)
                    variants.Add(empty - 4);

                if (row < 3 && empty + 4 != previousEmpty)
                    variants.Add(empty + 4);

                if (col > 0 && empty - 1 != previousEmpty)
                    variants.Add(empty - 1);

                if (col < 3 && empty + 1 != previousEmpty)
                    variants.Add(empty + 1);

                int selected = variants[rnd.Next(variants.Count)];

                board[empty] = board[selected];
                board[selected] = 0;

                previousEmpty = empty;
            }

            moves = 0;
            MovesText.Text = "Ходы: 0";
            seconds = 0;
            TimeText.Text = "Время: 00:00";

            UpdateBoard();
        }

        private void UpdateBoard()
        {
            for (int i = 0; i < 16; i++)
            {
                if (board[i] == 0)
                {
                    buttons[i].Content = "";
                    buttons[i].IsEnabled = false;
                }
                else
                {
                    buttons[i].Content = board[i].ToString();
                    buttons[i].IsEnabled = true;
                }
            }
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int index = Array.IndexOf(buttons, btn);
            int empty = Array.IndexOf(board, 0);
            

            int r1 = index / 4;
            int c1 = index % 4;

            int r2 = empty / 4;
            int c2 = empty % 4;

            if (Math.Abs(r1 - r2) + Math.Abs(c1 - c2) != 1)
                return;

            int temp = board[index];
            board[index] = board[empty];
            board[empty] = temp;
            

            moves++;
            MovesText.Text = $"Ходы: {moves}";

            UpdateBoard();

            CheckWin();
        }

        private void CheckWin()
        {
            if (board.SequenceEqual(new int[]
            {
        1,2,3,4,
        5,6,7,8,
        9,10,11,12,
        13,14,15,0
            }))
            {
                MessageBox.Show("Победа!");
            }
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}
