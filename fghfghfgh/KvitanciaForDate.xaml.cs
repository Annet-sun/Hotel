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
    /// Логика взаимодействия для KvitanciaForDate.xaml
    /// </summary>
    public partial class KvitanciaForDate : Window
    {
        public KvitanciaForDate()
        {
            InitializeComponent();
            list_bron.ItemsSource = getAllKvitancia();
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            if(date1.SelectedDate != null && date2.SelectedDate != null) {
             
                list_bron.ItemsSource = getKvitanciaDate(date1.SelectedDate.Value.ToString("yyyy-MM-dd"), date2.SelectedDate.Value.ToString("yyyy-MM-dd"));
            }
        }

        public ArrayList getAllKvitancia()
        {
            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "select id_reservation, client_fam, client_name, client_otch, administrator_fam,room_id_room, client_id_client,reservation_date_in,reservation_date_out, datediff(reservation_date_out,reservation_date_in) * price as 'count' from  reservation, room, client, administrator where client_id_client = id_client and room_id_room = id_room and administrator_id_administrator = id_administrator order by reservation_date_in desc";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            ArrayList tasks = new ArrayList();
            while (rdr.Read())
            {
                tasks.Add(
                        rdr.GetString(1) + " " + rdr.GetString(2) + " " + rdr.GetString(3) +"  администратор:"+ rdr.GetString(4) + " комната №"+ rdr.GetString(5) + "  с " + rdr.GetString(7) + "  по " + rdr.GetString(8) + " сумма = " + rdr.GetString(9));
            }
            return tasks;
        }

        public ArrayList getKvitanciaDate(String dateOne,String dateTwo)
        {
            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "select id_reservation, client_fam, client_name, client_otch, administrator_fam,room_id_room, client_id_client,reservation_date_in,reservation_date_out, datediff(reservation_date_out,reservation_date_in) * price as 'count' from  reservation, room, client, administrator where  client_id_client = id_client and room_id_room = id_room and administrator_id_administrator = id_administrator and reservation_date_in between '" + dateOne + "' and '" + dateTwo + "' order by reservation_date_in desc";
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            ArrayList tasks = new ArrayList();
            while (rdr.Read())
            {
                tasks.Add(
                        rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetString(4) + "  администратор:" + rdr.GetString(5) + " комната №" + rdr.GetString(6) + "  с " + rdr.GetString(7) + "  по " + rdr.GetString(8) + " сумма = " + rdr.GetString(9));
            }
            return tasks;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
