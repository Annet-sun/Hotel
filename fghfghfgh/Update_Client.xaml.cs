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
using hotel.DataAccess.DataObjects;

namespace hotel
{
    /// <summary>
    /// Логика взаимодействия для UpdateClient.xaml
    /// </summary>
    public partial class Update_Client : Window
    {
        Client cl;
        public Update_Client(Client client)
        {
            InitializeComponent();
            cl=client;
            l_name.Text= client.ClientName;
            l_fam.Text = client.ClientFam;
            l_telephone.Text = client.ClientTelephone;
            l_otch.Text = client.ClientOtch;
            l_passport.Text = client.ClientPassport;
        }

        private void btn_update_client_Click(object sender, RoutedEventArgs e)
        {
            cl.ClientName = l_name.Text;
            cl.ClientFam = l_fam.Text;
            cl.ClientTelephone = l_telephone.Text;
            cl.ClientOtch = l_otch.Text;
            cl.ClientPassport = l_passport.Text;
            UpdateClient(cl);
            New_Bron main = new New_Bron(cl);
            main.Show();
            this.Close();


        }

        private void UpdateClient(Client cli)
        {
            using (hotelContext db = new hotelContext())
            {
                Client user = db.Client.Where(p => p.IdClient == cli.IdClient).FirstOrDefault();
           
                user.ClientName = cli.ClientName;
                user.ClientFam = cli.ClientFam;
                user.ClientTelephone = cli.ClientTelephone;
                user.ClientOtch = cli.ClientOtch;
                user.ClientPassport = cli.ClientPassport;
               
                    //обновляем объект
                    db.Client.Update(user);
                    db.SaveChanges();
                }

            }
        }
    }
