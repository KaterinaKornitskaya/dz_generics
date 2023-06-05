﻿namespace task2
{
    // Создайте приложение "Англо-французский словарь". Необходимо
    // хранить след.информацию:
    // - слово на английском варианте
    // - варианты перевода на французский
    // Для хранения информации используйте Dictionary<T>
    // Приложение должно предоставлять такую функциональность:
    // - добавление слова и вариантов перевода
    // - удаление слова
    // - удаление вариантов перевода
    // - изменение слова
    // - изменение вариантов перевода
    // - поиск перевода слова
    internal class Program
    {
        static void Main(string[] args)
        {
            EnRusDictionary obj = new EnRusDictionary();
            bool c = true;
            do
            {
                Console.WriteLine("1. Добавить слово и перевод/переводы к нему в словарь.");
                Console.WriteLine("2. Добавить новый перевод к существующему слову.");               
                Console.WriteLine("3. Удалить слово и все его переводы.");
                Console.WriteLine("4. Удалить один из вариантов перевода слова.");
                Console.WriteLine("5. Изменить слово в словаре.");
                Console.WriteLine("6. Изменить существующий перевод существующего слова");
                Console.WriteLine("7. Поиск перевода слова");
                Console.WriteLine("8. Показать словарь.");
                Console.WriteLine("9. Выход.");
                int user_choice = 0;
                try
                {
                    user_choice = Int32.Parse(Console.ReadLine());
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                switch (user_choice)
                {
                    case 1:
                        obj.AddDict();
                        break;
                    case 2:
                        obj.AddTranslate();
                        break;
                    case 3:
                        obj.DeleteDict();
                        break;
                    case 4:
                        obj.DeleteTranslate();
                        break;
                    case 5:
                        obj.ChangeDict();
                        break;
                    case 6:
                        obj.ChangeTranslate();
                        break;
                    case 7:
                        obj.SearchTranslate(); 
                        break;
                    case 8:
                        obj.ShowDict();
                        break;
                    case 9:
                        c = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (c);
        }
    }
   
    class RuList  // класс Список переводов
    {
        public List<string> MyList;
    
        public List<string> AddRusVar()   // метод для добавления перевода в список
        {
            MyList = new List<string>();  // выделяем память под список
            bool c = true;                // переменная для цикла
            do
            {
                Console.Write("Добавить новый перевод - нажмите 1, нет - нажмте 0");
                int user_choice = 0;
                try                       // может быть сгенерировано исключение неверного ввода пункта выбора
                {
                    user_choice = Int32.Parse(Console.ReadLine());
                }
                catch(FormatException)    // если введена не цифра - генерируем FormatException
                {
                    Console.WriteLine("FormatException");
                }
                
                switch(user_choice)       // в зависимости от выбора пользователя
                {
                    case 1:               // если юзер нажал 1:
                        Console.Write("Введите вариант перевода на русском:");
                        string str = Console.ReadLine();  // считываем введенную строку
                        MyList.Add(str);                  // добавить строку к списку перевода
                        break;
                    case 0:               // если юзер наджал 0 - выход из цикла
                        c = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }              
            } while (c);
            return MyList;
        }
    }
    class EnRusDictionary : RuList  // класс Словарь, наследует класс Список переводов
    {
        public static Dictionary<string, List<string>> MyDict 
            = new Dictionary<string, List<string>>();
    
        public void AddDict()  // метод для добавления слова в словарь
        {
            Console.Write("Введите слово на английском:");
            string str = Console.ReadLine();   // считываем введенной слово

            if (MyDict.ContainsKey(str))       // если слово уже есть в словаре  -выводим информацию об этом
                Console.WriteLine("Такое слово уже есть в словаре");
            else                               // если такого слова нет
                MyDict.Add(str, AddRusVar());  // добавляем слово как ключ, как значение - вызов метода AddRusVar()
        }

        public void AddTranslate()  // метод для добавления перевода к уже существующему слову
        {
            Console.Write("Введите слово на английском, для которого вы хотите добавить перевод:");
            string str_en = Console.ReadLine();  // считываем введенной слово
            if (!MyDict.ContainsKey(str_en))     // если такого слова нет - выводим информацию и выходим из метода
            {
                Console.WriteLine($"слово не найдено");
                return;
            }
            Console.Write("Какой русскоий перевод добавить?");           
            string str_rus = Console.ReadLine();  // считываем введенный перевод
            if (MyList.Contains(str_rus))         // если такой перевод уже есть - выводим информацию
                Console.WriteLine("Такой вариант перевода уже есть.");
            else if (MyDict.ContainsKey(str_en))  // иначе, если такого перевода нет
            {
                MyDict[str_en].Add(str_rus);      // добавляем перевод по ключу
            }
        }

        public void DeleteDict()  // метод для удаления слова
        {
            Console.Write("Введите слово на английском, которое выходить удалить:");
            string str = Console.ReadLine();  // считываем введенное слово
            if (!MyDict.ContainsKey(str))     // если такого слова нет - выводим информацию и выходим из метода
            {
                Console.WriteLine($"слово не найдено");
                return;
            }
            else                     // иначе, если слово есть
            {
                MyDict.Remove(str);  // удаляем слово из словаря
                Console.WriteLine($"Слово {str} и его переводы удалено.");
            }            
        }

        public void DeleteTranslate()  // метод для удаления перевода
        {
            Console.Write("Введите слово на английском, для которого вы хотите удалить перевод:");
            string str_en = Console.ReadLine();  // считываем введенной слово
            if (!MyDict.ContainsKey(str_en))     // если такого слова нет - выводим информацию и выходим из метода
            {
                Console.WriteLine($"слово не найдено");
                return;
            }

            Console.Write("Какой русскоий перевод вы хотите удалить?");
            string str_rus = Console.ReadLine();  // считываем введенный перевод
            if (!MyList.Contains(str_rus))        // если такого перевода нет  - выводим информацию
                Console.WriteLine("Такой вариант перевода не найден.");
            else if(MyList.Contains(str_rus))     // если такой перевод есть
            {
                if (MyList.Count < 2)             // если в списке только один перевод
                {                                 // то запрашиваем, что в случае удаления последнего перевода удаляется слово
                    Console.WriteLine("В списке переводо одно слово. Если удалить его - будет удалено и слово." +
                        "Удалить последний перевод и слово? Если да - нажмите 1, если нет - нажмите 0.");
                    int c = Int32.Parse(Console.ReadLine());
                    if (c == 1)                   // если юзер хочет удалтьь последний перевол и все слово
                    {
                        MyDict.Remove(str_en);    // удаляем слово из словаря
                        Console.WriteLine($"Слово {str_en} и перевод удалено.");
                    }
                }
                else                              // иначе, если удаляемый перевод не последний
                    MyList.Remove(str_rus);       // удаляем перевод из листа
                Console.WriteLine($"Перевод {str_rus} удален.");
            }
        }

        public void ChangeDict()  // метод для изменения слова
        {
            Console.Write("Введите слово на английском, котрое вы хотите изменить:");
            string str_en = Console.ReadLine();  // считываем введенное слово
            if (!MyDict.ContainsKey(str_en))     // если такого слова нет - выводи инфо
                Console.WriteLine("Такого слове нет в словаре.");
            else                                 // если введенное слово есть в словаре
            {
                Console.WriteLine("Введите новое слово:");
                string str_en2 = Console.ReadLine();        // считываем введенное слово для изменения
                List<string> content = new List<string>();  // создаем новый список
                content = MyDict[str_en];                   // в новый список записываем переводды из старого списка
                MyDict.Remove(str_en);                      // удаляем старое слово
                MyDict.Add(str_en2, content);               // добавляем новое слово со старыми переводами
                Console.WriteLine($"Слово {str_en} изменилось на {str_en2}.");
            }
        }

        public void ChangeTranslate()  // метод для изменения перевода
        {
            Console.Write("Введите слово на английском, для которого вы хотите изменить вариант перевода:");
            string str_en = Console.ReadLine();  // считываем введенное слово
            if (!MyDict.ContainsKey(str_en))     // если нет в словаре - выводи инфо и выходим из метода
            {
                Console.WriteLine("Такого слове нет в словаре.");
                return;
            }
            Console.Write("Какой русскоий перевод вы хотите изменить?");
            string str_rus_old = Console.ReadLine();  // считываем введенный старый перевод
            if (!MyList.Contains(str_rus_old))        // если такого перевода нет - выводим инфо
                Console.WriteLine("Такой вариант перевода не найден.");
            else if(MyList.Contains(str_rus_old))     // если перевод есть
            {
                Console.WriteLine("Введите новый перевод:");
                string str_rus_new = Console.ReadLine();  // считываем введенный новый перевод
                int index = MyList.IndexOf(str_rus_old);  // узнаем индекс этого перевода в списке
                MyList.Remove(str_rus_old);               // удаляем старый перевод из списка
                MyList.Insert(index, str_rus_new);        // втсавляем новый перевод в список на место удаленного
            }
        }

        public void SearchTranslate()  // метод для поиска перевода (программа выведет слово)
        {
            bool c = false;            // переменная для цикла
            Console.WriteLine("Введите перевод, который вы хотите найти");
            string str_ru = Console.ReadLine();  // считываем введенный перевод
           
            foreach (KeyValuePair<string, List<string>> kvp in MyDict)  // цикл по словарю
            {             
                if (kvp.Value.Contains(str_ru))  // есть в словаре в значениях  есть такой перевод
                {
                    c = true;
                    Console.Write($"\nАнглийское слово:\n{kvp.Key}");  // то выводим ключ (слово) для этого значения
                }                
            }
            if (c == false)  // иначе перевод не найден, выводим инфо
            {
                Console.WriteLine("Нет такого перевода");
            }
        }
    
        public void ShowDict()  // метод для вывода словаря в консоль
        {
            foreach(KeyValuePair<string, List<string>> kvp in MyDict)
            {
                Console.Write($"\nАнглийское слово:\n{kvp.Key}");
                Console.Write("\nВарианты перевода:\n");
                foreach (string item in kvp.Value)
                {
                    Console.Write($"{item + "\n"}");
                }               
            }
            Console.WriteLine();
        }   
    }
}