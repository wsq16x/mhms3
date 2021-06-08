using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mhms3.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int ClientID { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }
        public string CounselorID { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeStart { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeEnd { get; set; }

        public string SessionKey { get; set; }

        public string Status { get; set; }
    }
}
