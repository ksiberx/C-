import sys
from PyQt5.QtWidgets import (
    QApplication, QWidget, QVBoxLayout, QLineEdit, 
    QPushButton, QLabel, QMessageBox
)

class CalculatorWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Калькулятор на два поля")
        self.resize(300, 180)

        layout = QVBoxLayout()

        self.input1 = QLineEdit()
        self.input1.setPlaceholderText("Введите первое число")
        layout.addWidget(self.input1)

        self.input2 = QLineEdit()
        self.input2.setPlaceholderText("Введите второе число")
        layout.addWidget(self.input2)

        self.btn_add = QPushButton("Сложить")
        self.btn_add.clicked.connect(self.calculate_sum)
        layout.addWidget(self.btn_add)

        self.result_label = QLabel("Результат: ")
        layout.addWidget(self.result_label)

        self.setLayout(layout)

    def calculate_sum(self):
        try:
            num1 = float(self.input1.text().replace(',', '.'))
            num2 = float(self.input2.text().replace(',', '.'))
            total = num1 + num2
            # Убираем .0, если число целое
            formatted_total = int(total) if total.is_integer() else total
            self.result_label.setText(f"Результат: {formatted_total}")
        except ValueError:
            QMessageBox.warning(self, "Ошибка ввода", "Пожалуйста, введите корректные числа!")

if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = CalculatorWindow()
    window.show()
    sys.exit(app.exec_())
