import pytest
from calculator_with_history import CalculatorWithHistory

def test_addition_and_history_saved():
    calc = CalculatorWithHistory()

    result = calc.perform_addition(5,3)

    assert result == 8
    history = calc.get_operation_history()
    assert history == ["5 + 3 -> 8"]

def test_subtraction_and_history_saved():
    calc = CalculatorWithHistory()

    result = calc.perform_subtraction(10, 4)

    assert result == 6
    history = calc.get_opertation_history()
    assert history == ["10 - 4 -> 6"]

def test_multiple_operations_history_order():
    calc = CalculatorWithHistory()

    calc.perform_addition(1, 1)
    calc.perform_subtraction(5, 2)

    history = calc.get_operation_history()

    assert history == ["1 + 1 -> 2", "5 - 2 -> 3"]
    assert len(history) == 2
