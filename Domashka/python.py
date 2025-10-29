# --- пузырьковая сортировка по убыванию ---
def bubble_sort_down(lst):
    size = len(lst)
    for end in range(size - 1):
        for k in range(size - end - 1):
            if lst[k] < lst[k + 1]:  # сравниваем и меняем местами
                lst[k], lst[k + 1] = lst[k + 1], lst[k]
    return lst


# --- проверка: упорядочен ли список по убыванию, если нет — сортируем ---
def ensure_desc_sorted(lst):
    ordered = True
    for i in range(len(lst) - 1):
        if lst[i] < lst[i + 1]:
            ordered = False
            break

    if ordered:
        return lst
    else:
        return bubble_sort_down(lst[:])  # сортируем копию


# --- сортировка элементов на четных позициях по возрастанию ---
def bubble_sort_even_positions(lst):
    # собираем элементы с четных индексов
    even_part = [lst[i] for i in range(0, len(lst), 2)]

    # сортировка пузырьком (по возрастанию)
    for i in range(len(even_part) - 1):
        for j in range(len(even_part) - i - 1):
            if even_part[j] > even_part[j + 1]:
                even_part[j], even_part[j + 1] = even_part[j + 1], even_part[j]

    # вставляем обратно
    new_list = lst[:]
    pos = 0
    for i in range(len(new_list)):
        if i % 2 == 0:
            new_list[i] = even_part[pos]
            pos += 1

    return new_list


# --- четные позиции по убыванию, нечетные по возрастанию ---
def mixed_sort(lst):
    odds = [lst[i] for i in range(0, len(lst), 2)]
    evens = [lst[i] for i in range(1, len(lst), 2)]

    odds.sort()            # по возрастанию
    evens.sort(reverse=True)  # по убыванию

    res = []
    o = e = 0
    for i in range(len(lst)):
        if i % 2 == 0:
            res.append(odds[o])
            o += 1
        else:
            res.append(evens[e])
            e += 1
    return res


# --- удаление повторяющихся элементов ---
def unique_elements(lst):
    seen = []
    for item in lst:
        if item not in seen:
            seen.append(item)
    return seen


# --- сортировка массива, содержащего только 0, 1 и 2 ---
def sort_zero_one_two(lst):
    zeros = lst.count(0)
    ones = lst.count(1)
    twos = lst.count(2)

    # порядок: 1, 2, 0 (как в твоем исходнике)
    return [1] * ones + [2] * twos + [0] * zeros


# --- тестирование ---
if __name__ == "__main__":
    arr1 = [4, 9, 7, 6, 2, 3]
    arr2 = [30, 53, 11, 35, 17, 42, 21, 84, 75, 61]
    arr3 = [1, 2, 2, 0, 1, 0, 2, 1]

    print("Исходный список:", arr1)
    print()

    print("1) Пузырьковая сортировка по убыванию:", bubble_sort_down(arr1[:]))
    print("2) Проверка и сортировка:", ensure_desc_sorted(arr1[:]))
    print("3) Сортировка четных индексов:", bubble_sort_even_positions(arr1[:]))
    print()

    print("Доп 1) Чет/нечет спец-сортировка:", mixed_sort(arr1[:]))
    print("Доп 2) Без повторов:", unique_elements([1, 2, 2, 3, 3, 3, 4, 5, 5]))
    print("Доп 3) Сортировка 0-1-2:", sort_zero_one_two(arr3[:]))
