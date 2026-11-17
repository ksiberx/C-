using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Mutex mutex = null;

        try
        {
            try
            {
                // Если такой мьютекс уже существует, значит приложение запущено.
                mutex = Mutex.OpenExisting("MY_MUTEX");
                Console.WriteLine("Приложение уже запущено!");
                return;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                // Мьютекс ещё не существует — создаём его.
                mutex = new Mutex(true, "MY_MUTEX");
            }

            Console.WriteLine("Приложение работает");
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        finally
        {
            if (mutex != null)
            {
                try
                {
                    mutex.ReleaseMutex();
                }
                catch (ApplicationException)
                {
                    // Мьютекс не принадлежит текущему потоку.
                }

                mutex.Dispose();
            }
        }
    }
}