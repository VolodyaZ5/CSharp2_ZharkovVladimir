using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EmployeeApp
{
    /// <summary>
    /// Класс описывает сотрудника
    /// </summary>
    public class Employee : INotifyPropertyChanged
    {
        private string surname; //фамилия
        private string firstName; //имя
        private string lastName; //отчество
        private string position; //должность
        private int lastID = 0; //идентификатор сотрудника

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Surname)));
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.FirstName)));
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.LastName)));
            }
        }
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Position)));
            }
        }
        public int ID { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="fName">Имя</param>
        /// <param name="lName">Отчество</param>
        /// <param name="position">Должность</param>
        public Employee(string surname, string fName, string lName, string position)
        {
            Surname = surname;
            FirstName = fName;
            LastName = lName;
            Position = position;
            ID = lastID++;
        }
    }
}
