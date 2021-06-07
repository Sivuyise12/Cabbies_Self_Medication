using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models.Equipment_Hire
{
    public class EquipmentHire
    {
        [Key]
        public int EquipmentHireId { get; set; }
        [Key]
        public int EquipmentId { get; set; }
        [DisplayName("Customer Email")]
        public string CustomerEmail { get; set; }
        [DisplayName()]
        public DateTime DateHired { get; set; }
        public DateTime DateFor { get; set; }

    }
}