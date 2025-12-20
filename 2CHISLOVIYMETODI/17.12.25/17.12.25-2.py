def rk4_system(f1, f2, x0, y0, z0, h, n):
    results = [(x0, y0, z0)]
    x, y, z = x0, y0, z0

    for _ in range(n):
        k1y = h * f1(x, y, z)
        k1z = h * f2(x, y, z)

        k2y = h * f1(x + h / 2, y + k1y / 2, z + k1z / 2)
        k2z = h * f2(x + h / 2, y + k1y / 2, z + k1z / 2)

        k3y = h * f1(x + h / 2, y + k2y / 2, z + k2z / 2)
        k3z = h * f2(x + h / 2, y + k2y / 2, z + k2z / 2)

        k4y = h * f1(x + h, y + k3y, z + k3z)
        k4z = h * f2(x + h, y + k3y, z + k3z)

        y = y + (k1y + 2 * k2y + 2 * k3y + k4y) / 6
        z = z + (k1z + 2 * k2z + 2 * k3z + k4z) / 6
        x = x + h

        results.append((x, y, z))

    return results

if __name__ == "__main__":
    def f1(x, y, z):
        return -0.5 * y

    def f2(x, y, z):
        return 4 - 0.3 * z - 0.1 * y

    x0, y0, z0 = 0, 4, 6
    h = 0.1
    n = 10

    solution = rk4_system(f1, f2, x0, y0, z0, h, n)

    for x, y, z in solution:
        print(f"x={x:.2f}, y={y:.6f}, z={z:.6f}")
