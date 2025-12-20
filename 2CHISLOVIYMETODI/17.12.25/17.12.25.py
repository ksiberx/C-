import math

class ODESolver:
    def euler(self, f, x0, y0, n, h):
        x_vals = [x0]
        y_vals = [y0]
        x, y = x0, y0

        for _ in range(n):
            y = y + h * f(x, y)
            x = x + h
            x_vals.append(x)
            y_vals.append(y)

        return x_vals, y_vals

    def euler_cauchy(self, f, x0, y0, n, h):
        x_vals = [x0]
        y_vals = [y0]
        x, y = x0, y0

        for _ in range(n):
            y_pred = y + h * f(x, y)
            x_new = x + h
            y = y + (h / 2) * (f(x, y) + f(x_new, y_pred))
            x = x_new
            x_vals.append(x)
            y_vals.append(y)

        return x_vals, y_vals

    def runge_kutta_4(self, f, x0, y0, n, h):
        x_vals = [x0]
        y_vals = [y0]
        x, y = x0, y0

        for _ in range(n):
            k1 = h * f(x, y)
            k2 = h * f(x + h / 2, y + k1 / 2)
            k3 = h * f(x + h / 2, y + k2 / 2)
            k4 = h * f(x + h, y + k3)
            y = y + (1 / 6) * (k1 + 2 * k2 + 2 * k3 + k4)
            x = x + h
            x_vals.append(x)
            y_vals.append(y)

        return x_vals, y_vals

    def solve(self, method, f, x0, y0, xn, n):
        h = (xn - x0) / n

        if method == 1:
            return self.euler(f, x0, y0, n, h)
        elif method == 2:
            return self.euler_cauchy(f, x0, y0, n, h)
        elif method == 3:
            return self.runge_kutta_4(f, x0, y0, n, h)
        else:
            raise ValueError("ÐÐµÐºÐ¾ÑÑÐµÐºÑÐ½ÑÐ¹ Ð¼ÐµÑÐ¾Ð´")


def print_results(x_vals, y_vals, name):
    print(f"ÐÐµÑÐ¾Ð´: {name}")
    print(f"{'i':<5} {'x':<15} {'y(x)':<15}")

    for i, (x, y) in enumerate(zip(x_vals, y_vals)):
        print(f"{i:<5} {x:<15.6f} {y:<15.6f}")


def get_function():
    print("\n1. y' = x + y")
    print("2. y' = x - y")
    print("3. y' = x * y")
    print("4. y' = sin(x) + cos(y)")
    print("5. y' = exp(x) - y")
    print("6. Ð¡Ð²Ð¾Ñ ÑÑÐ½ÐºÑÐ¸Ñ")

    choice = int(input("ÐÑÐ±ÐµÑÐ¸ÑÐµ (1-6): "))

    if choice == 1:
        return lambda x, y: x + y
    elif choice == 2:
        return lambda x, y: x - y
    elif choice == 3:
        return lambda x, y: x * y
    elif choice == 4:
        return lambda x, y: math.sin(x) + math.cos(y)
    elif choice == 5:
        return lambda x, y: math.exp(x) - y
    elif choice == 6:
        func_str = input("ÐÐ²ÐµÐ´Ð¸ÑÐµ f(x, y): ")
        return lambda x, y: eval(func_str)
    else:
        return lambda x, y: x + y


def main():
    solver = ODESolver()

    while True:
        print("Ð ÐÐ¨ÐÐÐÐ ÐÐÐ£")
        print("1. ÐÐµÑÐ¾Ð´ Ð­Ð¹Ð»ÐµÑÐ°")
        print("2. ÐÐµÑÐ¾Ð´ Ð­Ð¹Ð»ÐµÑÐ°-ÐÐ¾ÑÐ¸")
        print("3. ÐÐµÑÐ¾Ð´ Ð ÑÐ½Ð³Ðµ-ÐÑÑÑÐ° 4")
        print("4. Ð¡ÑÐ°Ð²Ð½Ð¸ÑÑ Ð²ÑÐµ")
        print("5. ÐÑÑÐ¾Ð´")

        try:
            choice = int(input("ÐÑÐ±ÐµÑÐ¸ÑÐµ (1-5): "))

            if choice == 5:
                break

            if choice not in [1, 2, 3, 4]:
                continue

            print("\nÐÐ°ÑÐ°Ð¼ÐµÑÑÑ:")
            x0 = float(input("x0: "))
            y0 = float(input(f"y({x0}): "))
            xn = float(input("xn: "))
            n = int(input("n: "))

            if n <= 0:
                continue

            f = get_function()

            if choice == 4:
                print("Ð¡Ð ÐÐÐÐÐÐÐ")

                results = []
                methods = [("Ð­Ð¹Ð»ÐµÑ", 1), ("Ð­Ð¹Ð»ÐµÑ-ÐÐ¾ÑÐ¸", 2), ("Ð ÑÐ½Ð³Ðµ-ÐÑÑÑÐ° 4", 3)]

                for name, num in methods:
                    x_vals, y_vals = solver.solve(num, f, x0, y0, xn, n)
                    results.append((name, x_vals, y_vals))

                print(f"\n{'i':<5} {'x':<10} ", end="")
                for name, _, _ in results:
                    print(f"{name:<20} ", end="")
                print()

                for i in range(n + 1):
                    x_val = x0 + i * (xn - x0) / n
                    print(f"{i:<5} {x_val:<10.6f} ", end="")
                    for _, _, y_vals in results:
                        print(f"{y_vals[i]:<20.10f} ", end="")
                    print()

                print("\nÐ ÐÐÐÐÐ¡Ð¢Ð:")
                print(f"{'x':<10} {'|Ð Ð4 - Ð­Ð¹Ð»ÐµÑ|':<15} {'|Ð Ð4 - Ð­-Ð|':<15}")
                for i in range(n + 1):
                    x_val = x0 + i * (xn - x0) / n
                    y_e = results[0][2][i]
                    y_ec = results[1][2][i]
                    y_rk = results[2][2][i]
                    print(f"{x_val:<10.6f} {abs(y_rk - y_e):<15.10f} {abs(y_rk - y_ec):<15.10f}")
            else:
                names = {1: "Ð­Ð¹Ð»ÐµÑ", 2: "Ð­Ð¹Ð»ÐµÑ-ÐÐ¾ÑÐ¸", 3: "Ð ÑÐ½Ð³Ðµ-ÐÑÑÑÐ° 4"}
                x_vals, y_vals = solver.solve(choice, f, x0, y0, xn, n)
                print_results(x_vals, y_vals, names[choice])
                print(f"\ny({xn}) = {y_vals[-1]:.10f}")

            cont = input("\nÐÑÐµ? (Ð´Ð°/Ð½ÐµÑ): ").lower()
            if cont not in ['Ð´Ð°', 'Ð´', 'yes', 'y']:
                break

        except:
            print("ÐÑÐ¸Ð±ÐºÐ°")


if __name__ == "__main__":
    main()
