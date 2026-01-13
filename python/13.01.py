"1 задание"
def greedy_gas_station(distances, tank):
    n = len(distances)
    current_fuel = tank
    stops = []

    for i in range(n):
        #если до следующей заправки не дотянуть
        if distances[i] > current_fuel:
            stops.append(i)  #заправляемся на текущей
            current_fuel = tank  #залили полный бак

        #едем до следующей заправки
        current_fuel -= distances[i]

    return stops


# Примерчик
dist = [200, 150, 300, 250, 100]
tank = 400

print("Эвристика: жадный выбор заправок")
print("Расстояния:", dist)
print("Бак (км):", tank)
print("Заправки (номера, начиная с 0):", greedy_gas_station(dist, tank))


"2 задание"
def greedy_meetings(meetings):
    sorted_meet = sorted(meetings, key=lambda x: x[1])

    selected = []
    last_end = -1

    for start, end in sorted_meet:
        if start >= last_end:
            selected.append((start, end))
            last_end = end

    return selected

# Пример
meetings = [(9, 10), (10, 12), (11, 13), (12, 14), (13, 15)]
print("\nЭвристика: выбор заседаний")
print("Все заседания:", meetings)
print("Максимум можно посетить:", greedy_meetings(meetings))
