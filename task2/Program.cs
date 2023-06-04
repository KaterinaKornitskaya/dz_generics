namespace task2
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
   
    class RusList 
    {
        public List<string> MyList; 
    
        public List<string> AddRusVar()
        {
            MyList = new List<string>();
            bool c = true;
            do
            {
                Console.Write("Добавить новый перевод - нажмите 1, нет - нажмте 0");
                int user_choice = 0;
                try
                {
                    user_choice = Int32.Parse(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("FormatException");
                }
                
                switch(user_choice)
                {
                    case 1:
                        Console.Write("Введите вариант перевода на русском:");
                        string str = Console.ReadLine();
                        MyList.Add(str);
                        break;
                    case 0:
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
    class EnRusDictionary : RusList
    {
        public static Dictionary<string, List<string>> MyDict 
            = new Dictionary<string, List<string>>();
    
        public void AddDict()
        {
            Console.Write("Введите слово на английском:");
            string str = Console.ReadLine();

            if (MyDict.ContainsKey(str))
                Console.WriteLine("Такое слово уже есть в словаре");
            else
                MyDict.Add(str, AddRusVar());
        }

        public void AddTranslate()
        {
            Console.Write("Введите слово на английском, для которого вы хотите добавить перевод:");
            string str_en = Console.ReadLine();
            if (!MyDict.ContainsKey(str_en))
            {
                Console.WriteLine($"слово не найдено");
                return;
            }

            Console.Write("Какой русскоий перевод добавить?");           
            string str_rus = Console.ReadLine();
            if (MyList.Contains(str_rus))
                Console.WriteLine("Такой вариант перевода уже есть.");
            else if (MyDict.ContainsKey(str_en))
            {
                MyDict[str_en].Add(str_rus);
                //Dict[str] = AddRusVar();
            }
        }

        public void DeleteDict()
        {
            Console.Write("Введите слово на английском, которое выходить удалить:");
            string str = Console.ReadLine();
            if (!MyDict.ContainsKey(str))
            {
                Console.WriteLine($"слово не найдено");
                return;
            }
            else
            {
                MyDict.Remove(str);
                Console.WriteLine($"Слово {str} и его переводы удалено.");
            }            
        }

        public void DeleteTranslate()
        {
            Console.Write("Введите слово на английском, для которого вы хотите удалить перевод:");
            string str_en = Console.ReadLine();
            if (!MyDict.ContainsKey(str_en))
            {
                Console.WriteLine($"слово не найдено");
                return;
            }

            Console.Write("Какой русскоий перевод вы хотите удалить?");
            string str_rus = Console.ReadLine();
            if (!MyList.Contains(str_rus))
                Console.WriteLine("Такой вариант перевода не найден.");
            else if(MyList.Contains(str_rus))
            {
                if (MyList.Count < 2)
                {
                    Console.WriteLine("В списке переводо одно слово. Если удалить его - будет удалено и слово." +
                        "Удалить последний перевод и слово? Если да - нажмите 1, если нет - нажмите 0.");
                    int c = Int32.Parse(Console.ReadLine());
                    if (c == 1)
                    {
                       // MyList.Remove(str_rus);
                        MyDict.Remove(str_en);
                        Console.WriteLine($"Слово {str_en} и перевод удалено.");
                    }
                }
                else
                    MyList.Remove(str_rus);
                Console.WriteLine($"Перевод {str_rus} удален.");
            }
        }

        public void ChangeDict()
        {
            Console.Write("Введите слово на английском, котрое вы хотите изменить:");
            string str_en = Console.ReadLine();
            if (!MyDict.ContainsKey(str_en))
                Console.WriteLine("Такого слове нет в словаре.");
            else
            {
                Console.WriteLine("Введите новое слово:");
                string str_en2 = Console.ReadLine();
                List<string> content = new List<string>();
                content = MyDict[str_en];
                MyDict.Remove(str_en);
                MyDict.Add(str_en2, content);
                //MyDict[str_en] = MyDict[str_en2];
                Console.WriteLine($"Слово {str_en} изменилось на {str_en2}.");
            }
        }

        public void ChangeTranslate()
        {
            Console.Write("Введите слово на английском, для которого вы хотите изменить вариант перевода:");
            string str_en = Console.ReadLine();
            if (!MyDict.ContainsKey(str_en))
            {
                Console.WriteLine("Такого слове нет в словаре.");
                return;
            }
            Console.Write("Какой русскоий перевод вы хотите изменить?");
            string str_rus_old = Console.ReadLine();
            if (!MyList.Contains(str_rus_old))
                Console.WriteLine("Такой вариант перевода не найден.");
            else if(MyList.Contains(str_rus_old))
            {
                Console.WriteLine("Введите новый перевод:");
                string str_rus_new = Console.ReadLine();
                int index = MyList.IndexOf(str_rus_old);
                MyList.Remove(str_rus_old);
                MyList.Insert(index, str_rus_new);
            }
        }

        public void SearchTranslate()
        {
            bool c = false;
            Console.WriteLine("Введите перевод, который вы хотите найти");
            string str_ru = Console.ReadLine();
           
            foreach (KeyValuePair<string, List<string>> kvp in MyDict)
            {             
                if (kvp.Value.Contains(str_ru))
                {
                    c = true;
                    Console.Write($"\nАнглийское слово:\n{kvp.Key}");
                }                
            }
            if (c == false)
            {
                Console.WriteLine("Нет такого перевода");
            }
        }
    
        public void ShowDict()
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