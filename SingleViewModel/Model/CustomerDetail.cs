using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SingleViewModel
{
    public class CustomerDetail
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [SQLite.MaxLength(100)]
        //[Required]
        public string FirstName { get; set; }
        [SQLite.MaxLength(100)]
        // [Required]
        public string LastName { get; set; }
        [SQLite.MaxLength(13)]
        // [Required]
        public string IDNumber { get; set; }
        // [Required]       
        public int Age { get; set; }
        [MaxLength(255)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }
    }
}
