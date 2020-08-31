using System;

namespace Homework_06
{
    /// <summary>
    /// Меню для пользователя
    /// </summary>
    class Menu
    {
        private string fileName = "NumbersGroup.txt";
        private NumbersGroup NG = new NumbersGroup(100);

        /// <summary>
        /// конструктор при создании создает репозиторий.
        /// </summary>
        /// <param name="repository">Репозиторий с которым будет взаимодействовать меню
        /// </param>
        public Menu()
        {
            Loop();
        }

        /// <summary>
        /// Отображает главное меню
        /// </summary>
        void ShowMenu()
        {
            UI.SetString(new string[] {"",
            "-------------------------------",
            "| 1 - Ввести число            |",
            "| 2 - Редактировать имя файла |",
            "| 3 - Сохранить группы в файл |",
            "---------------------------------",
            "          0 - ВЫХОД",
            "---------------------------------",
            $"Текущее имя файла:{fileName}",
            $"Текущее число: {NG.GetNumber()}",
            $"Количество групп:{NG.GetNumGroups()}",
            ""});
        }

        /// <summary>
        /// Основной цикл программы где от пользователя просят действий
        /// </summary>
        void Loop()
        {
            char key = ' ';
            while (key != '0')
            {
                Console.Clear();
                ShowMenu();
                key = UI.GetChar();
                switch (key)
                {
                    case '1':
                        EditNumber();
                        break;
                    case '2':
                        PathEdit();
                        break;
                    case '3':
                        SaveFile();
                        break;
                }
            }
        }

        /// <summary>
        /// Редактирование числа
        /// </summary>
        void EditNumber()
        {
            BeginAction("Ввод числа", "Введите номер число которое нужно разложить на группы не более миллиарда");
            NG.SetNumber(UI.GetRange(3, 1000000000));
        }

        /// <summary>
        /// Редактирование пути к файлу
        /// </summary>
        void PathEdit()
        {
            BeginAction($"Редактирование пути к файлу\nТекущийПуть: {fileName}", "Введите полный путь к файлу");
            string newPatch = UI.GetString();
            if (!string.IsNullOrEmpty(newPatch))
            {
                fileName = newPatch;
                UI.WaitUser();
            }
        }

        /// <summary>
        /// Сохраняет расчитанные группы в файл
        /// </summary>
        void SaveFile()
        {
            BeginAction($"Сохранение групп на диск", NG.GetNumber() > 10000000 ? "Откидывайтесь на спинку и ждите, сейчас происходит засорение диска мусором": "");

            DateTime begin = DateTime.Now; 
            NG.WriteStrem(fileName);
            TimeSpan ts = DateTime.Now - begin;

            UI.SetString(new string[] { 
                   "Процесс занял:",
                   $"Секунд - {ts.TotalSeconds}",
                   $"Миллисекунд - {ts.TotalMilliseconds}"});

            if (UI.UserOk("Заархивировать этот файл?"))
            {
                UI.SetString("Архивируем файл ...");
                NG.ZipFile(fileName);
            }
                
            
            UI.WaitUser();
        }

        /// <summary>
        /// Выводит оповещение о выбранном действии пользователя
        /// </summary>
        /// <param name="head"></param>
        /// <param name="query"></param>
        private void BeginAction(string head, string query)
        {
            UI.newList();
            UI.SetString(head);
            if (!string.IsNullOrEmpty(query))
            {
                UI.SetString(query);
            }
        }
    }
}