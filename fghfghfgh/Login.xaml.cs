using hotel.DataAccess.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public static int admin_id;

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (l_passwordadmin.Password.ToString().Length != 0 && l_famadmin.Text.Length != 0) {
                using (hotelContext db = new hotelContext())
                {
                    Administrator user = db.Administrator.Where(p => p.AdministratorFam == l_famadmin.Text && p.AdministratorPassword == l_passwordadmin.Password.ToString()).FirstOrDefault();

                    if (user != null)
                    {
                        admin_id = user.IdAdministrator;
                        MainWindow main = new MainWindow(user.IdAdministrator);
                        main.Show();
                        this.Close();

                    }

                    else
                    {
                        MessageBox.Show("Фамилия или пароль введены не верно");
                    }
                }
            }
            else
            {
                MessageBox.Show("Фамилия и пароль не могут быть пустыми");
            }
        }

        private void btn_registration_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
        }

        private void l_famadmin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        //using (usersContext db = new usersContext())
        //{
        //    object p = db.Users.SelectMany(p => p.Name == name,
        //                                   x => x.Name == text_pass.Text);
        //}
    }

       
  
}
