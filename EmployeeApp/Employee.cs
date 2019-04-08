using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace EmployeeApp
{
    class Employee
    {
        public string Surname { get; set; } //Фамилия
        public string FirstName { get; set; } //Имя
        public string LastName { get; set; } //Отчество
        public string Position { get; set; } //Должность
        public Department Department { get; set; } //Отдел        
        
        public Employee() : this(string.Empty, string.Empty, string.Empty, string.Empty, null)
        {
            
        }
        private Employee(string surname, string fName, string lName, string pos, Department dep)
        {
            Surname = surname;
            FirstName = fName;
            LastName = lName;
            Position = pos;
            Department = dep;
        }

        public override string ToString()
        {
            return string.Format($"{Surname} {FirstName} {LastName}, должность: {Position}");
        }

        public void Add(EmployeeWindow window)
        {
            if (window.txtFirstName.Text.Length == 0)
            {
                MessageBox.Show($"Введите имя!");
            }
            else
            {
                FirstName = window.txtFirstName.Text;
            }

            if (window.txtLastName.Text.Length == 0)
            {
                MessageBox.Show($"Введите Отчество");
            }
            else
            {
                LastName = window.txtLastName.Text;
            }
            if (window.txtSurname.Text.Length == 0)
            {
                MessageBox.Show("Введите фамилию");
            }
            else
            {
                Surname = window.txtSurname.Text;
            }
            if (window.txtPosition.Text.Length == 0)
            {
                MessageBox.Show("Введите должность");
            }
            else
            {
                Position = window.txtPosition.Text;
            }
        }


    }
}
