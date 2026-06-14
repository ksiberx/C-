#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QtSql>
#include <QDebug>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    QSqlDatabase db = QSqlDatabase::addDatabase("QSQLITE");
    db.setDatabaseName("abonents.db");

    if(db.open())
        qDebug() << "База подключена";
    else
        qDebug() << "Ошибка подключения";
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    QString name = ui->lineEdit->text();

    QSqlQuery query;
    query.prepare("SELECT * FROM abonents WHERE name = ?");
    query.addBindValue(name);

    ui->textEdit->clear();

    if(query.exec())
    {
        while(query.next())
        {
            ui->textEdit->append(
                query.value(0).toString() + " | " +
                query.value(1).toString() + " | " +
                query.value(2).toString()
                );
        }
    }
}
