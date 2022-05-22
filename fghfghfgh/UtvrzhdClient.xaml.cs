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
using hotel.DataAccess.DataObjects;

namespace hotel
{
    /// <summary>
    /// Логика взаимодействия для UtvrzhdClient.xaml
    /// </summary>
    public partial class UtvrzhdClient : Window
    {
        Client clientbr;
        public UtvrzhdClient(Client client)
        {
            clientbr = client;
            InitializeComponent();
            l_name.Text=client.ClientName;
            l_fam.Text=client.ClientFam;
            l_telephone.Text=client.ClientTelephone;
            l_otch.Text=client.ClientOtch;
            l_passport.Text=client.ClientPassport;
        }

        private void btn_update_cl_Click(object sender, RoutedEventArgs e)
        {
            Update_Client main = new Update_Client(clientbr);
            main.Show();
            this.Close();
        }

        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            New_Bron main = new New_Bron(clientbr);
            main.Show();
            this.Close();
        }
    }
}
