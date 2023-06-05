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
                        obj.AddPerson();
                        break;
                    case 2:
                        obj.DeletePerson();
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
    class User  // класс Пользователь
    {
        public string Login { get; set; }     // свойство логин
        public string Password { get; set; }  // свойство пароль
    }
    class Logins : User  // класс Логин, наследует класс Пользователь
    {
        // создаем словарь с ключам типа string, значениями типа string
        public static Dictionary<string, string> MyDict = new Dictionary<string, string>();
        
        public void AddPerson()  // метод для добавления логина и пароля
        {
            try  // здесь может быть сгенерировано исключение
            {
                Console.WriteLine("Введите логин: ");
                Login = Console.ReadLine();     // считываем введенный юзером логин
                Console.WriteLine("Введите пароль: ");
                Password = Console.ReadLine();  // считываем введенный юзером пароль
                if (Password.Length < 4)        // если пароль содержит менее 4 символов
                {                               // то выбрасываем исключение
                    throw new Exception("Пароль должен содержать минимум 4 символа");
                }
            }
            catch(Exception ex)                 // отлов ичключения
            { 
                Console.WriteLine(ex.Message);  // вывод информации об исключении
            }

            try  // здесь может быть сгенерировано исключение
            {
                MyDict.Add(Login, Password);  // добавление логина и пароля в словарь
                Console.WriteLine($"Логин {Login} и пароль к нему добавлены!");
            }
            catch (ArgumentException)         // отлов исключения, если такой ключ уже есть в словаре
            {
                Console.WriteLine($"Елемент с таким логином {Login} уже существует!");
            }           
        }

        public void DeletePerson()  // метод для удаления логина и пароль
        {
            Console.WriteLine("Введите логин для удаления: ");
            Login = Console.ReadLine();     // считываем введенный юзером логин
            if (MyDict.ContainsKey(Login))  // если словарь содержит такой ключ(логин)
            {
                MyDict.Remove(Login);       // то удаляем логин из словаря
                Console.WriteLine($"Логин {Login} и пароль к нему удалены!");
            }
            else if (!MyDict.ContainsKey(Login))  // иначе, если словарь не содержит такой логин
            {
                Console.WriteLine($"Логин {Login} не существует!");
            }          
        }
        
        public void WatchPassword()  // метод для просмотра пароля по логину
        {
            Console.WriteLine("Enter login to watch password: ");
            Login = Console.ReadLine();       // считываем введенный юзером логин
            Console.WriteLine($"Для логина {Login} " +
                $"пароль: {MyDict[Login]}");  // выводи в консоль пароль по ключу
        }

        public void ChangeInfo()  // метод для изменения пароля
        {
            Console.WriteLine("Ведите логин, по которому вы хотите изменить пароль: ");
            Login = Console.ReadLine();     // считываем введенный юзером логин
            Console.WriteLine("Введите новый пароль: ");
            Password = Console.ReadLine();  // считываем введенный новый пароль
            if (MyDict.ContainsKey(Login))  // если словарь содержит такой логин
            {
                MyDict[Login] = Password;   // то меняем пароль по ключу
                Console.WriteLine($"Логин {Login} и пароль к нему изменены!");
            }
            else if (!MyDict.ContainsKey(Login))  // иначе, если такой логин не существует
            {
                Console.WriteLine($"Логин {Login} не существует!");
            }          
        }

        public void ShowInfo()  // метод для вывода информации в консоль
        {
            foreach (KeyValuePair<string, string> pair in MyDict)  // цикл по парам в словаре
            {
                Console.WriteLine(pair.Key + " - " + pair.Value);  // выводим ключ и его значение
            }
        }
    }
}