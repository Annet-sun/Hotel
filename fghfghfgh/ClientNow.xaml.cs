using System;
using System.Collections;
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
using MySql.Data.MySqlClient;
using hotel.DataAccess.DataObjects;
using System.Linq;

namespace hotel
{
    /// <summary>
    /// Логика взаимодействия для ClientNow.xaml
    /// </summary>
    public partial class ClientNow : Window
    {
        ArrayList clients;
        public ClientNow()
        {
            InitializeComponent();
            clients= getClientsInHotelNow();
            list_clients_now.ItemsSource = clients;
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            if (t_search.Text.Length > 0)
            {
                ArrayList search_list = new ArrayList();
                String search = t_search.Text.ToLower();
                foreach (object o in clients)
                {
                    if (o.ToString().ToLower().Contains(search))
                    {
                        search_list.Add(o.ToString());
                    }
                }
                list_clients_now.ItemsSource = search_list;
            }
            else list_clients_now.ItemsSource = clients;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
         public ArrayList getClientsInHotelNow()
        {
            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "select client_id_client, room_id_room, client_fam, client_name, client_otch, reservation_date_out , client_passport, client_telephone from client, reservation " +
                "where client_id_client = id_client and CURDATE() between " +
                "reservation_date_in and reservation_date_out ORDER BY room_id_room";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            ArrayList tasks = new ArrayList();
            while (rdr.Read())
            {
                tasks.Add("к. " + rdr.GetString(1) + "  " +
                        rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetString(4) + "  до " + rdr.GetString(5)+"  пасспорт: "+ rdr.GetString(6)+ "  телефон: "+ rdr.GetString(7));
            }
            return tasks;
        }
    }
}
