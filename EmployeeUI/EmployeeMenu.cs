using System;
using System.Collections.Generic;
using System.Text;
using Library;

namespace EmployeeUI
{
    class EmployeeMenu
    {
        private List<Employee> EmployeeList = new List<Employee>();

        public FileRepository _fileRepository = new FileRepository();

        public void Run()
        {
            EmployeeList = _fileRepository.ReadFile();
            FirstMenu();
        }

        public void FirstMenu()
        {
            while (true)
            {
                Console.Clear();
                var employeeRepository = new Employee();
                Console.WriteLine("1: Login as Employee \n2: Register as a Employee");
                int input = int.Parse(Console.ReadLine());
                if (input == 1)
                {
                    LogIn();
                }
                else
                {
                    _fileRepository.CreateEmployee();
                    continue;
                }

                break;
            }
        }

        public void LogIn()
        {
            var loggedIn = false;
            Console.WriteLine("---------- Employee Login ----------");

            while (!loggedIn)
            {
                EmployeeList = _fileRepository.ReadFile();
                Console.Write("\nEnter first name: ");
                var inputName = Console.ReadLine();
                bool wrongName = false;
                foreach (Employee item in EmployeeList)
                {
                    if (item.Name == inputName)
                    {
                        Console.Write("\nEnter password: ");
                        var inputPassword = Console.ReadLine();
                        if (item.Password == inputPassword)
                        {
                            Console.WriteLine("Welcome " + item.Name);
                            loggedIn = true;
                            LoggedInMenu(EmployeeList.IndexOf(item));
                        }
                    }
                }
                Console.WriteLine("Incorrect login data.");
            }

            void LoggedInMenu(int employeeIndex)
            {
                while (loggedIn)
                {
                    EmployeeList = _fileRepository.ReadFile();
                    Console.WriteLine("Welcome! \n 1: Edit your profile \n2: LogOut");
                    int input = int.Parse(Console.ReadLine());
                    if (input == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("1: Edit name\n2: Edit password\n3: Edit address");
                        int inputEdit = int.Parse(Console.ReadLine());
                        if (inputEdit == 1)
                        {
                            Console.WriteLine("Enter new name: ");
                            EmployeeList[employeeIndex].Name = Console.ReadLine();
                        }
                        else if (inputEdit == 2)
                        {
                            Console.WriteLine("Enter new password: ");
                            EmployeeList[employeeIndex].Password = Console.ReadLine();
                        }
                        else if (inputEdit == 3)
                        {
                            Console.WriteLine("Enter new address: ");
                            EmployeeList[employeeIndex].Adress = Console.ReadLine();
                        }

                        _fileRepository.SaveToFile(EmployeeList);
                        Console.WriteLine("Employee successfully edited.");

                    }
                    else
                    {
                        loggedIn = false;
                        Console.WriteLine("Bye!");
                        FirstMenu();
                    }
                }
            }
        }
    }
}
