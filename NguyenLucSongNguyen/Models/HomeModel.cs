using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NguyenLucSongNguyen.Context;
namespace NguyenLucSongNguyen.Models
{
    public class HomeModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}