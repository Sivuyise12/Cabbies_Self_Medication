using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models.Equipment_Hire
{
    public class EquipmentType
    {
        [Key]
        public int EquipmentTypeId { get; set; }
        [DisplayName("Equipment Type Name"),Required]
        public string EquipementTypeName { get; set; }
    }
}