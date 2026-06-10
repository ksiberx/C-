#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QFileDialog>
#include <QFile>
#include <QFileInfo>         // информация о файле
#include <QTextStream>       // чтение текста из файла

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;               // освобождаем память
}

void MainWindow::on_pushButton_clicked()
{
    QString fileName = QFileDialog::getOpenFileName(
        this,
        "Выберите файл",
        "",
        "Text files (*.txt);;All files (*.*)"); // фильтр файлов

    if(fileName.isEmpty())
        return;

    QFile file(fileName);    // создаем объект файла

    if(file.open(QIODevice::ReadOnly | QIODevice::Text)) // открываем для чтения
    {
        QTextStream in(&file);

        ui->textEdit->setPlainText(
            in.readAll());           // читаем весь текст и выводим

        file.close();
    }

    QFileInfo info(fileName); // получаем информацию о файле

    QString text;

    text += "Имя: " + info.fileName() + "\n"; // имя файла

    text += "Дата создания: "
            + info.birthTime().toString()
            + "\n";                          // дата создания

    ui->label->setText(text); // вывод информации на форму
}
