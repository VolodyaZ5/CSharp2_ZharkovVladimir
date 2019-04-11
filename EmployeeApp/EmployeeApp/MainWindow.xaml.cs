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

namespace EmployeeApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter p;
        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);

            cmbDepartment.ItemsSource = p.DepartmentList;
            cmbEditDepartment.ItemsSource = p.DepartmentList;
            btnSaveEmployee.IsEnabled = false;
            btnCancelSaveEmployee.IsEnabled = false;
            btnAddDepartment.IsEnabled = false;

            btnDeleteDepartment.Click += delegate { p.RemoveDepartment(); };
            btnRenameDepartment.Click += delegate { p.RenameDepartment(); };
            btnAddDepartment.Click += delegate { p.AddDepartment(); };
        }


        #region Реализация интерфейса
        public string Surname { get => txtSurname.Text; set => txtSurname.Text = value; }
        public string FirstName { get => txtFirstName.Text; set => txtFirstName.Text = value; }
        public string LastName { get => txtLastName.Text; set => txtLastName.Text = value; }        
        public string DepartmentName { get => txtDepartment.Text; set => txtDepartment.Text = value; }

        public Department CurrentDepartment => (cmbDepartment.SelectedItem as Department);

        public Department EditDepartment { get => (cmbEditDepartment.SelectedItem as Department); set => cmbEditDepartment.SelectedItem = value; }

        public Employee CurrentEmployee => (lstEmployee.SelectedItem as Employee);

        public string Position { get => txtPosition.Text; set => txtPosition.Text = value; }
        #endregion

        private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDepartment.SelectedIndex > -1)
            {
                lstEmployee.ItemsSource = p.EmployeeList;
            }
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (lstEmployee.SelectedIndex > -1)
            {
                p.EditEmployee();
                btnSaveEmployee.IsEnabled = true;
                btnCancelSaveEmployee.IsEnabled = true;
                btnAddNewEmployee.IsEnabled = false;
            }
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (txtSurname.Text.Length > 0 && txtFirstName.Text.Length > 0 &&
                txtLastName.Text.Length > 0 && txtPosition.Text.Length > 0 &&
                cmbEditDepartment.SelectedIndex > -1)
            {
                p.AddEmployee();
            }
            else
            {
                MessageBox.Show($"Заполните все поля", $"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDepartment.SelectedIndex > -1 && lstEmployee.SelectedIndex > -1)
            {
                p.RemoveEmployee();
            }
        }

        /// <summary>
        /// Сохранение после редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (cmbEditDepartment.SelectedIndex > -1)
            {
                if (p.SavingEmployee())
                {
                    FinishEdit();
                }
            }
        }

        /// <summary>
        /// Сделать кнопки сохранения и отмены недоступными, а 
        /// кнопку добавления нового сотрудника доступной
        /// </summary>
        private void FinishEdit()
        {
            btnSaveEmployee.IsEnabled = false;
            btnCancelSaveEmployee.IsEnabled = false;
            btnAddNewEmployee.IsEnabled = true;
        }
        private void btnCancelSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            FinishEdit();
            txtSurname.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPosition.Text = string.Empty;
            cmbEditDepartment.SelectedIndex = -1;
        }

        private void txtDepartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtDepartment.Text.Length > 0)
            {
                btnAddDepartment.IsEnabled = true;
            }
            else
            {
                btnAddDepartment.IsEnabled = false;
            }
        }
    }
}
