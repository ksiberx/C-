using System;
using System.Threading;

class MutexCounter
{
    private int count = 0;
    private readonly Mutex mutex = new Mutex();

    public void AddOneMillion()
    {
        for (int i = 0; i < 1000000; i++)
        {
            mutex.WaitOne();
            try
            {
                count++;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }

    public int GetCount()
    {
        return count;
    }
}

class Program
{
    static void Main()
    {
        MutexCounter counter = new MutexCounter();
        Thread[] threads = new Thread[5];

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(counter.AddOneMillion);
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
            threads[i].Join();

        Console.WriteLine("Итоговое значение: " + counter.GetCount());
        Console.ReadKey();
    }
}