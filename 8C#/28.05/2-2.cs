using System;

// Событийные аргументы с именем свойства
class PropertyEventArgs : EventArgs
{
    public string PropertyName { get; set; }
    public PropertyEventArgs(string name) { PropertyName = name; }
}

// Делегат
public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

// Интерфейс
interface IPropertyChanged
{
    event PropertyEventHandler PropertyChanged;
}

// Класс с уведомлением об изменении свойств
class Person : IPropertyChanged
{
    private string name;
    private int age;
    
    public event PropertyEventHandler PropertyChanged;
    
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            OnPropertyChanged("Name");
        }
    }
    
    public int Age
    {
        get { return age; }
        set
        {
            age = value;
            OnPropertyChanged("Age");
        }
    }
    
    protected void OnPropertyChanged(string propName)
    {
        PropertyChanged?.Invoke(this, new PropertyEventArgs(propName));
    }
}

// Главный класс
class Program
{
    static void Main()
    {
        Person p = new Person();
        
        // Подписываемся на событие
        p.PropertyChanged += (sender, e) =>
        {
            Console.WriteLine($"Свойство '{e.PropertyName}' было изменено!");
        };
        
        // Меняем свойства
        Console.WriteLine("Изменяем свойства:");
        p.Name = "Джек";
        p.Age = 25;
        p.Name = "Джек Строитель";
        p.Age = 30;
        
        Console.ReadKey();
    }
}
