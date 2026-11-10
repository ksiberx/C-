// ОБЩИЕ ПЕРЕМЕННЫЕ ДЛЯ РАЗДЕЛА 1
int[] Mas1; // Массив для задания №4 (на 25 элементов)
int[] Mas2; // Массив для заданий №5 и №12 (на 30 и 14 элементов)

// Вспомогательная кнопка генерации массивов (для тестов)
private void btnGeneratePart1_Click(object sender, EventArgs e)
{
    Random rand = new Random();
    listBox1.Items.Clear();
    
    // Генерируем массив для Задания 4 (25 элементов)
    Mas1 = new int[25];
    for (int i = 0; i < 25; i++) 
    {
        Mas1[i] = rand.Next(-20, 20);
        listBox1.Items.Add($"[№4] Исходный Mas1[{i}] = {Mas1[i]}");
    }
    
    // Генерируем массив для Заданий 5 и 12 (30 элементов)
    Mas2 = new int[30];
    for (int i = 0; i < 30; i++) 
    {
        Mas2[i] = rand.Next(-10, 50);
    }
}

// ЗАДАНИЕ №4 (Массив из 25 элементов)
private void btnTask4_Click(object sender, EventArgs e)
{
    if (Mas1 == null) return;
    listBox2.Items.Clear();

    for (int i = 0; i < 25; i++)
    {
        if (Mas1[i] < 0)
        {
            Mas1[i] = Mas1[i] * Mas1[i]; // Квадрат значения
        }
        else if (Mas1[i] > 0)
        {
            Mas1[i] += 7; // Увеличиваем на 7
        }
        // Нулевые остаются без изменений

        listBox2.Items.Add($"Mas1[{i}] = {Mas1[i]}");
    }
}

// ЗАДАНИЕ №5 (Массив из 30 элементов)
private void btnTask5_Click(object sender, EventArgs e)
{
    if (Mas2 == null) return;
    int sum = 0;

    for (int i = 0; i < 30; i++)
    {
        if (Mas2[i] % 5 == 0)
        {
            sum += Mas2[i];
        }
    }
    MessageBox.Show($"Сумма элементов, кратных 5: {sum}");
}

// ЗАДАНИЕ №12 (Массив из 14 элементов)
private void btnTask12_Click(object sender, EventArgs e)
{
    if (Mas2 == null) return;
    int sum = 0;
    bool foundNegative = false;

    // Выводим первые 14 элементов исходного массива для наглядности
    listBox2.Items.Clear();
    for(int i = 0; i < 14; i++)
    {
        listBox2.Items.Add($"Mas2[{i}] = {Mas2[i]}");
    }

    // Считаем сумму до первого отрицательного
    for (int i = 0; i < 14; i++)
    {
        if (Mas2[i] < 0)
        {
            foundNegative = true;
            break; // Выходим из цикла при первом отрицательном
        }
        sum += Mas2[i];
    }

    string msg = foundNegative 
        ? $"Сумма элементов до первого отрицательного: {sum}" 
        : $"Отрицательных элементов нет. Сумма всех 14 элементов: {sum}";
        
    MessageBox.Show(msg);
}
