using System;
using System.Collections.Generic;

namespace Red_BlackTree
{
    class Input
    {
        
        public static int GetInt()
        {
            int a = 0;
            while (!int.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("Ошибка ввода! Введите целое число");
            }
            return a;
        }
        public static void Keyboard(RB tree, List<int> temp)
        {
       
            Console.WriteLine("Сколько узлов вы хотите добавить?");
            int count = GetInt();
            int node = 0;
            for (int i = 0; i <= count - 1; i++)
            {
                Console.WriteLine($"Введите значение узла {i + 1} (целое)");
                node = GetInt();
                temp.Add(node);
                
                tree.Insert(node);
            }
        }
        public static void Random(RB tree, List<int> temp)
        {
            
            const int RIGHT = 50;
            const int LEFT = -50;

            Console.WriteLine("Сколько узлов вы хотите добавить?");
            int count = GetInt();
            Random rnd = new Random();
            int node = 0;
            for (int i = 0; i <= count - 1; i++)
            {
                node = rnd.Next(LEFT, RIGHT);
                temp.Add(node);
                tree.Insert(node);
                Console.WriteLine($"Введенное значение узла: {node}");
            }
        }      
    }
}
