using System;
using System.Collections.Generic;
using System.Collections;

namespace Homework_06
{
    /// <summary>
    /// Класс для хранения натруальных чисел и групп
    /// </summary>
    class NaturNum : IEnumerator<int>
    {
        /// <summary>
        /// Массив выполняет две функции (для экономии памяти), содержит смещение на следующее натуральное число
        /// или номер группы к которой относится число
        /// Если значение отрицательное, тогда индекс этого массива является натуральным числом а в значении
        /// массива находится смещение относительно следующегоо натурального числа умноженое на -1
        /// если значение положительное >1, тогда это номер группы к которой относится число
        /// если значение 0 это число не отнесенное ни одной группе (еще в процессе)
        /// если значение 1 это последнее натуральное число 
        /// Сами числа это индекс начиная от 1
        /// </summary>
        private short[] Data;

        /// <summary>
        /// Курсор для итератора натуральных чисел
        /// </summary>
        private int cursor;

        /// <summary>
        /// При формировании получим натуральные числа и выставим индексы
        /// для быстрого перемещения курсора
        /// </summary>
        /// <param name="length"></param>
        public NaturNum(int length)
        {
            Data = new short[length + 1];

            for (int i = 2; i < Data.Length; i++)
                Data[i] = 1;

            for (int i = 2; i * i < Data.Length; i++)
            {
                if (Data[i] == 1)
                    for (int d = i * i; d < Data.Length; d += i)
                        Data[d] = 0;
            }

            int stNode = 1;
            for (int i = 2; i < Data.Length; i++)
            {
                if (Data[i] == 1)
                {
                    Data[stNode] = (short)(stNode - i);
                    stNode = i;
                }
            }
        }

        /// <summary>
        /// Возвращает номер группы к которой относится число
        /// </summary>
        /// <param name="num">число группу которого надо получить</param>
        /// <returns>номер группы к которой относится число</returns>
        public int group(int num)
        {
            return Data[num];
        }

        /// <summary>
        /// Устанавливает группу для число, если это не натуральное число
        /// и оно еще ни разу не устанавливалось
        /// </summary>
        /// <param name="num">число которому нужно присвоить группу</param>
        /// <param name="group">группа которая присваивается число</param>
        public void group(int num, int group)
        {
            if (Data[num] == 0)
                Data[num] = (short) group;
        }

        #region Реализция интерфейса итератора

        public Object Current => cursor;

        int IEnumerator<int>.Current => cursor;

        public void Reset() => cursor = 1;

        public IEnumerator GetEnumerator()
        {
            return this;
        }
        public bool MoveNext()
        {
            bool next = Data[cursor] < 0;
            cursor += -Data[cursor];
            return next;
        }
        public void Dispose()
        {
        }
        #endregion
    }
}