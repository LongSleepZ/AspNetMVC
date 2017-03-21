using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMVC.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "姓名")]
        [MaxLength(256)]
        public string name { get; set; }

        [Display(Name = "電子郵件")]
        [EmailAddress]
        public string email { get; set; }

        [Display(Name = "內文")]
        public string content { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? created_at { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? updated_at { get; set; }

        [ForeignKey("Posts")]
        public int post_id { get; set; }

        
        public virtual  Post Posts { get; set; }
    }

}