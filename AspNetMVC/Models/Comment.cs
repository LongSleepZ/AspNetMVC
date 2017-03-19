using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string content { get; set; }
        public int post_id { get; set; }

    }

}