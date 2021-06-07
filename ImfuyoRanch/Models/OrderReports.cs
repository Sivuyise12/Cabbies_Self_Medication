using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models
{
    public class OrderReports
    {
        [Key]
        public int OrderReportId { get; set; }
        [DisplayName("Order Id")]
        public string OrderId { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Order Amount"),DataType(DataType.Currency)]
        public double OrderAmount { get; set; }
        [DisplayName("Customer Email")]
        public string CustomerEmail { get; set; }

    }
}