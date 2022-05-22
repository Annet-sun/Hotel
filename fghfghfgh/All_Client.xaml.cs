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
using MySql.Data.MySqlClient;
using hotel.DataAccess.DataObjects;
using System.Linq;

namespace hotel
{
    /// <summary>
    /// Логика взаимодействия для All_Client.xaml
    /// </summary>
    public partial class All_Client : Window
    {
        public All_Client()
        {
            InitializeComponent();
            grid_Loaded();
        }

        private void grid_Loaded()
        {
            List<Client> result = new List<Client>();

            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM Client";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                result.Add(new Client(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5)));
            }
            
            table_grid.ItemsSource = result;
        }
        
      
    }
}
