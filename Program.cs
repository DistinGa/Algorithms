// Вавилов Дмитрий. C#
using System;
using System.Linq;
using System.Collections.Generic;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BaseMenuItem> Menu = new List<BaseMenuItem>();
            Menu.Add(new BaseMenuItem("Exit", () => { Console.WriteLine("\n\nBye!"); }));
            Menu.Add(new BaseMenuItem("Binary tree search", new Action(Task1)));
            Menu.Add(new BaseMenuItem("Quick sort", new Action(Task2)));
            Menu.Add(new BaseMenuItem("Comparing", new Action(Task3)));

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

            if (res < 0 || res > itemsCount)
            {
                Console.WriteLine("Incorrect input!");
                return GetTask(itemsCount);
            }

            return res;
        }

        /// <summary>
        /// Сортировка по дереву бинарного поиска.
        /// </summary>
        static void Task1()
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
            int[] array = GetRandomArray(n);

            SortingMetrics sm = SearchTreeSort(array);

            Console.WriteLine();
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }

            Console.WriteLine($"\nQuick sort - Compares: {sm.Compares}; Swaps: {sm.Swaps}");

            Console.ReadKey();
        }

        /// <summary>
        /// 2. Реализовать быструю сортировку.
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
            int[] array = GetRandomArray(n);

            SortingMetrics sm = new SortingMetrics() { Swaps = 0, Compares = 0 };
            QuickSort(array, 0, array.Length - 1, ref sm);

            Console.WriteLine();
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine($"\nQuick sort - Compares: {sm.Compares}; Swaps: {sm.Swaps}");

            Console.ReadKey();
        }

        /// <summary>
        /// Сравнение различных методов сортировки.
        /// </summary>
        static void Task3()
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
            int[] array = GetRandomArray(n);
            int[] cloneArray;
            SortingMetrics sm = new SortingMetrics() { Swaps = 0, Compares = 0 };
            DateTime t;

            cloneArray = (int[])array.Clone();
            t = DateTime.Now;
            sm = BubbleSortOpt(cloneArray);
            Console.WriteLine($"\nOptimized bubble sort - Compares: {sm.Compares}; Swaps: {sm.Swaps}");
            Console.WriteLine($"Time: {(DateTime.Now - t).TotalSeconds} seconds");

            sm = new SortingMetrics() { Swaps = 0, Compares = 0 };
            cloneArray = (int[])array.Clone();
            t = DateTime.Now;
            QuickSort(cloneArray, 0, cloneArray.Length - 1, ref sm);
            Console.WriteLine($"\nQuick sort - Compares: {sm.Compares}; Swaps: {sm.Swaps}");
            Console.WriteLine($"Time: {(DateTime.Now - t).TotalMilliseconds} Milliseconds");

            sm = new SortingMetrics() { Swaps = 0, Compares = 0 };
            cloneArray = (int[])array.Clone();
            t = DateTime.Now;
            sm = SearchTreeSort(cloneArray);
            Console.WriteLine($"\nSearch tree sort - Compares: {sm.Compares}; Swaps: {sm.Swaps}");
            Console.WriteLine($"Time: {(DateTime.Now - t).TotalMilliseconds} Milliseconds");

            Console.ReadKey();
        }

        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static int[] GetRandomArray(int length)
        {
            int[] array = new int[length];
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = rnd.Next(length);
            }

            return array;
        }

        /// <summary>
        /// Оптимизированная сортировка методом пузырька.
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns></returns>
        static SortingMetrics BubbleSortOpt(int[] intArray, bool assend = true)
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

        /// <summary>
        /// 2. Реализовать быструю сортировку.
        /// </summary>
        static void QuickSort(int[] array, int startIndx, int finishIndx, ref SortingMetrics sm)
        {
            if (startIndx == finishIndx)
                return;

            int left = startIndx;
            int right = finishIndx;
            int root = array[startIndx];

            while (left < right)
            {
                if (array[left] < root)
                {
                    sm.Compares++;
                    left++;
                    continue;
                }

                if (array[right] >= root)
                {
                    sm.Compares++;
                    right--;
                    continue;
                }

                Swap(ref array[left], ref array[right]);
                sm.Swaps++;
            }

            // Сейчас left = right
            if (left > startIndx)
                left--;
            else
                right++;

            QuickSort(array, startIndx, left, ref sm);
            QuickSort(array, right, finishIndx, ref sm);
        }

        static SortingMetrics SearchTreeSort(int[] array)
        {
            SortingMetrics sm = new SortingMetrics() { Swaps = 0, Compares = 0 };
            TreeNode root = new TreeNode(array[0], null, null);
            for (int i = 1; i < array.Length; i++)
            {
                GrowSearchTree(root, array[i], ref sm);
            }

            int j = 0;
            foreach (var item in root.InOrder())
            {
                array[j++] = item.Data;
            }

            return sm;
        }

        /// <summary>
        /// Размещение нового узла на дереве.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newElement"></param>
        /// <param name="sm"></param>
        static void GrowSearchTree(TreeNode root, int newElement, ref SortingMetrics sm)
        {
            TreeNode current = root;
            while (current != null)
            {
                sm.Compares++;

                if (newElement < current.Data)
                {
                    if (current.LeftNode == null)
                    {
                        current.LeftNode = new TreeNode(newElement, null, null, current);
                        current = null; // Заканчиваем цикл.
                    }
                    else
                    {
                        current = current.LeftNode;
                    }
                }
                else
                {
                    if (current.RightNode == null)
                    {
                        current.RightNode = new TreeNode(newElement, null, null, current);
                        current = null; // Заканчиваем цикл.
                    }
                    else
                    {
                        current = current.RightNode;
                    }
                }
            }
        }
    }
}
