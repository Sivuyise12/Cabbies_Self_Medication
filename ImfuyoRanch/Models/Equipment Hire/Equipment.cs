using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models.Equipment_Hire
{
    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }
        public int EquipmentTypeId { get; set; }
        public virtual EquipmentType EquipmentType { get; set; }
        [DisplayName("Equipment Name"),Required]
        public string EquipmentName { get; set; }
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        [DisplayName("Hire Price"),Required,DataType(DataType.Currency)]
        public decimal HirePrice { get; set; }
        public string Status { get; set; }
    }
}