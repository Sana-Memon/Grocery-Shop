using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class ResertPasswordModel
    {
        [Required(ErrorMessage ="New Password is required field", AllowEmptyStrings =false)]
        [DataType(DataType.Password)]
        public string NewPassword {  get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage =" Both password Doesn't match")]
        public string ConfirmPassword   {  get; set;  }

        [Required]
        public string ResetCode   {   get; set;   }
    }
}