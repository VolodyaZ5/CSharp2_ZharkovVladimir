using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
    class Department
    {
        public string NameOfDepartment { get; set; } //Название
        public string Activity { get; set; } //Деятельность

        public Department() : this(string.Empty, string.Empty) {}
        public Department(string nameOfDept, string activity)
        {
            NameOfDepartment = nameOfDept;
            Activity = activity;
        }

        public override string ToString()
        {
            return string.Format($"{NameOfDepartment}: {Activity}");
        }
    }
}
