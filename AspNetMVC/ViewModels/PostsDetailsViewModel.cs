using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AspNetMVC.Models;

namespace AspNetMVC.ViewModels
{
    public class PostsDetailsViewModel
    {
        public Post post { get; set; }
        public Comment comment { get; set; }
    }
}