#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "filefactory.h"

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

void MainWindow::run(Creator* creator)
{
    QString text = ui->textEdit->toPlainText();

    creator->execute(text, currentPath);

    ui->textEdit->setPlainText(text);

    delete creator;
}

void MainWindow::on_btnCreate_clicked() { run(new CreateCreator()); }
void MainWindow::on_btnOpen_clicked()   { run(new OpenCreator()); }
void MainWindow::on_btnSave_clicked()   { run(new SaveCreator()); }
void MainWindow::on_btnPrint_clicked()  { run(new PrintCreator()); }
void MainWindow::on_btnClose_clicked()  { run(new CloseCreator()); }
