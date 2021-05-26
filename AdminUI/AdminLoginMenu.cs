using System;
using System.Collections.Generic;
using System.Text;
using Library;

namespace AdminUI
{
    public class AdminLoginMenu
    {
        private List<Employee> EmployeeList = new List<Employee>();
        public FileRepository _fileRepository = new FileRepository();
        public void Run()
        {
            EmployeeList = _fileRepository.ReadFile();
            Console.WriteLine("1: Login as Admin \n2: Register as a Admin");
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                LogIn();
            }
            else
            {
                Employee employee = new Employee();
                Guid id = Guid.NewGuid();
                Console.WriteLine($"New ID: {id} \n Enter password: ");
                employee.Id = id.ToString();
                employee.Password = Console.ReadLine();
                Console.WriteLine("Enter your name: ");
                employee.Name = Console.ReadLine();
                Console.WriteLine("Enter your adress: ");
                employee.Adress = Console.ReadLine();
                employee.IsAdmin = true;
                EmployeeList.Add(employee);
                _fileRepository.SaveToFile(EmployeeList);
                Console.WriteLine("Thank You");
                LogIn();
            }

            bool LogIn()
            {
                EmployeeList = _fileRepository.ReadFile();
                Console.WriteLine("---------- Admin Login ----------");
                Employee employee = null;
                do
                {
                    Console.Write("\nEnter first name: ");
                    var inputName = Console.ReadLine();
                    employee = GetAllNames(inputName);
                    if (employee == null)
                    {
                        Console.WriteLine("Incorrect name.");
                    }
                    if (employee != null)
                    {
                        if (!employee.IsAdmin)
                        {
                            Console.WriteLine("Employee is not an admin.");
                            employee = null;
                            continue;
                        }
                        var _latestEmployee = employee;
                        var inputPassword = string.Empty;

                        Console.Write("\nEnter password: ");
                        inputPassword = Console.ReadLine();
                        if (inputPassword != _latestEmployee.Password)
                        {
                            Console.WriteLine("Incorrect password.");
                            employee = null;
                            continue;
                        }
                        Console.WriteLine($"Welcome {_latestEmployee.Name}!");
                        LoggedInMenu();
                    }
                } while (employee == null);

                return false;
            }
            Employee GetAllNames(string Name)
            {
                foreach (var employee in EmployeeList)
                {
                    if (Name == employee.Name)
                    {
                        return employee;
                    }
                }
                return null;
                {

                    Console.WriteLine("---------- Admin Login ----------\n 1: Create an Employee \n 2: Remove an Employee");
                    int input = int.Parse(Console.ReadLine());
                    if (input == 1)
                    {
                        //CreateEmployee();

                    }
                    if (input == 2)
                    {
                        RemoveEmployee(EmployeeList);
                    }
                }
            }
            void LoggedInMenu()
            {
                while (true)
                {
                    Console.WriteLine("---------- Admin Login ----------\n1: Create an Admin \n2: Remove an Employee\n3: Print all Employees\n4: Logout");
                    int input = int.Parse(Console.ReadLine());
                    EmployeeList = _fileRepository.ReadFile();
                    if (input == 1)
                    {
                        Console.Clear();
                        _fileRepository.CreateAdmin();

                    }
                    if (input == 2)
                    {
                        RemoveEmployee(EmployeeList);
                    }
                    if (input == 3)
                    {
                        Console.Clear();
                        //Update EmployeeList
                        EmployeeList = _fileRepository.ReadFile();

                        //Print all employees
                        foreach (var employee in EmployeeList)
                            Console.WriteLine("ID: " + employee.Id + "  |  " + "Name: " + employee.Name + "  |  "
                                              + "Adress: " + employee.Adress + "  |  " + "IsAdmin: " + employee.IsAdmin);
                        //---
                    }
                    if (input == 4)
                        break;
                }
                Console.Clear();
                Run();
            }

            void RemoveEmployee(List<Employee> EmployeeList)
            {

                Console.Clear();
                Console.WriteLine("What name has the Employee you want to remove?");
                string nameToRemove = Console.ReadLine();
                int i = 0;
                var employeeToRemove = new Employee();
                foreach (var employee in EmployeeList)
                    if (employee.Name == nameToRemove)
                        employeeToRemove = employee;
                this.EmployeeList.Remove(employeeToRemove);
                _fileRepository.SaveToFile(this.EmployeeList);
                i++;
                LoggedInMenu();
            }


        }
    }
}
