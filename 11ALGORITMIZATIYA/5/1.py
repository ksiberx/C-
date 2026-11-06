
import sys
from PyQt5 import uic
from PyQt5.QtWidgets import QApplication, QMainWindow

class Window(QMainWindow):
    def __init__(self):
        super().__init__()
        uic.loadUi("form.ui", self)
        self.btnOK.clicked.connect(self.change_text)

    def change_text(self):
        self.label.setText("Кнопка нажата!")

app = QApplication(sys.argv)
window = Window()
window.show()
sys.exit(app.exec_())
