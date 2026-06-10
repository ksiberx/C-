#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QPainter>                     // для рисования
#include <QRandomGenerator>             // генератор случайных чисел
#include <QTimer>
#include <cmath>                        // cos() и sin()

using namespace std;

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    sec = 0;                            // начальное значение секунд

    timer = new QTimer(this);           // создаем таймер

    connect(timer, &QTimer::timeout, this, [=]()
            {
                sec++;                          // увеличиваем секунды

                if(sec >= 60)                   // если дошли до 60
                    sec = 0;                    // начинаем заново

                update();                       // перерисовываем окно
            });
}

MainWindow::~MainWindow()
{
    delete ui;                          // освобождаем память
}

void MainWindow::on_pushButtonRandom_clicked()
{
    int a[20];

    for(int i = 0; i < 20; i++)
        a[i] = i + 1;                   // записываем числа от 1 до 20

    for(int i = 19; i > 0; i--)
    {
        int j = QRandomGenerator::global()->bounded(i + 1);

        int temp = a[i];                // меняем элементы местами
        a[i] = a[j];
        a[j] = temp;
    }

    QString text;                       // строка для вывода

    for(int i = 0; i < 20; i++)
        text += QString::number(a[i]) + " ";

    ui->textEdit->setText(text);        // выводим результат в TextEdit
}

// 2 ЗАДАНИЕ

void MainWindow::on_pushButtonClock_clicked()
{
    timer->start(1000);                 // запуск таймера раз в секунду
}

void MainWindow::paintEvent(QPaintEvent *)
{
    QPainter p(this);                   // объект для рисования

    QPoint center(450,250);             // центр часов

    p.drawEllipse(center,100,100);      // рисуем окружность часов

    double angle = sec * 6 - 90;        // угол стрелки

    int x = center.x() + 80 * cos(angle * 3.14159 / 180);
    int y = center.y() + 80 * sin(angle * 3.14159 / 180);

    p.drawLine(center, QPoint(x,y));    // рисуем секундную стрелку
}
