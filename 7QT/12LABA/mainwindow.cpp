#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QRegularExpression>
#include <QMessageBox>

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

void MainWindow::on_pushButton_clicked()
{
    QString login = ui->lineEdit->text();

    QRegularExpression rx("^[A-Za-z0-9]{4,10}$");

    if(rx.match(login).hasMatch())
    {
        QMessageBox::information(this,
                                 "Авторизация",
                                 "Доступ разрешен");
    }
    else
    {
        QMessageBox::warning(this,
                             "Ошибка",
                             "Неверный логин");
    }
}
