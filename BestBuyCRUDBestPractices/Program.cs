using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BestBuyCRUDBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            DepartmentRepository repo = new DepartmentRepository(connString);

            IEnumerable<Department> DeptList = repo.GetAllDepartments();

            foreach(Department dept in DeptList)
            {
                Console.WriteLine(dept.Name);
            }
        }
    }
}
