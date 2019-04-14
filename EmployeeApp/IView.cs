using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    public interface IView
    {
        string Surname { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }        
        string Position { get; set; }
        string DepartmentName { get; set; }
        Department CurrentDepartment { get; }
        Department EditDepartment { get; set; }
        Employee CurrentEmployee { get; }

    }
}
