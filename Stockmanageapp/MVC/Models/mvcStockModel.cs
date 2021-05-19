using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcStockModel
    {
        public int Stock_Id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Stock_Name { get; set; }
        public string Stock_type { get; set; }
        public int Stock_qty { get; set; }
        public int Stock_preorderlevel { get; set; }
    }
}