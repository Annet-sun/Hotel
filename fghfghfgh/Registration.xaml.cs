using hotel.DataAccess.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hotel
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            if (l_fam.Text.Length < 1)
            {
                MessageBox.Show("Фамилия Error");
                return;
            }
            if (l_name.Text.Length < 1)
            {
                MessageBox.Show("Инициалы Error");
                return;
            }
            if (l_reg_password.Text.Length < 4)
            {
                MessageBox.Show("Пароль should be at least 4 characters long");
                return;
            }
            else
            {
                Administrator admin = new Administrator
                {
                    AdministratorFam = l_fam.Text,
                    AdministratorIo = l_name.Text,
                    AdministratorPassword = l_reg_password.Text
                };
                AddAdmin(admin);
               
                this.Close();
                MessageBox.Show("Администратор был успешно зарегистрирован");
                
                
            }        
            
        }

        private void AddAdmin(Administrator admin)
        {
            using (hotelContext db = new hotelContext())
            {
                // Добавление
                db.Administrator.Add(admin);
                db.SaveChanges();
            }
        }
    }
}
