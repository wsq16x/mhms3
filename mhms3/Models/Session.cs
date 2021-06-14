using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mhms3.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string CounselorID { get; set; }
        public int AppointmentID { get; set; }

        [ForeignKey("AppointmentID")]
        public Appointment Appointment { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime TimeStart { get; set; }
        [DataType(DataType.Time)]
        public DateTime TimeEnd { get; set; }

        public string Expressions { get; set; }
        public string Notes { get; set; }
    }
}
