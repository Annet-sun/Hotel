using System;
using System.Collections.Generic;

namespace hotel.DataAccess.DataObjects
{
    public partial class Room
    {
        public Room()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int IdRoom { get; set; }
        public int? RoomKolmest { get; set; }
        public decimal? Price { get; set; }
        public int CategoryIdCategory { get; set; }

        public virtual Category CategoryIdCategoryNavigation { get; set; }
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
