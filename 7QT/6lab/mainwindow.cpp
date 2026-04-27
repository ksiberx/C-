#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QFile>
#include <QTextStream>
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
    QFile f1("C:\\Users\\murtm\\Downloads\\f1.txt");
    QFile f2("C:\\Users\\murtm\\Downloads\\f2.txt");
    QFile h("C:\\Users\\murtm\\Downloads\\h.txt");

    // Проверка существования
    if (!f1.exists() || !f2.exists()) {
        QMessageBox::warning(this, "Ошибка", "Один из файлов не найден");
        return;
    }

    // Открываем файлы для чтения
    if (!f1.open(QIODevice::ReadOnly) || !f2.open(QIODevice::ReadOnly)) {
        QMessageBox::warning(this, "Ошибка", "Ошибка открытия файлов для чтения");
        return;
    }

    QString data1 = f1.readAll();
    QString data2 = f2.readAll();

    f1.close();
    f2.close();

    // Записываем f1 - hы
    if (!h.open(QIODevice::WriteOnly)) {
        QMessageBox::warning(this, "Ошибка", "Ошибка открытия временного файла");
        return;
    }
    QTextStream streamH(&h);
    streamH << data1;
    h.close();

    // f2 - f1
    if (!f1.open(QIODevice::WriteOnly)) {
        QMessageBox::warning(this, "Ошибка", "Ошибка записи в f1");
        return;
    }
    QTextStream stream1(&f1);
    stream1 << data2;
    f1.close();

    // h - f2
    if (!h.open(QIODevice::ReadOnly)) {
        QMessageBox::warning(this, "Ошибка", "Ошибка чтения временного файла");
        return;
    }

    QString temp = h.readAll();
    h.close();

    if (!f2.open(QIODevice::WriteOnly)) {
        QMessageBox::warning(this, "Ошибка", "Ошибка записи в f2");
        return;
    }
    QTextStream stream2(&f2);
    stream2 << temp;
    f2.close();

    ui->textEdit->setText("Файлы успешно обменяны!");
}
