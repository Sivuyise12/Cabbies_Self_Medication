using Shopping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models
{
    public class AssignEmployee
    {
        [Key]
        public int AssignEmployeeId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string Order_ID { get; set; }
        public virtual Order Order { get; set; }
        [DisplayName("Date Assigned"),DataType(DataType.Date)]
        public DateTime DateAssigned { get; set; }
        public string Status { get; set; }
    }
}