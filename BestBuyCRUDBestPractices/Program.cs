using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BestBuyCRUDBestPractices
{
    class Program
    {
        static string response;
        static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            DepartmentRepository repo = new DepartmentRepository(connString);

            IEnumerable<Department> DeptList = repo.GetAllDepartments();

            foreach (Department dept in DeptList)
            {
                Console.WriteLine($"{dept.Name}\n{dept.DepartmentId}");
            }

            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository dapRepo = new DapperDepartmentRepository(conn);
            IEnumerable<Department> Depts = dapRepo.GetAllDepartments();

            do
            {
                Console.WriteLine("Would you like to add a new department?");
                Console.WriteLine("\n   Type Y for yes \n   Type EXIT to exit the program");

                response = Console.ReadLine().ToUpper();

                if (response == "Y")
                {
                    Console.WriteLine("What is the name of the new department?");
                    string departmentName = Console.ReadLine();
                    dapRepo.InsertDepartment(departmentName);

                    Depts = dapRepo.GetAllDepartments();

                    foreach (Department dept in Depts)
                    {
                        Console.WriteLine($"{dept.Name}\n{dept.DepartmentId}");
                    }
                }
            } while (response != "EXIT");

            do
            {
                Console.WriteLine("Would you like to update a department name?");
                Console.WriteLine("\n   Type Y for yes \n   Type EXIT to exit the program");

                response = Console.ReadLine().ToUpper();

                if (response == "Y")
                {
                    Console.WriteLine("What is the ID of the department?");
                    string departmentID = Console.ReadLine();
                    Console.WriteLine("What is the new name of the department?");
                    string departmentName = Console.ReadLine();
                    dapRepo.UpdateDepartment(departmentName, departmentID);

                    Depts = dapRepo.GetAllDepartments();

                    foreach (Department dept in Depts)
                    {
                        Console.WriteLine($"{dept.Name}\n{dept.DepartmentId}");
                    }
                }
            } while (response != "EXIT");


            foreach (Department dept in Depts)
            {
                Console.WriteLine($"{dept.Name}\n{dept.DepartmentId}");
            }

            do
            {
                Console.WriteLine("Would you like to delete a department name?");
                Console.WriteLine("\n   Type Y for yes \n   Type EXIT to exit the program");

                response = Console.ReadLine().ToUpper();

                if (response == "Y")
                {
                    Console.WriteLine("What is the ID of the department?");
                    string departmentID = Console.ReadLine();
                    dapRepo.DeleteDepartment(departmentID);
                    Depts = dapRepo.GetAllDepartments();

                    foreach (Department dept in Depts)
                    {
                        Console.WriteLine($"{dept.Name}\n{dept.DepartmentId}");
                    }
                }


            } while (response != "EXIT");
        }
    }
}
