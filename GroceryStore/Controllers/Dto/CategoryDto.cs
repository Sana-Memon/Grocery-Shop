using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class CategoryDto
    {
        public int category_id { get; set; }
        public Nullable<int> position_id { get; set; }
        public string category1 { get; set; }
        public string ImageFilePath { get; set; }
        public int customerId { get; set; }

    }
}