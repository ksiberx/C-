#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    srand(time(0));

    for(int i = 0; i < 5; i++) {
        list1.append(rand() % 50);
        list2.append(rand() % 50);
    }

    ui->textEdit->append("Список 1:");
    for(int x : list1)
        ui->textEdit->append(QString::number(x));

    ui->textEdit_2->append("Список 2:");
    for(int x : list2)
        ui->textEdit_2->append((QString::number(x)));
}

MainWindow::~MainWindow()
{
    delete ui;
}
void MainWindow::on_pushButton_clicked()
{
    int num = ui->lineEdit->text().toInt();

    if(ui->checkBox->isChecked()) {
        list1.append(num);
    }

    if(ui->checkBox_2->isChecked()) {
        list2.append(num);
    }

    ui->textEdit->clear();
    ui->textEdit_2->clear();

    ui->textEdit->append("Список 1:");
    for(int x : list1)
        ui->textEdit->append(QString::number(x));

    ui->textEdit_2->append("Список 2:");
    for(int x : list2)
        ui->textEdit_2->append(QString::number(x));
}

