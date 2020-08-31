using System;

namespace Homework_06
{
    /// <summary>
    /// Класс определяет как выводить и как считывать информацию от пользователя
    /// </summary>
    static class UI
    {
        /// <summary>
        /// Линия для выделения блоков
        /// </summary>
        static private string SP = "-------------------------------------";

        /// <summary>
        /// Выводит массив строк
        /// </summary>
        /// <param name="texts">Масиив строк которые надо вывести</param>
        static public void SetString(string[] texts)
        {
            foreach(string text in texts)
            {
                SetString(text);
            }
        }

        /// <summary>
        /// выводит строку
        /// </summary>
        /// <param name="text">Строка для вывода</param>
        static public void SetString(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Очищает видимую область
        /// </summary>
        static public void newList()
        {
            Console.Clear();
        }

        /// <summary>
        /// Ожидает реакции пользователя
        /// </summary>
        static public void WaitUser()
        {
            SetString(new string[] { "", SP, "Для продолжения нажмите любую клавишу ..." });
            Console.ReadKey();
        }

        /// <summary>
        /// Получает строку от пользователя
        /// </summary>
        /// <returns>Строка от пользователя</returns>
        static public string GetString()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Получает от пользователя диапозон чисел
        /// </summary>
        /// <param name="min">Минимальное число</param>
        /// <param name="max">Максимальное число</param>
        /// <returns>Число которое вве пользователь</returns>
        static public int GetRange(int min, int max)
        {
            int num = min - 1;
            while (num < min || num > max)
            {
                Console.WriteLine($"num({min}-{max}):");
                int.TryParse(Console.ReadLine(), out num);
            }
            return num;
        }

        /// <summary>
        /// Получает символ который ввел пользователь
        /// </summary>
        /// <returns>Символ от пользователя</returns>
        static public char GetChar()
        {
            return Console.ReadKey().KeyChar;
        }

        /// <summary>
        /// Спрашивает пользователя да или нет
        /// </summary>
        /// <returns>true если нажал "да"</returns>
        static public bool UserOk(string text)
        {
            SetString(text + " (y/n)");
            char answer = GetChar();
            return answer != 'y' && answer != 'n' ? UserOk(text) : answer == 'y';
        }
    }
}