using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace mhms3.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string CounselorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentificationNum { get; set; }
    }
}
