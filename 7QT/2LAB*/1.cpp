#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget(*parent))
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
    int A[] = {5, 91, 10, 21, 23, 33, 41, 51, 72, 83};
    int result = 0;

    ui->label_2->setText("Массив: [5, 9, 10, 21, 23, 33, 41, 51, 72, 83]");

    for (int i = 0; i < 10; i++) {
        if (A[0] < A[i] && A[i] < A[9]) {
            result = i + 1;
            break;
        }
    }
    ui->label->setText("Номер элемента: " + QString::number(result));
}
