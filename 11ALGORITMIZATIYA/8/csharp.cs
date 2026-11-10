public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    // Вспомогательный метод для получения строки из ListBox
    private string GetSelectedString()
    {
        if (listBox1.SelectedIndex == -1)
        {
            labelResult.Text = "Выберите строку в списке!";
            return null;
        }
        return (string)listBox1.Items[listBox1.SelectedIndex];
    }

    // Задание 1: Посчитать количество нулей и единиц
    private void button1_Click(object sender, EventArgs e)
    {
        string str = GetSelectedString();
        if (str == null) return;

        int zeros = 0;
        int ones = 0;

        foreach (char ch in str)
        {
            if (ch == '0') zeros++;
            else if (ch == '1') ones++;
        }

        labelResult.Text = $"Нулей: {zeros}, Единиц: {ones}";
    }

    // Задание 2: Посчитать количество слов
    private void button2_Click(object sender, EventArgs e)
    {
        string str = GetSelectedString();
        if (str == null) return;

        string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        labelResult.Text = $"Количество слов = {words.Length}";
    }

    // Задание 3: Найти количество знаков препинания
    private void button3_Click(object sender, EventArgs e)
    {
        string str = GetSelectedString();
        if (str == null) return;

        int punctuationCount = 0;
        foreach (char ch in str)
        {
            if (char.IsPunctuation(ch)) punctuationCount++;
        }

        labelResult.Text = $"Знаков препинания = {punctuationCount}";
    }

    // Задание 4: Вывести содержащиеся цифры
    private void button4_Click(object sender, EventArgs e)
    {
        string str = GetSelectedString();
        if (str == null) return;

        StringBuilder digits = new StringBuilder();
        foreach (char ch in str)
        {
            if (char.IsDigit(ch)) digits.Append(ch);
        }

        if (digits.Length > 0)
            labelResult.Text = $"Найденные цифры: {digits.ToString()}";
        else
            labelResult.Text = "Цифры в строке не найдены";
    }

    // Задание 5: Символы на четных местах в обратном порядке
    private void button5_Click(object sender, EventArgs e)
    {
        string str = GetSelectedString();
        if (str == null) return;

        StringBuilder sb = new StringBuilder();
        
        // Собираем символы с четных позиций (индексы 0, 2, 4...)
        for (int i = 0; i < str.Length; i += 2)
        {
            sb.Append(str[i]);
        }

        // Разворачиваем строку
        char[] charArray = sb.ToString().ToCharArray();
        Array.Reverse(charArray);
        string reversedResult = new string(charArray);

        labelResult.Text = $"Результат: {reversedResult}";
    }
}
