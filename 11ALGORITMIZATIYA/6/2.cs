
// ЗАДАНИЕ №2 (Матрица 3х3)
private void btnMatrixTask2_Click(object sender, EventArgs e)
{
    int[,] A = new int[3, 3];
    Random rand = new Random();
    
    // Настройка таблицы
    dataGridView1.RowCount = 3;
    dataGridView1.ColumnCount = 3;

    // Заполнение и вывод
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            A[i, j] = rand.Next(1, 10);
            dataGridView1.Rows[i].Cells[j].Value = A[i, j].ToString();
        }
    }

    // Вычисления (индексация начинается с 0, поэтому 2-я строка — это индекс 1, 1-й столбец — индекс 0)
    int sumRow2 = 0;
    for (int j = 0; j < 3; j++)
    {
        sumRow2 += A[1, j];
    }

    int mulCol1 = 1;
    for (int i = 0; i < 3; i++)
    {
        mulCol1 *= A[i, 0];
    }

    MessageBox.Show($"Сумма 2-й строки: {sumRow2}\nПроизведение 1-го столбца: {mulCol1}");
}

// ЗАДАНИЕ №5 (Матрица 7х7)
private void btnMatrixTask5_Click(object sender, EventArgs e)
{
    int[,] F = new int[7, 7];
    Random rand = new Random();
    
    dataGridView1.RowCount = 7;
    dataGridView1.ColumnCount = 7;
    listBox1.Items.Clear();

    // Заполнение
    for (int i = 0; i < 7; i++)
    {
        for (int j = 0; j < 7; j++)
        {
            F[i, j] = rand.Next(-50, 50);
            dataGridView1.Rows[i].Cells[j].Value = F[i, j].ToString();
        }
    }

    // Поиск минимума во внешнем цикле по столбцам (j), во внутреннем по строкам (i)
    for (int j = 0; j < 7; j++)
    {
        int minInCol = F[0, j];
        for (int i = 1; i < 7; i++)
        {
            if (F[i, j] < minInCol)
            {
                minInCol = F[i, j];
            }
        }
        listBox1.Items.Add($"Столбец {j + 1}: минимум = {minInCol}");
    }
}

// ЗАДАНИЕ №11 (Матрица 12х12)
private void btnMatrixTask11_Click(object sender, EventArgs e)
{
    int[,] A = new int[12, 12];
    Random rand = new Random();
    
    dataGridView1.RowCount = 12;
    dataGridView1.ColumnCount = 12;

    int sumAboveDiagonal = 0;

    for (int i = 0; i < 12; i++)
    {
        for (int j = 0; j < 12; j++)
        {
            A[i, j] = rand.Next(1, 10);
            dataGridView1.Rows[i].Cells[j].Value = A[i, j].ToString();

            // Элементы над главной диагональю удовлетворяют условию j > i
            if (j > i)
            {
                sumAboveDiagonal += A[i, j];
            }
        }
    }

    MessageBox.Show($"Сумма элементов над главной диагональю: {sumAboveDiagonal}");
}
