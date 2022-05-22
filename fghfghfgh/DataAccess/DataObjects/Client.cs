using System;
using System.Collections.Generic;

namespace hotel.DataAccess.DataObjects
{
    public partial class Client
    {
        public Client()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int IdClient { get; set; }
        public string ClientFam { get; set; }
        public string ClientName { get; set; }
        public string ClientOtch { get; set; }
        public string ClientPassport { get; set; }
        public string ClientTelephone { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }

        public Client(int IdClient, string ClientFam, string ClientName, string ClientOtch, string ClientPassport, string ClientTelephone)
        {
            this.IdClient = IdClient;
            this.ClientFam = ClientFam;
            this.ClientName = ClientName;
            this.ClientOtch = ClientOtch;
            this.ClientPassport = ClientPassport;
            this.ClientTelephone = ClientTelephone;
        }
    }
}
