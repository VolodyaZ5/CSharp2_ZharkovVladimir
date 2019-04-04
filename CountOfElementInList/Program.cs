using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
    a. для целых чисел;
    b. * для обобщенной коллекции;
    c. ** используя Linq.*/

namespace CountOfElementInList
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> myList = new MyList<int>();
            myList.Add(1);
            myList.Add(1);
            myList.Add(2);
            myList.Add(2);
            myList.Add(2);
            myList.Add(3);
            myList.Add(4);
            myList.Add(4);
            myList.Add(4);
            myList.Add(4);
            myList.Add(4);
            myList.Add(4);
            myList.Add(4);
            myList.Add(4);
            //MyList<double> a = new MyList<double>();
            //a.Add(3.14);
            //a.Add(3.14);
            //a.Add(3.14);
            //a.Add(3.12);
            Console.WriteLine($"Для обобщенной коллекции: ");
            foreach (var item in myList.CountOfSameObjects())
            {
                Console.WriteLine(item);
            }

            

            Console.WriteLine($"\nИспользуя Linq: ");
            List<int> list = new List<int> { 1, 2, 2, 1, 5, 3, 4, 2, 5 };
            var res = list.GroupBy(i => i);            
            foreach (var item in res)
            {
                Console.WriteLine($"[{item.Key}, {item.Count()}]");
            }

            Console.ReadKey();
        }
    }
}
