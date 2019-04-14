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
using System.Data.SqlClient;
using System.Data;

namespace EmployeeApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        Presenter p;

        SqlConnection connection;
        SqlDataAdapter adapterEmployee;
        SqlDataAdapter adapterDepartment;
        DataTable dtEmployee;
        DataTable dtDepartment;
        SqlCommand commandSelect;

        public MainWindow()
        {
            InitializeComponent();
            p = new Presenter(this);

            SqlConnectionStringBuilder connectionStrBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "DepDB",
                IntegratedSecurity = true,
                Pooling = true
            };
            connection = new SqlConnection(connectionStrBuilder.ConnectionString);
            
            InitDBDepartment();
            InitDBEmployee();
                        
            dtEmployee = new DataTable();
            dtDepartment = new DataTable();

            adapterEmployee.Fill(dtEmployee);
            adapterDepartment.Fill(dtDepartment);

            //for (int i = 0; i < 100; i++)
            //{
            //    DataRow newRow = dtDepartment.NewRow();                
            //    newRow["DepartmentName"] = $"Департамент {i}";
            //    dtDepartment.Rows.Add(newRow);
            //    adapterDepartment.Update(dtDepartment);
            //    var depID = dtDepartment.Select().Where((e) => e["DepartmentName"].Equals($"Департамент {i}")).Select((e) => e["Id"]);
            //    for (int j = 0; j < 100; j++)
            //    {
            //        newRow = dtEmployee.NewRow();
            //        newRow["Surname"] = $"Фамилия_{j}";
            //        newRow["FirstName"] = $"Имя_{j}";
            //        newRow["LastName"] = $"Отчество_{j}";
            //        newRow["Position"] = $"Должность_{j}";
            //        newRow["DepId"] = depID.First();
            //        dtEmployee.Rows.Add(newRow);
            //    }
            //    adapterEmployee.Update(dtEmployee);
            //}

            lstEmployee.DataContext = dtEmployee.DefaultView;
            cmbDepartment.ItemsSource = dtDepartment.DefaultView;

            cmbDepartment.SelectionChanged += cmbDepartment_SelectionChanged;
            btnDeleteDepartment.Click += BtnDeleteDepartment_Click;



            //cmbDepartment.ItemsSource = p.DepartmentList;
            //cmbEditDepartment.ItemsSource = p.DepartmentList;
            //btnSaveEmployee.IsEnabled = false;
            //btnCancelSaveEmployee.IsEnabled = false;
            //btnAddDepartment.IsEnabled = false;

            //btnDeleteDepartment.Click += delegate { p.RemoveDepartment(); };
            //btnRenameDepartment.Click += delegate { p.RenameDepartment(); };
            //btnAddDepartment.Click += delegate { p.AddDepartment(); };
        }

        private void BtnDeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            DataRowView curRow = (DataRowView)cmbDepartment.SelectedItem;

            curRow?.Row?.Delete();
            adapterDepartment.Update(dtDepartment);
        }

        private void InitDBDepartment()
        {
            adapterDepartment = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Id, DepartmentName FROM Department", connection);            
            adapterDepartment.SelectCommand = command;

            command = new SqlCommand(@"INSERT INTO Department (DepartmentName) VALUES (@DepartmentName); SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 50, "DepartmentName");
            SqlParameter parameter = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.Direction = ParameterDirection.Output;
            adapterDepartment.InsertCommand = command;

            command = new SqlCommand(@"UPDATE Department SET DepartmentName = @DepartmentName WHERE Id = @Id", connection);
            command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 50, "DepartmentName");
            parameter = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.SourceVersion = DataRowVersion.Original;
            adapterDepartment.UpdateCommand = command;

            command = new SqlCommand(@"DELETE FROM Department WHERE Id=@Id", connection);
            parameter = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.SourceVersion = DataRowVersion.Original;
            adapterDepartment.DeleteCommand = command;

        }

        private void InitDBEmployee()
        {
            adapterEmployee = new SqlDataAdapter();
            commandSelect = new SqlCommand("SELECT Id, Surname, FirstName, LastName, Position, DepId FROM Employee WHERE DepId=@DepId", connection);
            commandSelect.Parameters.AddWithValue("@DepId", 0);
            adapterEmployee.SelectCommand = commandSelect;

            SqlCommand command = new SqlCommand(@"INSERT INTO Employee (Surname, FirstName, LastName, Position, DepId)
                VALUES (@Surname, @FirstName, @LastName, @Position, @DepId); SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, 50, "Surname");
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@Position", SqlDbType.NVarChar, 50, "Position");
            command.Parameters.Add("@DepId", SqlDbType.Int, 0, "DepId");
            SqlParameter parameter = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.Direction = ParameterDirection.Output;
            adapterEmployee.InsertCommand = command;

            command = new SqlCommand(@"UPDATE Employee SET Surname=@Surname, FirstName=@FirstName, LastName=@LastName,
                Position=@Position, DepId=@DepId WHERE Id=@Id", connection);
            command.Parameters.Add("@Surname", SqlDbType.NVarChar, 50, "Surname");
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50, "LastName");
            command.Parameters.Add("@Position", SqlDbType.NVarChar, 50, "Position");
            command.Parameters.Add("@DepId", SqlDbType.Int, 0, "DepId");
            parameter = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            parameter.SourceVersion = DataRowVersion.Original;
            adapterEmployee.UpdateCommand = command;

            command = new SqlCommand(@"DELETE FROM Employee WHERE Id=@Id", connection);
            parameter = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            adapterEmployee.DeleteCommand = command;
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
            DataRowView curRow = (DataRowView)cmbDepartment.SelectedItem;
            commandSelect.Parameters["@DepId"].Value = curRow?.Row["Id"] ?? 0;
            dtEmployee.Clear();
            adapterEmployee.Fill(dtEmployee);

            //if (cmbDepartment.SelectedIndex > -1)
            //{
            //    lstEmployee.ItemsSource = p.EmployeeList;
            //}
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
