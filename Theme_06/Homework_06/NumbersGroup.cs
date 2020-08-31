using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Homework_06
{
    /// <summary>
    /// Расчитывает количество групп и выводит значения групп в стрим
    /// </summary>
    class NumbersGroup
    {
        /// <summary>
        /// Число для которого расчитываем группы
        /// </summary>
        private int num;

        public NumbersGroup(int num)
        {
            this.num = num;
        }

        /// <summary>
        /// Возвращает число групп, расчитанное от числа
        /// </summary>
        /// <returns>Число групп</returns>
        public int GetNumGroups()
        {
            return (int) Math.Log(num, 2) + 1;
        }

        /// <summary>
        /// Возвращает текущее число
        /// </summary>
        /// <returns>Текущее число</returns>
        public int GetNumber()
        {
            return num;
        }

        /// <summary>
        /// Устанавливает число
        /// </summary>
        /// <param name="num">Число которое нужно установить</param>
        public void SetNumber(int num)
        {
            this.num = num;
        }

        /// <summary>
        /// Добавляет ряд к стриму
        /// </summary>
        /// <param name="sw">поток записи куда добавляется группа</param>
        /// <param name="lastGroup">последнии цифры предыдущей группы</param>
        /// <param name="natur">натйральные числа</param>
        /// <param name="numGroup">номер группы которую добавляем</param>
        /// <returns>Массив чисел которые были добавлены в группу</returns>
        private int[] WriteGroupToStream(StreamWriter sw , int[] lastGroup, NaturNum natur, int numGroup)
        {
            List<int> tempGroup = new List<int>();
            
            for (int i = 0; i < lastGroup.Length; i++)
            {
                if (lastGroup[i] * 2 > num) break;
                natur.Reset();

                foreach (int nat in natur)
                {
                    int currNum = nat * lastGroup[i];
                    if (currNum > num) break;

                    if (natur.group(currNum) <= 1)
                    {
                        sw.Write(" " + currNum);
                        tempGroup.Add(currNum);
                        
                        natur.group(currNum, numGroup);
                    }
                }
            }
            tempGroup.Sort();
            return tempGroup.ToArray();
        }

        /// <summary>
        /// Записывает ряды в файл
        /// </summary>
        /// <param name="fileName">Имя файла куда записать</param>
        public void WriteStrem(string fileName)
        {
            int group = GetNumGroups();
            NaturNum natur = new NaturNum(num);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                int[] lastGroup = new int[1];
                lastGroup[0] = 1;
                sw.Write(" 1");

                for (int numGroup = 1; numGroup <= group; numGroup++)
                {
                    sw.WriteLine();
                    lastGroup = WriteGroupToStream(sw, lastGroup, natur, numGroup);
                }
            }
        }

        /// <summary>
        /// Архивирует файл, создавая такой же с добавлением расширения zip
        /// </summary>
        /// <param name="fileName">Путь к файлу который надо заархивировать</param>
        public void ZipFile(string fileName)
        {
            string compressed = fileName + ".zip";
            using (FileStream ss = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (FileStream ts = File.Create(compressed))
                {
                    using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                    {
                        ss.CopyTo(cs);
                        Console.WriteLine("Сжатие файла {0} завершено. Было: {1}  стало: {2}.",
                                          fileName,
                                          ss.Length,
                                          ts.Length);
                    }
                }
            }
        }
    }    
}