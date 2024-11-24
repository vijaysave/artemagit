using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, 68);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Server=WIN-8RVJJGOSUJI\\SQLEXPRESS; Database=123;Integrated Security=True;";
            string username = loginField.Text;
            string password = passField.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Вы вошли", "Успешный вход", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Открыть форму ProductsForm
                        this.Hide(); // Скрываем текущую форму
                        ProductsForm productsForm = new ProductsForm();
                        productsForm.FormClosed += (s, args) => this.Show(); // Показать форму авторизации при закрытии ProductsForm
                        productsForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}