
#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent), ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_btnSum_clicked()
{
    bool ok1, ok2;

    double a = ui->lineEditA->text().toDouble(&ok1);
    double b = ui->lineEditB->text().toDouble(&ok2);

    if (ok1 && ok2)
        ui->labelResult->setText(QString::number(a + b));
    else
        ui->labelResult->setText("Ошибка ввода!");
}
