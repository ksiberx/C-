#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget(*parent))
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->comboBox->addItem("Минимальное");
    ui->comboBox->addItem("Максимальное");
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    int A[3][3] = {
        {5, 8, 3},
        {9, 1, 7},
        {4, 6, 2}
    };
    ui->label->setText("Массив: \n"
                       " {5, 8, 3}\n"
                       " {9, 1, 7}\n"
                       " {4, 6, 2}\n");
    QString result;

    for (int i = 0; i < 3; i++) {
        int val = A[i][0];

        for (int j = 1; j < 3; j++) {
            if (ui->comboBox->currentIndex() == 0) {
                if (A[i][j] < val) val = A[i][j];
            } else {
                if (A[i][j] > val) val = A[i][j];
            }
        }

        result+= "Строка " + QString::number(i+1) + ": " + QString::number(val) + "\n";
    }
    ui->textEdit->setText(result);
}
