using Shopping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImfuyoRanch.Models
{
    public class ItemRating
    {
        [Key]
        public int ItemRatingID { get; set; }
        [DisplayName("Customer Email")]
        public string CustomerEmail { get; set; }
        public int ItemCode { get; set; }
        public virtual Item Item { get; set; }
        public string Comment { get; set; }
        [DisplayName("Item Rating")]
        public int Rating { get; set; }
    }
}