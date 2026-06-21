using System; 
using System.Collections.Generic; 
using System.Windows; 
using System.Windows.Controls; 
using System.Windows.Input; // MouseEventArgs для обработки движения мыши
using System.Windows.Media; // Brushes (цвета)
using System.Windows.Shapes;
using System.Windows.Threading; // DispatcherTimer — таймер игрового цикла

namespace AutoRacing2D 
{
    public partial class MainWindow : Window // главное окно, partial — вторая часть в MainWindow.xaml
    {
        private readonly DispatcherTimer _gameLoop = new DispatcherTimer(DispatcherPriority.Render); // таймер с высоким приоритетом — рисуется без задержек
        private readonly List<Rectangle> _traffic = new List<Rectangle>(); // список машин трафика на канвасе
        private readonly List<Rectangle> _dashes = new List<Rectangle>(); // список штрихов центральной разметки
        private readonly Random _rnd = new Random(); 
        private string _lang = "ru"; // текущий язык интерфейса (ru/en), по умолчанию русский

        private const double RoadLeftX = 250; 
        private const double RoadRightX = 650;
        private const double FieldHeight = 600; 

        private double _carX = 450; 
        private const double CarY = 500; 
        private double _mouseX = 450; 
        private bool _isGameRunning; 

        private int _score; // текущее количество очков за сессию
        private double _speed = 100; // текущая скорость (км/час), растёт с течением времени
        private double _maxSpeed = 100; 
        private const double TopSpeed = 200;
        private int _tickCounter; // счётчик кадров для начисления очков раз в N тиков
        private int _nextSpawnIn; // сколько кадров осталось до следующего спавна машины трафика
        private int _targetTrafficCount; // сколько машин трафика должно быть на поле одновременно (3-4)

        public MainWindow() // конструктор окна — выполняется при запуске
        {
            InitializeComponent(); // загружает разметку из MainWindow.xaml
            _gameLoop.Interval = TimeSpan.FromMilliseconds(16); // 16мс = 60 кадров в секунду
            _gameLoop.Tick += GameLoop_Tick; // подписываемся на событие тика таймера
            BuildDashedLine(); // один раз создаём штрихи центральной линии
        }

        private void BuildDashedLine() // создаёт штрихи прерывистой центральной линии один раз
        {
            for (int i = 0; i < 10; i++) 
            {
                var dash = new Rectangle { Width = 6, Height = 30, Fill = Brushes.White }; // один штрих разметки
                Canvas.SetTop(dash, i * 65 - 60); 
                DashContainer.Children.Add(dash); 
                _dashes.Add(dash); 
            }
        }

        private void LangButton_Click(object sender, RoutedEventArgs e) // обработчик кнопки переключения языка (ru/en)
        {
            _lang = _lang == "ru" ? "en" : "ru"; // переключаем язык на противоположный
            LangButton.Content = _lang; 
            ApplyLocalization(); // обновляем все тексты на экране
        }

        private void ApplyLocalization() // метод подставляет тексты по текущему языку
        {
            bool ru = _lang == "ru"; // короткая проверка: русский язык или нет
            TitleText.Text = ru ? "Автогоночки 2D" : "Auto Racing 2D"; 
            StartButton.Content = ru ? "Начать игру" : "Start Game"; 
            ExitButton.Content = ru ? "Выйти из игры" : "Exit Game"; 
            RestartButton.Content = ru ? "Начать игру заново" : "Restart"; 
            ToMenuButton.Content = ru ? "Выйти в меню" : "Back to Menu";
            UpdateHud(); 
        }

        private void UpdateHud() // обновляет текст очков и скорости в HUD
        {
            bool ru = _lang == "ru"; // проверка текущего языка
            ScoreText.Text = (ru ? "Очки: " : "Score: ") + _score; 
            SpeedText.Text = (ru ? "Скорость: " : "Speed: ") + Math.Round(_speed) + (ru ? " км/час" : " km/h"); 
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) 
        {
            Application.Current.Shutdown(); // закрывает приложение полностью
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) 
        {
            StartGame(); // запускаем игровую сессию
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e) 
        {
            StartGame(); // заново запускаем сессию
        }

        private void ToMenuButton_Click(object sender, RoutedEventArgs e) 
        {
            DefeatPanel.Visibility = Visibility.Collapsed; // скрываем экран поражения
            GamePanel.Visibility = Visibility.Collapsed; // скрываем игровое поле
            MenuPanel.Visibility = Visibility.Visible; // показываем главное меню
        }

        private void StartGame() 
        {
            MenuPanel.Visibility = Visibility.Collapsed; // скрываем главное меню
            DefeatPanel.Visibility = Visibility.Collapsed; // скрываем экран поражения (если был)
            GamePanel.Visibility = Visibility.Visible; // показываем игровое поле

            _carX = (RoadLeftX + RoadRightX) / 2; // ставим машину игрока в центр дороги
            Canvas.SetLeft(PlayerCar, _carX - PlayerCar.Width / 2); // выставляем X машины
            Canvas.SetTop(PlayerCar, CarY - PlayerCar.Height / 2); // выставляем Y машины 

            foreach (var car in _traffic) GameCanvas.Children.Remove(car); // убираем машины трафика с прошлой сессии
            _traffic.Clear(); // очищаем список трафика
            _targetTrafficCount = _rnd.Next(3, 5); // случайно выбираем: держать на поле 3 или 4 машины одновременно
            for (int i = 0; i < _targetTrafficCount; i++) SpawnTrafficCar(); // сразу заполняем поле нужным количеством машин
            _nextSpawnIn = _rnd.Next(40, 90); // задаём случайную задержку до следующего возможного спавна

            _score = 0; 
            _speed = 100; 
            _maxSpeed = 100; 
            _tickCounter = 0; 
            UpdateHud(); 

            _isGameRunning = true; 
            _gameLoop.Start(); // запускаем игровой таймер/цикл
        }

