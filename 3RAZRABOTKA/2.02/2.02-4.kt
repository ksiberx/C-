fun main() {
    val isSunny = true
    val isRainy = false

    println(isSunny && isRainy)
    println(isSunny || isRainy)
    println(!isRainy)

    // Первый результат false, потому что логическое И (&&)
    // возвращает true только если ОБА значения true.
}
