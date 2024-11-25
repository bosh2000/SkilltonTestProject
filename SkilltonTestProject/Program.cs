using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilltonTestProject
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool exit = false;
            do
                switch (MainMenu())
                {
                    case '1':
                        AddEmployee();
                        Console.Clear();
                        break;

                    case '2':
                        ScreenAllEmployees();
                        break;

                    case '3':
                        UpdateEmployee();
                        Console.Clear();
                        break;

                    case '4':
                        DbWork.DeleteEmployee(InputEmployeerId());
                        Console.Clear();
                        break;

                    case '5':
                        Console.WriteLine("Выбран выход из приложения");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Выбрано не существующий пункт меню.");
                        break;
                }
            while (!exit);

            Console.ReadLine();
        }

        private static void UpdateEmployee()
        {
            int employeeId = InputEmployeerId();
            Employee employee = DbWork.GetEmployeeById(employeeId);
            Console.Write("Введите новую дату рождения-");
            employee.DateOfBirth = InputUpdateDate(employee.DateOfBirth);
            Console.Write("Введите Email-");
            ReadLine.Send(employee.Email.Trim());
            employee.Email = ReadLine.Read();
            Console.Write("Введите имя-");
            ReadLine.Send(employee.FirstName.Trim());
            employee.FirstName = ReadLine.Read();
            Console.Write("Введите фамилию-");
            ReadLine.Send(employee.LastName.Trim());
            employee.LastName = ReadLine.Read();
            Console.Write("Введите зарплату-");
            employee.Salary = InputUpdateSalary(employee.Salary);
            DbWork.UpdateEmployee(employee);
        }

        private static void AddEmployee()
        {
            Employee employee = new Employee();
            employee.EmployeeID = DbWork.GetMaxIdEmployee() + 1;
            Console.Write("Введите дату рождения-");
            employee.DateOfBirth = InputDate();
            Console.Write("Введите Email-");
            employee.Email = Console.ReadLine();
            Console.Write("Введите имя-");
            employee.FirstName = Console.ReadLine();
            Console.Write("Введите фамилию-");
            employee.LastName = Console.ReadLine();
            Console.Write("Введите зарплату-");
            employee.Salary = InputSalary();
            DbWork.AddEmployee(employee);
        }

        private static decimal InputSalary()
        {
            int id;
            string inputId = string.Empty;
            bool correctInput=false;
            do
            {
                inputId = Console.ReadLine();
                if (!int.TryParse(inputId, out id))
                {
                    Console.WriteLine("Ошибка ввода!!! Повторите еще раз");
                }else
                {
                    correctInput = true;
                }
            } while (!correctInput);

            return id;
        }

        private static DateTime InputDate()
        {
            DateTime date;
            string inputDate = string.Empty;
            bool correctInput = false;
            do
            {
                inputDate = Console.ReadLine();
                if (!DateTime.TryParse(inputDate, out date))
                {
                    Console.WriteLine("Ошибка ввода!!! Повторите еще раз");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput);

            return date;
        }
        private static DateTime InputUpdateDate(DateTime oldDate)
        {
            DateTime date;
            string inputDate = string.Empty;
            bool correctInput = false;
            do
            {
                ReadLine.Send(oldDate.ToString("d"));
                inputDate = ReadLine.Read();
                if (!DateTime.TryParse(inputDate, out date))
                {
                    Console.WriteLine("Ошибка ввода!!! Повторите еще раз");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput);

            return date;
        }

        private static decimal InputUpdateSalary(decimal oldSalary)
        {
            int id;
            string inputId = string.Empty;
            bool correctInput = false;
            do
            {
                ReadLine.Send(oldSalary.ToString());
                inputId = ReadLine.Read();
                if (!int.TryParse(inputId, out id))
                {
                    Console.WriteLine("Ошибка ввода!!! Повторите еще раз");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput);

            return id;
        }

        private static void ScreenAllEmployees()
        {
            List<Employee> employees = DbWork.GetAllEmployees();
            foreach (Employee employee in employees)
            {
                Console.WriteLine("|{0,10}|{1,15}|{2,15}|{3,30}|{4,10}|{5,6}|",
                employee.EmployeeID,
                employee.FirstName.Trim(),
                employee.LastName.Trim(),
                employee.Email.Trim(),
                employee.DateOfBirth,
                employee.Salary);
            }
        }

        private static char MainMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Выберите действие");
                Console.WriteLine("1.Добавить нового сотрудника");
                Console.WriteLine("2.Просмотр всех сотрудников");
                Console.WriteLine("3.Обновить информацию о сотруднике");
                Console.WriteLine("4.Удалить сотрудника");
                Console.WriteLine("5.Выйти из приложения");
                key = Console.ReadKey();
                Console.WriteLine(Environment.NewLine);
            } while (!(char.IsDigit(key.KeyChar) || char.IsControl(key.KeyChar)));
            return key.KeyChar;
        }

        private static int InputEmployeerId()
        {
            int id;
            string inputId = string.Empty;
            bool correctInput = false;
            do
            {
                Console.Write("Введите номер сотрудника - ");
                inputId = Console.ReadLine();
                if (!int.TryParse(inputId, out id))
                {
                    Console.WriteLine("Ошибка ввода!!! Повторите еще раз");
                }
                else
                {
                    correctInput = true;
                }
            } while (!correctInput);

            return id;
        }
    }
}