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
        private int[] board = new int[16]; // массив из 16 клеток поля
        private Button[] buttons = new Button[16]; // кнопки

        private Random rnd = new Random();
        private int moves = 0;
        private DispatcherTimer timer; // таймер
        private int seconds = 0;

        public MainWindow()
        {
            InitializeComponent(); 
            timer = new DispatcherTimer(); // создание таймера
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start(); // запуска таймера


            CreateButtons();
            NewGame();
        }

        // кнопки
        private void CreateButtons()
        {
            for (int i = 0; i < 16; i++) // цикл по всем 16 клеткам
            {
                Button btn = new Button();
                btn.Background = System.Windows.Media.Brushes.LightGoldenrodYellow; 
                btn.FontSize = 28; // размер шрифта
                btn.Tag = i; 
                btn.Click += Tile_Click; 
                
                buttons[i] = btn; // сохранение кнопки
                GameGrid.Children.Add(btn); // добавление кнопки в сетку
            }
        }
        // сохранение
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog(); 

            saveDialog.Filter = "Файл пятнашек (*.p15)|*.p15";
            saveDialog.DefaultExt = ".p15";

            if (saveDialog.ShowDialog() == true) // если пользователь выбрал файл
            {
                // перенос всего файла
                string data =
                    string.Join(" ", board) +
                    Environment.NewLine +
                    moves +
                    Environment.NewLine +
                    seconds;

                File.WriteAllText(saveDialog.FileName, data); // сохранение данных в файл

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
                string[] lines = File.ReadAllLines(openDialog.FileName); // чтение строк файла

                // перенос файла
                board = lines[0]
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                moves = int.Parse(lines[1]); // загрузка ходов
                seconds = int.Parse(lines[2]); // загрузка времени

                MovesText.Text = $"Ходы: {moves}"; 

                TimeSpan time = TimeSpan.FromSeconds(seconds);

                TimeText.Text =
                    $"Время: {time.Minutes:D2}:{time.Seconds:D2}";

                UpdateBoard(); // перерисовка поля

                MessageBox.Show("Игра загружена.");
            }
        }

        // правила
        private void Rules_Click(object sender, RoutedEventArgs e)
        {
            RulesWindow rulesWindow = new RulesWindow(); // новое окно с правилами

            rulesWindow.Owner = this; 
            rulesWindow.ShowDialog(); // открытие окна
        }

        // выход 
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

        // врем
        private void Timer_Tick(object sender, EventArgs e)
        {
            seconds++; 

            TimeSpan time = TimeSpan.FromSeconds(seconds);

            TimeText.Text =
                $"Время: {time.Minutes:D2}:{time.Seconds:D2}"; 
        }

        // новая игра
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

            for (int i = 0; i < 100; i++) // 100 случайных ходов
            {
                int empty = Array.IndexOf(board, 0); // поиск пустой клетки

                List<int> variants = new List<int>(); // список возможных ходов

                int row = empty / 4; 
                int col = empty % 4;

                if (row > 0 && empty - 4 != previousEmpty) 
                    variants.Add(empty - 4); // ход вверх

                if (row < 3 && empty + 4 != previousEmpty)
                    variants.Add(empty + 4); // ход вниз

                if (col > 0 && empty - 1 != previousEmpty)
                    variants.Add(empty - 1); // ход влево

                if (col < 3 && empty + 1 != previousEmpty)
                    variants.Add(empty + 1); // ход вправо

                int selected = variants[rnd.Next(variants.Count)]; // случайный выбор хода

                board[empty] = board[selected]; // перемещение плитки на пустое время
                board[selected] = 0; // освобождение предыдущей позиции плитки

                previousEmpty = empty; // запоминание предыдущей позиции пустой клетки
            }

            moves = 0;
            MovesText.Text = "Ходы: 0";
            seconds = 0;
            TimeText.Text = "Время: 00:00";

            UpdateBoard(); // перерисовка поля
        }

        // обновление клеток
        private void UpdateBoard()
        {
            for (int i = 0; i < 16; i++) // проходит по 16 клеткам поля
            {
                if (board[i] == 0) // если клетка пустая
                {
                    buttons[i].Content = ""; // убираем текст с кнопки
                    buttons[i].IsEnabled = false; // делаем кнопку не активной
                }
                else
                {
                    buttons[i].Content = board[i].ToString(); // выводим число на кнопку
                    buttons[i].IsEnabled = true; // включаем кнопку
                }
            }
        }

        // обрабатывание кнопок
        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            int index = Array.IndexOf(buttons, btn); // индекс нажатой кнопки
            int empty = Array.IndexOf(board, 0); // пустую клетку
            

            int r1 = index / 4; // строка нажатой клетки
            int c1 = index % 4; // столбец нажатой клетки

            int r2 = empty / 4; // строка пустой клетки
            int c2 = empty % 4; // столбец пустой клетки

            if (Math.Abs(r1 - r2) + Math.Abs(c1 - c2) != 1) // если клетки не соседние
                return; // отмена хода

            int temp = board[index]; 
            board[index] = board[empty]; // перещемение пустоты на место нажатой клетки
            board[empty] = temp; // перемещение плитки на место пустоты
            

            moves++;
            MovesText.Text = $"Ходы: {moves}";

            UpdateBoard(); 

            CheckWin(); // проверка условия победы
        }

        private void CheckWin()
        {
            if (board.SequenceEqual(new int[] // сравнение поля с решенным состоянием
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
            NewGame(); // запуск новой игры
        }
    }
}