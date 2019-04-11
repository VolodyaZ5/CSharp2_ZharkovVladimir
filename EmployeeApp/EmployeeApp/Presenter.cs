using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace EmployeeApp
{
    class Presenter
    {
        public ObservableCollection<Department> DepartmentList;
        private Department departmentEdit;
        private Employee employeeEdit;
        private IView view;
        public Presenter(IView view)
        {
            this.view = view;
            DepartmentList = new ObservableCollection<Department>();            
            for (int i = 0; i < 100; i++)
            {
                DepartmentList.Add(new Department($"Department_{i}"));
                for (int j = 0; j < 100; j++)
                {
                    DepartmentList[i].employeeList.Add(new Employee($"Фамилия_{j}", 
                        $"Имя_{j}", $"Отчество_{j}", $"Должность_{j}"));
                }
            }

            //DepartmentList[0].employeeList.Add(new Employee("Петров", "Александр", "Иванович", "Полиграфист"));
            //DepartmentList[1].employeeList.Add(new Employee("Иванов", "Андрей", "Павлович", "Дизайнер"));
        }
        public ObservableCollection<Employee> EmployeeList
        {
            get => view.CurrentDepartment?.employeeList;
        }
        /// <summary>
        /// Добавление департамента
        /// </summary>
        public void AddDepartment()
        {
            DepartmentList.Add(new Department(view.DepartmentName));
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        public void RemoveDepartment()
        {
            if (view.CurrentDepartment != null)
            {
                view.CurrentDepartment.employeeList.Clear();
                DepartmentList.Remove(view.CurrentDepartment);
            }
        }
        /// <summary>
        /// Переименование департамента
        /// </summary>
        public void RenameDepartment()
        {
            if (view.CurrentDepartment != null)
            {
                view.CurrentDepartment.DepartmentName = view.DepartmentName;
            }
            view.DepartmentName = string.Empty;
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <returns></returns>

        public bool AddEmployee()
        {
            bool addFlag = true;
            try
            {
                view.EditDepartment?.employeeList?.Add(new Employee(view.Surname,
                    view.FirstName, view.LastName, view.Position));
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}", "Ошибка", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                addFlag = false;
            }
            if (addFlag)
            {
                ClearEmployeeText();
            }
            return addFlag;
        }

        /// <summary>
        /// Очистка полей ввода сотрудника
        /// </summary>
        private void ClearEmployeeText()
        {
            view.Surname = string.Empty;
            view.FirstName = string.Empty;
            view.LastName = string.Empty;
            view.Position = string.Empty;
            view.EditDepartment = null;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public void RemoveEmployee()
        {
            view.CurrentDepartment?.employeeList?.Remove(view.CurrentEmployee);
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        public void EditEmployee()
        {
            if (view.CurrentEmployee != null)
            {
                departmentEdit = view.CurrentDepartment;
                employeeEdit = view.CurrentEmployee;
                view.Surname = employeeEdit.Surname;
                view.FirstName = employeeEdit.FirstName;
                view.LastName = employeeEdit.LastName;
                view.Position = employeeEdit.Position;
                view.EditDepartment = view.CurrentDepartment;
            }
        }

        public bool SavingEmployee()
        {
            bool saveFlag = true;
            if (view.EditDepartment != departmentEdit)
            {
                saveFlag = AddEmployee();
                if (saveFlag)
                {
                    departmentEdit.employeeList.Remove(employeeEdit);
                    ClearEmployeeText();
                }
            }
            else
            {
                try
                {
                    employeeEdit.Surname = view.Surname;
                    employeeEdit.FirstName = view.FirstName;
                    employeeEdit.LastName = view.LastName;
                    employeeEdit.Position = view.Position;
                    ClearEmployeeText();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, $"Ошибка!", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    saveFlag = false;
                }
            }
            return saveFlag;
        }
    }
}
