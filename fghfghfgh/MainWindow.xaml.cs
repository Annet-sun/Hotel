using hotel.DataAccess.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Administrator admin;
        public MainWindow(int idadmin)
        {
            InitializeComponent();
            using (hotelContext db = new hotelContext())
            {
                admin = db.Administrator.Where(p => p.IdAdministrator == idadmin).FirstOrDefault();
                l_admin_fio.Content = admin.AdministratorFam + " " + admin.AdministratorIo;


            }
            place1_1category.Content= getCountRoomFreeNowFilter(1, 1);
            place2_1category.Content = getCountRoomFreeNowFilter(1, 2);
            place3_1category.Content = getCountRoomFreeNowFilter(1, 3);
            place4_1category.Content = getCountRoomFreeNowFilter(1, 4);
            place1_2category.Content = getCountRoomFreeNowFilter(2, 1);
            place2_2category.Content = getCountRoomFreeNowFilter(2, 2);
            place3_2category.Content = getCountRoomFreeNowFilter(2, 3);
            place4_2category.Content = getCountRoomFreeNowFilter(2, 4);
            place1_3category.Content = getCountRoomFreeNowFilter(3, 1);
            place2_3category.Content = getCountRoomFreeNowFilter(3, 2);
            place3_3category.Content = getCountRoomFreeNowFilter(3, 3);
            place4_3category.Content = getCountRoomFreeNowFilter(3, 4);
        }

        public String getCountRoomFreeNowFilter(int cat, int kol)
        {
            string cs = @"server=localhost;userid=root;password=root;database=hotel";

            using var con = new MySqlConnection(cs);
            con.Open();

            var stm = "select Count(id_room)  from room\n" +
                    "join category on category_id_category=id_category and id_category =" + cat + " \n" +
                    "left join reservation on (reservation.room_id_room = room.id_room and room_kolmest =" + kol + " and \n" +
                    " \n" +
                    "datediff(CURDATE(),reservation_date_out)<0)\n" +
                    "where reservation.room_id_room is null and room_kolmest =" + kol + "\n"; ;
            var cmd = new MySqlCommand(stm, con);

            var version = cmd.ExecuteScalar().ToString();

            Console.WriteLine(version);
            return version;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

 

        private void btn_registr_client_Click(object sender, RoutedEventArgs e)
        {
            RegistrationClient reg = new RegistrationClient();
            reg.Show();
        }

        private void btn_zaselenie_Click(object sender, RoutedEventArgs e)
        {
            Find_client_forbron zacelenie = new Find_client_forbron();
            zacelenie.Show();
        }

        private void btn_oplata_Click(object sender, RoutedEventArgs e)
        {
            FindClientForKvitancia oplata = new FindClientForKvitancia();
            oplata.Show();
        }

        private void btn_clients_Click(object sender, RoutedEventArgs e)
        {
            All_Client allcl = new All_Client();
            allcl.Show();
        }

        private void btn_now_Click(object sender, RoutedEventArgs e)
        {
            ClientNow clnow=new ClientNow();
            clnow.Show();
        }

        private void btn_reservations_Click(object sender, RoutedEventArgs e)
        {
            KvitanciaAll kv_all=new KvitanciaAll();
            kv_all.Show();

        }

        private void btn_reservationsDate_Click(object sender, RoutedEventArgs e)
        {
            KvitanciaForDate kv_date = new KvitanciaForDate();
            kv_date.Show();
        }

        private void btn_update_freeroomsforcategory_Click(object sender, RoutedEventArgs e)
        {
            place1_1category.Content = getCountRoomFreeNowFilter(1, 1);
            place2_1category.Content = getCountRoomFreeNowFilter(1, 2);
            place3_1category.Content = getCountRoomFreeNowFilter(1, 3);
            place4_1category.Content = getCountRoomFreeNowFilter(1, 4);
            place1_2category.Content = getCountRoomFreeNowFilter(2, 1);
            place2_2category.Content = getCountRoomFreeNowFilter(2, 2);
            place3_2category.Content = getCountRoomFreeNowFilter(2, 3);
            place4_2category.Content = getCountRoomFreeNowFilter(2, 4);
            place1_3category.Content = getCountRoomFreeNowFilter(3, 1);
            place2_3category.Content = getCountRoomFreeNowFilter(3, 2);
            place3_3category.Content = getCountRoomFreeNowFilter(3, 3);
            place4_3category.Content = getCountRoomFreeNowFilter(3, 4);
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            this.Close();
            log.Show();
        }
    }
}