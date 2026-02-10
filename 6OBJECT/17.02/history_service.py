class HistoryService:
    def __init__(self):
        self.history = []

def save_record(self, operation: str, result: int) -> None:
    record = f"{operation} {result}"
    self.history.append(record)

def get_history(self) -> list:
    return self._history.copy()
