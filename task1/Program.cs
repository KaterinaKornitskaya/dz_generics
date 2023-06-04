using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace task1
{
    // Задание 1
    // Создайте приложение для менеджмента сотрудников и паролей.
    // Необходимо хранить след.информацию:
    // - Логины сотрудников
    // - Пароли сотрудников
    // Для хранения информации используйте Dictionary<T>
    // Приложение должно предоставлять такую функциональность:
    // - добавление логина и пароля сотрудника
    // - удаление логина сотрудника
    // - изменение информации о логине и пароле сотрудника
    // - получение информации о пароле по логину сотрудника



    internal class Program
    {
        static void Main(string[] args)
        {

            Logins obj = new Logins();

            int choice;
            do
            {
                Console.WriteLine("1. Добавить новый логин и пароль.");
                Console.WriteLine("2. Удалить логин (и пароль к нему).");
                Console.WriteLine("3. Изменить пароль по логину.");
                Console.WriteLine("4. Получить информацию о пароле по логину.");
                Console.WriteLine("5. Показать все логины и пароли.");
                Console.WriteLine("6. Выйти");

                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        obj.AddOnePerson();
                        break;
                    case 2:
                        obj.DeleteOnePerson();
                        break;
                    case 3:
                        obj.ChangeInfo();
                        break;
                    case 4:
                        obj.WatchPassword();
                        break;
                    case 5:
                        obj.ShowInfo();
                        break;
                }
            } while (choice!=6);
        }
    }
    class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    class Logins : User
    {
        public static Dictionary<string, string> MyDict = new Dictionary<string, string>();
        
        public void AddOnePerson()
        {
            try
            {
                Console.WriteLine("Введите логин: ");
                Login = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                Password = Console.ReadLine();
                if (Password.Length < 4)
                {
                    throw new Exception("Пароль должен содержать минимум 4 символа");
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
           
            try
            {
                MyDict.Add(Login, Password);
                Console.WriteLine($"Логин {Login} и пароль к нему добавлены!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Елемент с таким логином {Login} уже существует!");
            }           
        }

        public void DeleteOnePerson()
        {
            Console.WriteLine("Введите логин для удаления: ");
            Login = Console.ReadLine();
            if(MyDict.ContainsKey(Login))
            {
                MyDict.Remove(Login);
                Console.WriteLine($"Логин {Login} и пароль к нему удалены!");
            }
            else if (!MyDict.ContainsKey(Login))
            {
                Console.WriteLine($"Логин {Login} не существует!");
            }          
        }
        
        public void WatchPassword()
        {
            Console.WriteLine("Enter login to watch password: ");
            Login = Console.ReadLine();
            Console.WriteLine($"Для логина {Login} пароль: {MyDict[Login]}");
        }

        public void ChangeInfo()
        {
            Console.WriteLine("Ведите логин, по которому вы хотите изменить пароль: ");
            Login = Console.ReadLine();
            Console.WriteLine("Введите новый пароль: ");
            Password = Console.ReadLine();
            //MyDict[Login] = Password;
            if (MyDict.ContainsKey(Login))
            {
                MyDict[Login] = Password;
                Console.WriteLine($"Логин {Login} и пароль к нему изменены!");
            }
            else if (!MyDict.ContainsKey(Login))
            {
                Console.WriteLine($"Логин {Login} не существует!");
            }          
        }

        public void ShowInfo()
        {
            foreach (KeyValuePair<string, string> pair in MyDict)
            {
                Console.WriteLine(pair.Key + " - " + pair.Value);
            }
        }
    }
}