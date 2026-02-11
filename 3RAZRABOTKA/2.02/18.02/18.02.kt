// Задание 1 — Book
class Book(
    val title: String,
    val author: String,
    var year: Int,
    var price: Int
)

// Задание 2 — Student
class Student(
    val name: String,
    val surname: String,
    var group: String
) {
    val fullName: String

    init {
        fullName = "$name $surname"
        println("Создан студент: $fullName, группа: $group")
    }
}

// Задание 3 — BankAccount
class BankAccount(initialBalance: Int) {

    var balance: Int = initialBalance
        set(value) {
            if (value < 0) {
                println("Ошибка: баланс не может быть отрицательным!")
            } else {
                field = value
            }
        }
        get() {
            return field
        }

    fun printBalance() {
        println("Баланс: $balance ₽")
    }
}

// Задание 4 — OrderStatus
enum class OrderStatus(val description: String) {

    NEW("Новый заказ"),
    PROCESSING("Заказ в обработке"),
    SHIPPED("Отправлен"),
    DELIVERED("Доставлен"),
    CANCELLED("Отменён");

    fun isFinished(): Boolean {
        return this == DELIVERED || this == CANCELLED
    }
}

// Задание 5 — Product + Catalog

data class Product(
    val id: Int,
    val name: String,
    var price: Int,
    var inStock: Boolean
)

object ProductCatalog {

    private val _products = mutableListOf<Product>()

    val products: List<Product>
        get() = _products.toList()

    fun addProduct(product: Product) {
        _products.add(product)
    }

    fun findById(id: Int): Product? {
        return _products.find { it.id == id }
    }
}

// MAIN

fun main() {

    println("===== ЗАДАНИЕ 1 =====")

    val book1 = Book("Война и мир", "Лев Толстой", 1869, 1200)
    val book2 = Book("Преступление и наказание", "Фёдор Достоевский", 1866, 900)
    val book3 = Book("Мастер и Маргарита", "Михаил Булгаков", 1967, 1000)

    book2.price = 850

    val books = listOf(book1, book2, book3)

    for (book in books) {
        println("Книга: ${book.title}, Автор: ${book.author}, Год: ${book.year}, Цена: ${book.price}₽")
    }

    println("\n===== ЗАДАНИЕ 2 =====")

    val student1 = Student("Иван", "Петров", "ИС-21")
    val student2 = Student("Анна", "Смирнова", "БИ-32")

    println(student1.fullName)
    println(student2.fullName)

    println("\n===== ЗАДАНИЕ 3 =====")

    val account = BankAccount(1000)
    account.printBalance()

    account.balance = -500
    account.balance = 2000
    account.printBalance()

    println("\n===== ЗАДАНИЕ 4 =====")

    for (status in OrderStatus.values()) {
        println("${status.name} - ${status.description}")
    }

    println("\nЗавершённые статусы:")
    for (status in OrderStatus.values()) {
        if (status.isFinished()) {
            println(status.name)
        }
    }

    println("\n===== ЗАДАНИЕ 5 =====")

    val product1 = Product(1, "Ноутбук", 80000, true)
    val product2 = Product(1, "Ноутбук", 80000, true)

    println("product1 == product2 -> ${product1 == product2}")

    val productCopy = product1.copy(price = 75000)

    println("Оригинал: $product1")
    println("Копия: $productCopy")

    ProductCatalog.addProduct(product1)
    ProductCatalog.addProduct(productCopy)
    ProductCatalog.addProduct(Product(2, "Телефон", 50000, true))

    println("\nКаталог товаров:")
    for (p in ProductCatalog.products) {
        println(p)
    }

    val found = ProductCatalog.findById(2)
    println("\nНайден товар: $found")
}
