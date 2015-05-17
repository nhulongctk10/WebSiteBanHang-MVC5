using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LongKhanhMobile.Areas.Admin.Models
{
    public class ProductEditViewModel:ProductCreateViewModel
    {
        public int ProductId { get; set; }
        public string ThumbImage { get; set; }
        public byte[] RowVersion { get; set; }
    }
}