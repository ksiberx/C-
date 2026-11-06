
void MainWindow::on_btnCheck_clicked()
{
    bool ok;
    int number = ui->lineEditNumber->text().toInt(&ok);

    if (!ok)
        ui->labelResult->setText("Ошибка ввода!");
    else if (number % 2 == 0)
        ui->labelResult->setText("Чётное");
    else
        ui->labelResult->setText("Нечётное");
}
