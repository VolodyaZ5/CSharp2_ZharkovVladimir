using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace EmployeeApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> listEmployee = new ObservableCollection<Employee>();        
        ObservableCollection<Department> listDepartment = new ObservableCollection<Department>();                        

        EmployeeWindow empWindow = new EmployeeWindow();
        DepartmentWindow depWindow = new DepartmentWindow();

        public MainWindow()
        {
            InitializeComponent();
            FillEmployes();
            FillDepartments();
        }

        private void FillEmployes()
        {
            listEmployee.Add(new Employee()
            {
                FirstName = "Василий",
                LastName = "Иванович",
                Surname = "Шепель",
                Position = "Бухгалтер",
                Department = new Department() { NameOfDepartment = "Бухгалтерия", Activity = "Управление финансовыми потоками организации" }
            });
            listEmployee.Add(new Employee()
            {
                FirstName = "Николай",
                LastName = "Алексеевич",
                Surname = "Киров",
                Position = "Дизайнер",
                Department = new Department() { NameOfDepartment = "Дизайнеры", Activity = "Создание оригинал-макетов заказов" }
            });
            listEmployee.Add(new Employee()
            {
                FirstName = "Алексей",
                LastName = "Сафронович",
                Surname = "Мендель",
                Position = "Менеджер",
                Department = new Department() { NameOfDepartment = "Менеджеры", Activity = "Ведение заказов" }
            });
            listEmployee.Add(new Employee()
            {
                FirstName = "Дмитрий",
                LastName = "Александрович",
                Surname = "Бурый",
                Position = "Дизайнер",
                Department = new Department() { NameOfDepartment = "Дизайнеры", Activity = "Создание оригинал-макетов заказов" }
            });
            listEmployee.Add(new Employee()
            {
                FirstName = "Александр",
                LastName = "Иванович",
                Surname = "Краснов",
                Position = "Полиграфист",
                Department = new Department() { NameOfDepartment = "Полиграфия", Activity = "Производство полиграфической продукции" }
            });
            empWindow.lstEmployee.ItemsSource = listEmployee;
        }
        private void FillDepartments()
        {
            listDepartment.Add(new Department() { NameOfDepartment = "Бухгалтерия", Activity = "Управление финансовыми потоками организации" });
            listDepartment.Add(new Department() { NameOfDepartment = "Дизайнеры", Activity = "Создание оригинал-макетов заказов" });
            listDepartment.Add(new Department() { NameOfDepartment = "Менеджеры", Activity = "Ведение заказов" });
            listDepartment.Add(new Department() { NameOfDepartment = "Полиграфия", Activity = "Производство полиграфической продукции" });
            listDepartment.Add(new Department() { NameOfDepartment = "Водители", Activity = "Доставка материалов и людей" });
            listDepartment.Add(new Department() { NameOfDepartment = "Монтажники", Activity = "Установка изделия на стороне заказчика" });
            depWindow.lstDepartment.ItemsSource = listDepartment;
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            empWindow.Owner = this;
            empWindow.Show();
        }

        private void btnDepartments_Click(object sender, RoutedEventArgs e)
        {
            depWindow.Owner = this;
            depWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
