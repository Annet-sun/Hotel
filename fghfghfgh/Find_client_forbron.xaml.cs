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
    /// Логика взаимодействия для Find_client_forbron.xaml
    /// </summary>
    public partial class Find_client_forbron : Window
    {
        ArrayList allclient;
        public Find_client_forbron()
        {
            InitializeComponent();

            allclient = getAllClients();
            list_clients.ItemsSource = allclient;
        }

        public ArrayList getAllClients() {
            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM Client";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            ArrayList tasks = new ArrayList();
            while (rdr.Read())
            {
                tasks.Add(rdr.GetString(0)+" "+rdr.GetString(1)+" "+
                        rdr.GetString(2)+" "+rdr.GetString(3));
            }
            return tasks;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String cl=list_clients.SelectedValue.ToString();
            string[] words = cl.Split(' ');
            int idcl = Int32.Parse( words.GetValue(0).ToString());
            using (hotelContext db = new hotelContext())
            {
                Client user = db.Client.Where(p => p.IdClient == idcl).FirstOrDefault();

                if (user != null)
                {
                    UtvrzhdClient main = new UtvrzhdClient(user);
                    main.Show();
                    this.Close();
                }
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            if (t_search.Text.Length > 0)
            {
                ArrayList search_list = new ArrayList();
                String search = t_search.Text.ToLower();
                foreach (object o in allclient)
                {
                    if (o.ToString().ToLower().Contains(search))
                    {
                        search_list.Add(o.ToString());
                    }
                }
                list_clients.ItemsSource= search_list;
            }
            else list_clients.ItemsSource = allclient;
        }
    }
}
