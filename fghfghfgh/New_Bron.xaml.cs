using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using  static hotel.Login;

namespace hotel
{
    /// <summary>
    /// Логика взаимодействия для New_Bron.xaml
    /// </summary>
    public partial class New_Bron : Window
    {
        Client clientbr;
        public New_Bron(Client cl)
        {
            InitializeComponent();
            clientbr = cl;
            l_fio.Text=clientbr.ClientFam + " " + clientbr.ClientName + " " + clientbr.ClientOtch;
            choice_box_category.Items.Add("Эконом");
            choice_box_category.Items.Add("Комфорт");
            choice_box_category.Items.Add("Люкс");

            String[] kolmest = { "1", "2", "3", "4" };
            foreach (var item in kolmest)
            {
                choice_box_kolmest.Items.Add(item);
            }
        }

        private void btn_bron_Click(object sender, RoutedEventArgs e)
        {
           Reservation reservation = new Reservation
            {
                ReservationDateIn = DateTime.Now,
                ReservationDateOut = date_out.SelectedDate.Value,
                AdministratorIdAdministrator=admin_id,
                ClientIdClient = clientbr.IdClient,
                RoomIdRoom = Int32.Parse(choice_box_room.SelectedItem.ToString())
            }; 
            AddReservation(reservation);
            this.Close();
            MessageBox.Show("Номер был успешно забронирован.");
        }

        private void AddReservation(Reservation r)
        {
            using (hotelContext db = new hotelContext())
            {
                // Добавление
                db.Reservation.Add(r);
                db.SaveChanges();
            }
        }

        private void btn_bron_and_bill_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*",
                // Set other options depending on your needs ...
            };
            if (sfd.ShowDialog() == true)
            { // Returns a bool?, therefore the == to convert it into bool.
                string filename = sfd.FileName;
                // Save the file ...

                string inputFormat = "dd-MM-yyyy HH:mm:ss";
                string outputFormat = "yyyy-MM-dd"; 

                Reservation reservation = new Reservation
                {
                    ReservationDateIn = DateTime.Now,
                    ReservationDateOut = date_out.SelectedDate.Value,
                    AdministratorIdAdministrator = admin_id,
                    ClientIdClient = clientbr.IdClient,
                    RoomIdRoom = Int32.Parse(choice_box_room.SelectedItem.ToString())
                };
                AddReservation(reservation);
                String sampleText = "Мы рады приветствовать вас в нашей гостинице!" +
               "\nУважаемый(ая) " + clientbr.ClientFam + " " + clientbr.ClientName + " " + clientbr.ClientOtch + ", Вы будете проживать в номере " + choice_box_room.SelectedItem.ToString() + " с " + DateTime.Now.ToLongDateString() + " по " + date_out.SelectedDate.Value.ToLongDateString() + "" +
               "\nВ стоимость вашего номера так же входят такие услуги, как уборка номера, смена постельного белья и завтрак." +
               "\nСумма проживания в номере за сутки = " + l_prise.Text + " руб." +
               "\nИтого к оплате: " + l_day.Text + " * " + l_prise.Text + " руб. = " + l_sum.Text + " руб. \nБлагодарим, что выбрали нашу гостиницу. \nМы искренне надеемся, что Вы получили удовольствие от проживания у нас \nи выберите нас снова!";
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine(sampleText);
                }
                this.Close();
                MessageBox.Show("Номер был успешно забронирован и сохранена квитанция.");
            }
        }

        private void change_category(object sender, SelectionChangedEventArgs e)
        {

        }

        private void change_kol_mest(object sender, SelectionChangedEventArgs e)
        {

        }

        private void change_date(object sender, SelectionChangedEventArgs e)
        {
            var room = getRoomFreeNowFilter(choice_box_category.SelectedIndex + 1, choice_box_kolmest.SelectedIndex + 1);
            choice_box_room.Items.Clear();
            foreach (var item in room)
            {
                choice_box_room.Items.Add(item);
            }
            DateTime febDate = DateTime.Today;
            DateTime pickerDate = date_out.SelectedDate.Value;
            TimeSpan tspan = pickerDate-febDate;

            int differenceInDays = tspan.Days;

            l_day.Text = differenceInDays.ToString();
        }

        private void change_room(object sender, SelectionChangedEventArgs e)
        {
            double price = getPriceRoom(choice_box_room.SelectedItem.ToString());
            l_prise.Text= price.ToString();
            l_sum.Text = (price * Int32.Parse(l_day.Text)).ToString();
        }

        public ArrayList getRoomFreeNowFilter(int cat, int kol) {
            string cs = @"server=localhost;userid=root;password=root;database=hotel";
            String stm = "select id_room  from room\n" +
                "join category on category_id_category=id_category \n" +
                "left join reservation on (reservation.room_id_room = room.id_room and \n" +
                "datediff(CURDATE(),reservation_date_out)<0)\n" +
                "where reservation.room_id_room is null and id_category =" + cat + " and room_kolmest =" + kol + "\n";

            using var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySqlCommand(stm, con);
            
            using MySqlDataReader rdr = cmd.ExecuteReader();

            ArrayList tasks = new ArrayList();
            while (rdr.Read())
            {
                tasks.Add(rdr.GetValue(0));
            }
            return tasks;
    }
        public Double getPriceRoom(String id_room){
            string cs = @"server=localhost;userid=root;password=root;database=hotel";
            String sql = "select price  from room\n" +
                "where id_room =" + id_room + "\n";
            using var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            Double tasks = 0.0;
            while (rdr.Read())
            {
                tasks = Double.Parse(rdr.GetString(0));
            }
            return tasks;
    }
}
}
