using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountOfElementInList
{
    /// <summary>
    /// Обобщенный класс
    /// </summary>
    /// <typeparam name="T">Тип класса</typeparam>
    class MyList<T>
    {        
        T[] list = new T[3];
        private int index;

        /// <summary>
        /// Метод вычисляет количество одинаковых элементов
        /// </summary>
        /// <returns></returns>
        public Dictionary<T, int> CountOfSameObjects()
        {
            Dictionary<T, int> res = new Dictionary<T, int>();
            for (int i = 0; i < index; i++)
            {
                if (res.ContainsKey(list[i]))
                {
                    res[list[i]]++;
                }
                else
                {
                    res.Add(list[i], 1);
                }
            }
            return res;
        }

        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Add(T item)
        {
            if (index >= list.Length)
            {
                Array.Resize(ref list, list.Length * 2);
            }
            list[index++] = item;
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="Index">Удаляемый элемент</param>
        public void RemoveAt(int Index)
        {
            for (int i = Index; i < index; i++)
            {
                list[i] = list[i + 1];
            }
            index--;
        }

    }
}
