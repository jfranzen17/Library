using System;
using System.Collections.Generic;
using System.IO;

namespace Library
{

    public class FileRepository
    {
        string _path;
        List<Employee> EmployeeList = new List<Employee>();
        public FileRepository()
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dir, "emploeeyes.csv");
            _path = path;
            if (!File.Exists(_path))
                File.Create(_path).Close();
            EmployeeList = ReadFile();
        }

        public void SaveToFile(List<Employee> EmployeeList)
        {
            using (StreamWriter sw = File.CreateText(_path))
            {
                foreach (var employee in EmployeeList)
                {
                    var csv = $"{employee.Id},{employee.Name},{employee.Password},{employee.Adress},{employee.IsAdmin}";
                    sw.WriteLine(csv);
                }
            }
        }
        public List<Employee> ReadFile()
        {
            List<Employee> EmployeeList = new List<Employee>();
            using (StreamReader sr = File.OpenText(_path))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    string getId = line.Split(',')[0];
                    string getName = line.Split(',')[1];
                    string getPassword = line.Split(',')[2];
                    string getAdress = line.Split(',')[3];
                    string getAdmin = line.Split(',')[4];

                    Employee employee = new Employee();
                    employee.Id = getId;
                    employee.Name = getName;
                    employee.Password = getPassword;
                    employee.Adress = getAdress;
                    employee.IsAdmin = Convert.ToBoolean(getAdmin);

                    EmployeeList.Add(employee);          
                }
            }
            return EmployeeList;
        }
        public void CreateAdmin()
        {
            Employee employee = new Employee();
            Guid id = Guid.NewGuid();
            Console.WriteLine($"New employee ID: {id}");
            employee.Id = id.ToString();
            Console.WriteLine("Enter password: ");
            employee.Password = Console.ReadLine();
            Console.WriteLine("Enter name: ");
            employee.Name = Console.ReadLine();
            Console.WriteLine("Enter adress: ");
            employee.Adress = Console.ReadLine();
            Console.WriteLine("Should the employee have Admin access? \n 1: Yes \n 2: No");
            int adminAccess = int.Parse(Console.ReadLine());
            if (adminAccess == 1)
            {
                employee.IsAdmin = true;
            }
            else
            {
                employee.IsAdmin = false;
            }
            EmployeeList.Add(employee);
            SaveToFile(EmployeeList);
        }
        public void CreateEmployee()
        {
            Employee employee = new Employee();
            Guid id = Guid.NewGuid();
            Console.WriteLine($"New employee ID: {id}");
            employee.Id = id.ToString();
            Console.WriteLine("Enter password: ");
            employee.Password = Console.ReadLine();
            Console.WriteLine("Enter name: ");
            employee.Name = Console.ReadLine();
            Console.WriteLine("Enter adress: ");
            employee.Adress = Console.ReadLine();
            employee.IsAdmin = false;
            EmployeeList.Add(employee);
            SaveToFile(EmployeeList);
        }
    }
}