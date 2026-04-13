#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <ctime>
using namespace std;
MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    srand(time(0)); // генератор чисел

    for(int i = 0; i < 10; i++) {
        list.append(rand() % 50 - 20);
    }
    for(int x : list)
        ui->textEdit->append(QString::number(x));
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    // сортировка
    sort(list.begin(), list.end());

    // очищение вывода
    ui->textEdit->clear();

    for(int x : list)
        ui->textEdit->append(QString::number(x));
}

