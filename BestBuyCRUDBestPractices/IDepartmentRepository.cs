using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyCRUDBestPractices
{
    interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
    }
}
