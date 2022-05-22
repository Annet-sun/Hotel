using System;
using System.Collections.Generic;

namespace hotel.DataAccess.DataObjects
{
    public partial class Category
    {
        public Category()
        {
            Room = new HashSet<Room>();
        }

        public int IdCategory { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
