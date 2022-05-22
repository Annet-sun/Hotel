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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class RegistrationClient : Window
    {
        public RegistrationClient()
        {
            InitializeComponent();
        }

        private void zareg_client_Click(object sender, RoutedEventArgs e)
        {
            if (l_fam.Text.Length < 1)
            {
                MessageBox.Show("Фамилия Error");
                return;
            }
            if (l_name.Text.Length < 1)
            {
                MessageBox.Show("Имя Error");
                return;
            }
            if (l_otch.Text.Length < 1)
            {
                MessageBox.Show("Отчество Error");
                return;
            }
            if (l_passport.Text.Length < 1)
            {
                MessageBox.Show("Паспорт Error");
                return;
            }
            if (l_telephone.Text.Length < 1)
            {
                MessageBox.Show("Телефон Error");
                return;
            }
            else
            {
                Client cl = new Client
                {
                    ClientFam = l_fam.Text,
                    ClientName = l_name.Text,
                    ClientOtch = l_otch.Text,
                    ClientPassport = l_passport.Text,
                    ClientTelephone = l_telephone.Text
                };
                AddClient(cl);
          
                this.Close();
                MessageBox.Show("Регистрация гостя прошла успешно");

            }

        }

        private void AddClient(Client client)
        {
            using (hotelContext db = new hotelContext())
            {
                // Добавление
                db.Client.Add(client);
                db.SaveChanges();
            }
        }
    }
}
