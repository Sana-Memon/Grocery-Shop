using GroceryStore.Models;
using System;
using System.Collections.Generic;

namespace GroceryStore.Controllers.Dto
{
    public class ProductDto
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int category_id { get; set; }
        public string ImageFilePath { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string FullDescription { get; set; }
        public Nullable<int> StockAmount { get; set; }
        public Nullable<int> SkuId { get; set; }
        public string MartLocation { get; set; }
        public string ImageFilePath2 { get; set; }
        public int customerId { get; set; }
    }
}