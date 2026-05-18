#include "mainwindow.h"
#include "./ui_mainwindow.h"
#include <cmath>
#include <QString>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

// цифры
void MainWindow::on_pushButton_clicked()
{
    ui->lineEdit->insert("1");
}
void MainWindow::on_pushButton_2_clicked()
{
    ui->lineEdit->insert("2");
}
void MainWindow::on_pushButton_3_clicked()
{
    ui->lineEdit->insert("3");
}
void MainWindow::on_pushButton_6_clicked()
{
    ui->lineEdit->insert("4");
}
void MainWindow::on_pushButton_7_clicked()
{
    ui->lineEdit->insert("5");
}
void MainWindow::on_pushButton_8_clicked()
{
    ui->lineEdit->insert("6");
}
void MainWindow::on_pushButton_11_clicked()
{
    ui->lineEdit->insert("7");
}
void MainWindow::on_pushButton_12_clicked()
{
    ui->lineEdit->insert("8");
}
void MainWindow::on_pushButton_13_clicked()
{
    ui->lineEdit->insert("9");
}
void MainWindow::on_pushButton_18_clicked()
{
    ui->lineEdit->insert("0");
}

// ариф. операции
void MainWindow::on_pushButton_4_clicked()
{
    firstNumber = ui->lineEdit->text().toDouble();
    operation = "+";
    ui->lineEdit->clear();
    ui->lineEdit_2->setText(QString::number(firstNumber) + " +");
}

void MainWindow::on_pushButton_5_clicked()
{
    firstNumber = ui->lineEdit->text().toDouble();
    operation = "-";
    ui->lineEdit->clear();
    ui->lineEdit_2->setText(QString::number(firstNumber) + " -");
}
void MainWindow::on_pushButton_9_clicked()
{
    firstNumber = ui->lineEdit->text().toDouble();
    operation = "*";
    ui->lineEdit->clear();
    ui->lineEdit_2->setText(QString::number(firstNumber) + " *");
}
void MainWindow::on_pushButton_10_clicked()
{
    firstNumber = ui->lineEdit->text().toDouble();
    operation = "/";
    ui->lineEdit->clear();
    ui->lineEdit_2->setText(QString::number(firstNumber) + " /");
}

// функции
void MainWindow::calculate()
{
    //Проверка наличия операции
    if (operation.isEmpty()) return;

    //Второе число
    double twoNumber = ui->lineEdit->text().toDouble();
    double result = 0;

    //Вычисление
    if (operation == "+") {
        result = firstNumber + twoNumber;
    }
    else if (operation == "-") {
        result = firstNumber - twoNumber;
    }
    else if (operation == "*") {
        result = firstNumber * twoNumber;
    }
    else if (operation == "/") {
        if (twoNumber == 0) {
            ui->lineEdit->setText("Ошибка!");
            return;
        }
        result = firstNumber / twoNumber;
    }

    // вывод
    ui->lineEdit->setText(QString::number(result));
    ui->lineEdit_2->setText(QString::number(firstNumber) + " " + operation + " " + QString::number(twoNumber) + " =");

    // очистка
    operation = "";
}

// вывод результата
void MainWindow::on_pushButton_15_clicked()
{
    calculate();
}

// очистка строки
void MainWindow::on_pushButton_16_clicked()
{
    ui->lineEdit->setText("0");
    ui->lineEdit_2->clear();
    firstNumber = 0;
    operation = "";
}

// смена знака
void MainWindow::on_pushButton_14_clicked()
{
    double numbers = ui->lineEdit->text().toDouble();
    numbers = -numbers;
    ui->lineEdit->setText(QString::number(numbers));
}

// точка
void MainWindow::on_pushButton_17_clicked()
{
    // eсли точка есть
    if(ui->lineEdit->text().contains('.')){
        return;
    }

    //e сли поле пустое
    if(ui->lineEdit->text().isEmpty()){
        ui->lineEdit->setText("0.");
    }

    else{
        ui->lineEdit->setText(ui->lineEdit->text() + ".");
    }
}

// sin
void MainWindow::on_pushButton_19_clicked()
{
    double numbers = ui->lineEdit->text().toDouble();
    double result = sin(numbers);
    ui->lineEdit->setText(QString::number(result));
    ui->lineEdit_2->setText("sin(" + QString::number(numbers) + ") =");
}

// ln (натуральный логарифм)
void MainWindow::on_pushButton_20_clicked()
{
    double numbers = ui->lineEdit->text().toDouble();

    if(numbers <= 0){
        ui->lineEdit->setText("Ошибка!");
        ui->lineEdit_2->setText("ln(" + QString::number(numbers) + ") = Неопределенно!");
        return;
    }

    double result = log(numbers);
    ui->lineEdit->setText(QString::number(result));
    ui->lineEdit_2->setText("ln(" + QString::number(numbers) + ") =");
}

// x^y
void MainWindow::on_pushButton_21_clicked()
{
    double base = ui->lineEdit->text().toDouble();
    double exponent = ui->lineEdit_2->text().toDouble();

    double result = pow(base, exponent);
    ui->lineEdit->setText(QString::number(result));
    ui->lineEdit_2->setText("(" + QString::number(base) + ")^(" + QString::number(exponent) + ") =");
}

// 1/x
void MainWindow::on_pushButton_22_clicked()
{
    double numbers = ui->lineEdit->text().toDouble();

    if(numbers == 0){
        ui->lineEdit->setText("Ошибка!");
        ui->lineEdit_2->setText("1/0 = Неопределенно!");
        return;
    }

    double result = 1.0 / numbers;
    ui->lineEdit->setText(QString::number(result));
    ui->lineEdit_2->setText("1/(" + QString::number(numbers) + ") =");
}
