using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models.ViewModel
{
    public class CommentViewModel
    {
        public int R_Id { get; set; }
        public int product_id { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
        public int Customer_ID { get; set; }
        public string ImageFilePath { get; set; }
    }
}