        private void SpawnTrafficCar() // создаёт одну машину трафика случайного цвета в случайной точке выше экрана
        {
            Brush[] colors = { Brushes.MediumPurple, Brushes.IndianRed, Brushes.Gold, Brushes.Orchid }; // палитра случайных цветов машин
            var car = new Rectangle { Width = 34, Height = 56, Fill = colors[_rnd.Next(colors.Length)], RadiusX = 6, RadiusY = 6 }; 
            double laneX = _rnd.Next((int)RoadLeftX + 25, (int)RoadRightX - 25); // случайная X-позиция в пределах дороги
            Canvas.SetLeft(car, laneX - car.Width / 2); 
            Canvas.SetTop(car, -60 - _rnd.Next(0, 200)); 
            GameCanvas.Children.Add(car); // добавляем машину
            _traffic.Add(car); // добавляем машину в список
        }

        private void Window_MouseMove(object sender, MouseEventArgs e) // обработчик движения мыши над всем окном
        {
            if (GamePanel.Visibility != Visibility.Visible) return; // если игра не на экране — координату не обновляем
            _mouseX = e.GetPosition(GameCanvas).X; // координата курсора пересчитана относительно канваса игры
        }

        private void GameLoop_Tick(object sender, EventArgs e) // основной игровой цикл — вызывается каждый кадр (60 фпс)
        {
            if (!_isGameRunning) return; // если игра не активна — ничего не делаем

            _carX += (_mouseX - _carX) * 0.2; // плавно подтягиваем машину к X-координате курсора (быстрее реакция)
            Canvas.SetLeft(PlayerCar, _carX - PlayerCar.Width / 2); // обновляем позицию машины игрока на канвасе

            double trafficSpeed = 2 + (_speed - 100) / 14; // скорость трафика напрямую зависит от текущей скорости игры (заметно растёт к 200)
            for (int i = 0; i < _dashes.Count; i++) // двигаем штрихи разметки для эффекта движения дороги
            {
                double dy = Canvas.GetTop(_dashes[i]) + trafficSpeed; // смещаем штрих вниз пропорционально скорости
                if (dy > FieldHeight) dy = -30; 
                Canvas.SetTop(_dashes[i], dy); 
            }

            _tickCounter++; // увеличиваем счётчик кадров
            if (_tickCounter % 20 == 0) // раз в треть секунды (20 кадров при 60 FPS) — скорость растёт заметнее
            {
                _score += 10;
                _speed = Math.Min(_speed + 4, TopSpeed); // увеличиваем скорость постепенно, с ограничением в 200
                if (_speed > _maxSpeed) _maxSpeed = _speed; // фиксируем новый рекорд скорости
                UpdateHud(); 
            }

            if (_carX - PlayerCar.Width / 2 < RoadLeftX || _carX + PlayerCar.Width / 2 > RoadRightX) // проверка: вышла ли машина за границы дороги
            {
                EndGame(); // если да — завершаем игру поражением
                return; 
            }

            for (int i = _traffic.Count - 1; i >= 0; i--) // идём с конца списка, т.к. внутри цикла возможно удаление элементов
            {
                var car = _traffic[i]; // текущая машина трафика
                double top = Canvas.GetTop(car) + trafficSpeed; // двигаем машину трафика вниз пропорционально скорости
                Canvas.SetTop(car, top); // применяем новую Y-координату машины трафика

                if (top > FieldHeight) // если машина полностью ушла за низ экрана
                {
                    GameCanvas.Children.Remove(car); // удаляем машину с канваса
                    _traffic.RemoveAt(i); // удаляем машину из списка трафика
                    continue; 
                }

                if (IsColliding(car)) // проверяем столкновение машины игрока с этой машиной трафика
                {
                    EndGame(); // если столкновение — завершаем игру поражением
                    return; 
                }
            }

            _nextSpawnIn--; // уменьшаем таймер до следующего возможного спавна
            if (_nextSpawnIn <= 0 && _traffic.Count < _targetTrafficCount) // если время пришло и машин на поле меньше целевого числа (3-4)
            {
                SpawnTrafficCar(); 
                _nextSpawnIn = _rnd.Next(40, 100); 
            }
        }

        private bool IsColliding(Rectangle car) // проверка столкновения прямоугольников
        {
            double carLeft = Canvas.GetLeft(car), carTop = Canvas.GetTop(car); // координаты левого верхнего угла машины трафика
            double playerLeft = _carX - PlayerCar.Width / 2, playerTop = CarY - PlayerCar.Height / 2; // координаты левого верхнего угла машины игрока

            return playerLeft < carLeft + car.Width && // пересечение по X слева
                   playerLeft + PlayerCar.Width > carLeft && // пересечение по X справа
                   playerTop < carTop + car.Height && // пересечение по Y сверху
                   playerTop + PlayerCar.Height > carTop; // пересечение по Y снизу
        }

        private void EndGame() // завершение игровой сессии поражением
        {
            _isGameRunning = false; // помечаем игру как неактивную
            _gameLoop.Stop(); 
            GamePanel.Visibility = Visibility.Collapsed; // скрываем игровое поле
            bool ru = _lang == "ru"; // проверка текущего языка
            ScoreFinalText.Text = (ru ? "Количество очков: " : "Score: ") + _score; // итоговые очки на экране поражения
            MaxSpeedText.Text = (ru ? "Максимальная скорость: " : "Max speed: ") + Math.Round(_maxSpeed); // максимальная скорость на экране поражения
            DefeatPanel.Visibility = Visibility.Visible; // показываем экран поражения
        }
    }
}
