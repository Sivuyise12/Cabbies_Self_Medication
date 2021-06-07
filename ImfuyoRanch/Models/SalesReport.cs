using Shopping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models
{
    public class SalesReport
    {
        [Key]
        public int SalesReportId { get; set; }
        public int ItemCode { get; set; }
        public virtual Item Item { get; set; }
        [DisplayName("Number Of Orders")]
        public int NumberOfOrders { get; set; }
    }
}