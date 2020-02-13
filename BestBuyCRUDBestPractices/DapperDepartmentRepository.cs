using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BestBuyCRUDBestPractices
{
    class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection;
        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM Departments;").ToList();
        }

        public void InsertDepartment(string department)
        {
            _connection.Execute("INSERT INTO Departments (Name) VALUES (@departmentName)", new { departmentName = department });
        }

        public void UpdateDepartment(string department, string id)
        {
            _connection.Execute("UPDATE Departments SET Name = (@deptName) WHERE departmentID = (@deptId)", new { deptName = department, deptId = id });
        }

        public void DeleteDepartment(string id)
        {
            _connection.Execute("DELETE FROM Departments WHERE departmentId = (@deptId)", new { deptId = id });
        }
    }
}
