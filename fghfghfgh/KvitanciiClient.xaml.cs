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
using System.IO;
using Microsoft.Win32;

namespace hotel
{
    /// <summary>
    /// Логика взаимодействия для KvitanciiClient.xaml
    /// </summary>
    public partial class KvitanciiClient : Window
    {
        Client client;
        public KvitanciiClient(Client user)
        {
            InitializeComponent();
            client = user;
            label_fio.Text = user.ClientFam + " " + user.ClientName + " " + user.ClientOtch;
            grid_Loaded();
        }
        private void grid_Loaded()
        {
            List<Reservation> result = new List<Reservation>();

            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            string sql = "SELECT *  from  reservation where  client_id_client = " + client.IdClient + " order by reservation_date_in desc";
           
            using var cmd = new MySqlCommand(sql, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                result.Add(new Reservation(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetDateTime(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5)));
            }

            table_grid.ItemsSource = result;
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reservation bron = (Reservation)table_grid.SelectedValue;
            
                if (bron != null)
                {
                    string cs = @"server=localhost;userid=root;password=root;database=hotel";
                    using var con = new MySqlConnection(cs);
                    con.Open();
                    string sql = "select price from room where id_room =" + bron.RoomIdRoom;
                    using var cmd = new MySqlCommand(sql, con);
                    using MySqlDataReader rdr = cmd.ExecuteReader();
                    double price = 0;
                    while (rdr.Read())
                    {
                        price = rdr.GetDouble(0);
                    }
                    TimeSpan tspan = (TimeSpan)(bron.ReservationDateOut - bron.ReservationDateIn);
                    int day = tspan.Days;


                    String sampleText = "Мы рады приветствовать вас в нашей гостинице!" +
                    "\nУважаемый(ая) " + client.ClientFam + " " + client.ClientName + " " + client.ClientOtch + ", Вы будете проживать в номере " + bron.RoomIdRoom.ToString() + " с " + bron.ReservationDateIn.ToString() + " по " + bron.ReservationDateOut.ToString() + "" +
                    "\nВ стоимость вашего номера так же входят такие услуги, как уборка номера, смена постельного белья и завтрак." +
                    "\nСумма проживания в номере за сутки = " + price + " руб." +
                    "\nИтого к оплате: " + day + " * " + price + " руб. = " + day * price + " руб. \nБлагодарим, что выбрали нашу гостиницу. \nМы искренне надеемся, что Вы получили удовольствие от проживания у нас \nи выберите нас снова!";

                    var sfd = new SaveFileDialog
                    {
                        Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
                        // Set other options depending on your needs ...
                    };
                    if (sfd.ShowDialog() == true)
                    { // Returns a bool?, therefore the == to convert it into bool.
                        string filename = sfd.FileName;
                        using (StreamWriter writer = new StreamWriter(filename, true))
                        {
                            writer.WriteLine(sampleText);
                        }
                        this.Close();
                        MessageBox.Show("Квитанция была успешно сохранена.");
                        
                    }
                }
            
            
        }
    }
}
