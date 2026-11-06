
import sys
from PyQt5.QtWidgets import *

class Window(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Счётчик")

        self.count = 0
        self.label = QLabel("Нажатий: 0")
        self.button = QPushButton("Нажми меня")

        layout = QVBoxLayout(self)
        layout.addWidget(self.label)
        layout.addWidget(self.button)

        self.button.clicked.connect(self.click)

    def click(self):
        self.count += 1
        self.label.setText(f"Нажатий: {self.count}")

        if self.count >= 10:
            self.button.setEnabled(False)

app = QApplication(sys.argv)
window = Window()
window.show()
sys.exit(app.exec_())
