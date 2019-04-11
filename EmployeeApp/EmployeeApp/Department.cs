using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace EmployeeApp
{
    /// <summary>
    /// Департамент, содержит коллекцию сотрудников
    /// </summary>
    public class Department : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string departmentName; //название департамента

        public ObservableCollection<Employee> employeeList;

        public string DepartmentName
        {
            get { return departmentName; }
            set
            {
                departmentName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.DepartmentName)));
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="depName">Название департамента</param>
        public Department(string depName)
        {
            DepartmentName = depName;
            employeeList = new ObservableCollection<Employee>();
        }
    }
}
