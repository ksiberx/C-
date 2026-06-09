using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        double firstNumber = 0;
        string operation = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            tbCurrent.Text += btn.Content.ToString();
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurrent.Text == "") return;

            firstNumber = double.Parse(tbCurrent.Text);

            Button btn = (Button)sender;
            operation = btn.Content.ToString();

            tbHistory.Text = tbCurrent.Text + " " + operation;
            tbCurrent.Clear();
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurrent.Text == "") return;

            double secondNumber = double.Parse(tbCurrent.Text);
            double result = 0;

            switch (operation)
            {
                case "+": result = firstNumber + secondNumber; break;
                case "-": result = firstNumber - secondNumber; break;
                case "*": result = firstNumber * secondNumber; break;
                case "/": result = firstNumber / secondNumber; break;
            }

            tbCurrent.Text = result.ToString();
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            tbCurrent.Clear();
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            tbCurrent.Clear();
            tbHistory.Clear();
            firstNumber = 0;
            operation = "";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (tbCurrent.Text.Length > 0)
                tbCurrent.Text =
                    tbCurrent.Text.Remove(tbCurrent.Text.Length - 1);
        }
    }
}
