using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TestApp1
{
    /*Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать. 
     * Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом. 
     * Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз. 
     * Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево. 
     * Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. 
     * Садовники должны работать параллельно. Создать многопоточное приложение, моделирующее работу садовников.*/
    class Program
    {
        static int[,] fieldTask;
        static int vDim, gDim;
        

        static void Main(string[] args)
        {
            Console.Write("Введите вертикальный размер поля: ");
            vDim = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите горизонтальный размер поля: ");
            gDim = Convert.ToInt32(Console.ReadLine());
            fieldTask = new int[vDim, gDim];

            ThreadStart delPotoka1 = new ThreadStart(Worker1);
            Thread Potok1 = new Thread(delPotoka1);
            ThreadStart delPotoka2 = new ThreadStart(Worker2);
            Thread Potok2 = new Thread(delPotoka2);

            Potok1.Start();
            Potok2.Start();
            Potok1.Join();
            Potok2.Join();

            Console.WriteLine("\nРезультат работы на поле:");

            for (int i = 0; i < vDim; i++)
            {
                for (int j = 0; j < gDim; j++)
                {
                    Console.Write($"{fieldTask[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
        static void Worker1()
        {

            for (int i = 0; i < vDim; i++)
            {
                for (int j = 0; j < gDim; j++)
                {
                    if (fieldTask[i, j] == 0)
                        fieldTask[i, j] = 1;
                    Thread.Sleep(5);
                }
                Console.WriteLine("\nСадовник 1 закончил грядку {0}", i+1);
            }
        }
        static void Worker2()
        {

            for (int i = gDim - 1; i > 0; i--)
            {
                for (int j = vDim - 1; j > 0; j--)
                {
                    if (fieldTask[j, i] == 0)
                        fieldTask[j, i] = 2;
                    Thread.Sleep(5);
                }
                Console.WriteLine("\nСадовник 2 закончил грядку {0}", i+1);
            }
        }
    }
}
