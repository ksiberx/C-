#ifndef FILEFACTORY_H
#define FILEFACTORY_H

#include <QString>
#include <QFile>
#include <QFileDialog>
#include <QTextStream>
#include <QMessageBox>

class File {
public:
    virtual void action(QString &text, QString &path) = 0;
    virtual ~File() {}
};

class CreateFile : public File {
public:
    void action(QString &text, QString &path) override {
        text.clear();
        path = "";
        QMessageBox::information(nullptr, "Файл", "Создан новый файл");
    }
};

class OpenFile : public File {
public:
    void action(QString &text, QString &path) override {
        path = QFileDialog::getOpenFileName(nullptr, "Открыть файл");

        if (path.isEmpty()) return;

        QFile file(path);
        if (file.open(QIODevice::ReadOnly)) {
            QTextStream in(&file);
            text = in.readAll();
            file.close();
        }
    }
};

class SaveFile : public File {
public:
    void action(QString &text, QString &path) override {
        if (path.isEmpty()) {
            path = QFileDialog::getSaveFileName(nullptr, "Сохранить файл");
        }

        QFile file(path);
        if (file.open(QIODevice::WriteOnly)) {
            QTextStream out(&file);
            out << text;
            file.close();
            QMessageBox::information(nullptr, "Файл", "Файл сохранён");
        }
    }
};

class PrintFile : public File {
public:
    void action(QString &, QString &) override {
        QMessageBox::information(nullptr, "Печать", "Отправлено на печать");
    }
};

class CloseFile : public File {
public:
    void action(QString &text, QString &path) override {
        text.clear();
        path = "";
        QMessageBox::information(nullptr, "Файл", "Файл закрыт");
    }
};

class Creator {
public:
    void execute(QString &text, QString &path) {
        File* file = createFile(); // factoory Method
        file->action(text, path);
        delete file;
    }

protected:
    virtual File* createFile() = 0;
};


class CreateCreator : public Creator {
protected:
    File* createFile() override { return new CreateFile(); }
};

class OpenCreator : public Creator {
protected:
    File* createFile() override { return new OpenFile(); }
};

class SaveCreator : public Creator {
protected:
    File* createFile() override { return new SaveFile(); }
};

class PrintCreator : public Creator {
protected:
    File* createFile() override { return new PrintFile(); }
};

class CloseCreator : public Creator {
protected:
    File* createFile() override { return new CloseFile(); }
};

#endif
