using System;
using System.Collections.Generic;

namespace hotel.DataAccess.DataObjects
{
    public partial class Administrator
    {
        public Administrator()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int IdAdministrator { get; set; }
        public string AdministratorFam { get; set; }
        public string AdministratorIo { get; set; }
        public string AdministratorPassword { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
