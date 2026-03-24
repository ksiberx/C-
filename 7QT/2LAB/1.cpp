#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *id871537943 (*parent))
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    // 1 Задание
    float a = ui->lineEdit->text().toFloat();
    float b = ui->lineEdit_2->text().toFloat();
    float c = ui->lineEdit_3->text().toFloat();

    if (a >= b && a >= c) {
        a = b - c;
        ui->label->setText("a = " + QString::number(a));
    }

    else if (b >= a && b >= c) {
        b = a - c;
        ui->label->setText(" b = " + QString::number(b));
    }

    else {
        c = a - b;
        ui->label->setText(" c = " + QString::number(c));
    }

    // 2
}
