using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Models
{
    public class Post
    {
        public int id { get; set; } 
        public string title { get; set; }     
        public string sub_title { get; set; }
        public string content { get; set; }
        public bool  is_feature { get; set; }
        public int page_view { get; set; }
        public Guid   user_id { get; set; }


    }
}