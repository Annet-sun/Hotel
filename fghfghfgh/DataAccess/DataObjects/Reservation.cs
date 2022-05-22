using System;
using System.Collections.Generic;

namespace hotel.DataAccess.DataObjects
{
    public partial class Reservation
    {
        public int IdReservation { get; set; }
        public DateTime? ReservationDateIn { get; set; }
        public DateTime? ReservationDateOut { get; set; }
        public int AdministratorIdAdministrator { get; set; }
        public int ClientIdClient { get; set; }
        public int RoomIdRoom { get; set; }
        public Reservation(int v1, DateTime v2, DateTime v3, int v4, int v5, int v6)
        {
            this.IdReservation = v1;
            this.ReservationDateIn = v2;
            this.ReservationDateOut = v3;
            this.AdministratorIdAdministrator = v4;
            this.ClientIdClient = v5;
            this.RoomIdRoom = v6;
        }
        public Reservation( )
        {
        }
        public virtual Administrator AdministratorIdAdministratorNavigation { get; set; }
        public virtual Client ClientIdClientNavigation { get; set; }
        public virtual Room RoomIdRoomNavigation { get; set; }
    }
}
