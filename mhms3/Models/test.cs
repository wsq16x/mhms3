using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mhms3.Models
{
    public class test
    {
        [Key]
        public int testId { get; set; }
        public int ClientID { get; set; }

        [ForeignKey("ClientID")]
        public Client Client { get; set; }
    }
}
