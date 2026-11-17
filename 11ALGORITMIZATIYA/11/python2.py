import sys
from PyQt5.QtWidgets import QApplication, QWidget, QVBoxLayout, QPushButton

class ColorSwitcherWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Переключатель цвета")
        self.resize(300, 200)
        self.setStyleSheet("background-color: white;")

        layout = QVBoxLayout()

        buttons = ["Красный", "Зелёный", "Синий", "Сброс"]
        for text in buttons:
            btn = QPushButton(text)
            # Возвращаем кнопкам стандартный стиль, чтобы они не сливались с фоном окна
            btn.setStyleSheet("background-color: #f0f0f0; color: black;")
            btn.clicked.connect(self.change_color)
            layout.addWidget(btn)

        self.setLayout(layout)

    def change_color(self):
        clicked_button = self.sender()
        if not clicked_button:
            return

        color_map = {
            "Красный": "#ffcccc",
            "Зелёный": "#ccffcc",
            "Синий": "#ccccff",
            "Сброс": "white"
        }

        chosen_color = color_map.get(clicked_button.text(), "white")
        self.setStyleSheet(f"background-color: {chosen_color};")

if __name__ == "__main__":
    app = QApplication(sys.argv)
    window = ColorSwitcherWindow()
    window.show()
    sys.exit(app.exec_())
