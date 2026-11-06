
import sys
from PyQt5.QtWidgets import *

class Window(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Конвертер температур")

        self.input = QLineEdit()
        self.input.setPlaceholderText("Введите температуру")

        self.mode = QComboBox()
        self.mode.addItems([
            "Цельсий → Фаренгейт",
            "Фаренгейт → Цельсий"
        ])

        self.button = QPushButton("Перевести")
        self.result = QLabel("Результат:")

        layout = QVBoxLayout(self)
        layout.addWidget(self.input)
        layout.addWidget(self.mode)
        layout.addWidget(self.button)
        layout.addWidget(self.result)

        self.button.clicked.connect(self.convert)
        self.input.textChanged.connect(
            lambda text: self.button.setEnabled(bool(text.strip()))
        )

    def convert(self):
        try:
            value = float(self.input.text())

            if self.mode.currentIndex() == 0:
                result = value * 9 / 5 + 32
            else:
                result = (value - 32) * 5 / 9

            self.result.setText(f"Результат: {result:.2f}")
        except ValueError:
            self.result.setText("Ошибка ввода!")

app = QApplication(sys.argv)
window = Window()
window.show()
sys.exit(app.exec_())
