// Вавилов Дмитрий. C#
using System;
using System.Collections.Generic;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BaseMenuItem> Menu = new List<BaseMenuItem>();
            Menu.Add(new BaseMenuItem("Exit", () => {Console.WriteLine("\n\nBye!");}));
            Menu.Add(new BaseMenuItem("Simple hash", new Action(Task1)));
            Menu.Add(new BaseMenuItem("Binary search tree", new Action(Task2)));

            int task;

            do
            {
                ShowMenu(Menu);
                task = GetTask(Menu.Count);
                Menu[task].DoMenuAction();
            }
            while (task != 0);

            Console.ReadKey();
        }


        static void ShowMenu(List<BaseMenuItem> menuItems)
        {
            Console.Clear();

            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i}: {menuItems[i].MenuItemText}");
            }
        }

        /// <summary>
        /// Выбор задачи
        /// </summary>
        /// <returns></returns>
        static int GetTask(int itemsCount)
        {
            string selection = Console.ReadKey().KeyChar.ToString();
            int res = 0;

            if (!int.TryParse(selection, out res))
            {
                Console.WriteLine("Incorrect input!");
                return GetTask(itemsCount);
            }

            if(res < 0 || res > itemsCount)
            {
                Console.WriteLine("Incorrect input!");
                return GetTask(itemsCount);
            }

            return res;
        }

        /// <summary>
        /// 1. Реализовать простейшую хеш-функцию. На вход функции подается строка, на выходе сумма кодов символов.
        /// </summary>
        static void Task1()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Input string\n");

            string str = Console.ReadLine();
            Console.WriteLine($"Hash {str} is: {GetHash(str)}");

            Console.ReadKey();
        }

        /// <summary>
        /// 2. Переписать программу, реализующую двоичное дерево поиска.
        /// а) Добавить в него обход дерева различными способами;
        /// б) Реализовать поиск в двоичном дереве поиска;
        /// </summary>
        static void Task2()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Input elements count\n");
            int n;
            if (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Incorrect input\n");
                return;
            }

            // Заполнение массива значений;
            Console.WriteLine($"Input {n} elements\n");
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                Console.Write($"{i}: ");
                int.TryParse(Console.ReadLine(), out array[i]);
            }

            BubbleSortOpt(ref array);

            TreeNode root = BinarySearchTree.CreateBalansedTree(array, array.Length / 2, 0, array.Length - 1);

            Console.WriteLine();
            Console.WriteLine(root.ToString());

            Console.Write("Pre-order: ");
            foreach (var item in root.PreOrder())
            {
                Console.Write(item.Data + " ");
            }

            Console.Write("\nIn-order: ");
            foreach (var item in root.InOrder())
            {
                Console.Write(item.Data + " ");
            }

            Console.Write("\nPost-order: ");
            foreach (var item in root.PostOrder())
            {
                Console.Write(item.Data + " ");
            }

            Console.Write("\n\nEnter value to search: ");
            int.TryParse(Console.ReadLine(), out n);

            Console.WriteLine();
            Console.WriteLine($"Found subtree is: {BinarySearchTree.Find(root, n)}");

            Console.ReadKey();
        }

        static int GetHash(string str)
        {
            int res = 0;
            foreach (char ch in str)
            {
                res += ch;
            }
            return res;
        }

        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        /// <summary>
        /// Оптимизированная сортировка методом пузырька.
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns></returns>
        static SortingMetrics BubbleSortOpt(ref int[] intArray, bool assend = true)
        {
            SortingMetrics res = new SortingMetrics() { Swaps = 0, Compares = 0 };

            int tmp = 0;
            for (int i = 0; i < intArray.Length; i++)
            {
                tmp = 0;
                for (int j = 0; j < intArray.Length - 1 - i; j++)
                {
                    res.Compares++;

                    if ((assend && intArray[j] > intArray[j + 1]) || (!assend && intArray[j] < intArray[j + 1]))
                    {
                        tmp++;
                        Swap(ref intArray[j], ref intArray[j + 1]);
                        res.Swaps++;
                    }
                }

                // Если перестановок не было, массив отсортирован.
                if (tmp == 0)
                    break;
            }

            return res;
        }

    }
}
