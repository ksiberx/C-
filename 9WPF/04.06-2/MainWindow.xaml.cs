using System;
using System.Windows;
using System.Windows.Interop;

namespace MonitorDiagonal
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            // Получаем дескриптор окна
            IntPtr hwnd = new WindowInteropHelper(this).Handle;

            // Получаем HDC (контекст устройства)
            IntPtr hdc = GetDC(hwnd);

            // Получаем DPI
            int dpiX = GetDeviceCaps(hdc, 88);   // LOGPIXELSX = 88
            int dpiY = GetDeviceCaps(hdc, 90);   // LOGPIXELSY = 90

            // Освобождаем HDC
            ReleaseDC(hwnd, hdc);

            // Размеры экрана в пикселях
            double widthPx = SystemParameters.PrimaryScreenWidth;
            double heightPx = SystemParameters.PrimaryScreenHeight;

            // Переводим в дюймы
            double widthInches = widthPx / dpiX;
            double heightInches = heightPx / dpiY;

            // Диагональ
            double diagonal = Math.Sqrt(widthInches * widthInches + heightInches * heightInches);

            txtResult.Text = $"Диагональ: {diagonal:F1} дюймов";
        }

        // WinAPI функции
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
    }
}
