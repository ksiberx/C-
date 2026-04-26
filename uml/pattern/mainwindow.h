#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class Creator;

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_btnCreate_clicked();
    void on_btnOpen_clicked();
    void on_btnSave_clicked();
    void on_btnPrint_clicked();
    void on_btnClose_clicked();

private:
    Ui::MainWindow *ui;
    QString currentPath;

    void run(Creator* creator);
};

#endif
