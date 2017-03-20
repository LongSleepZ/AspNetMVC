using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMVC.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        [Key]
        public int id { get; set; }

        [Display(Name = "標題")]
        [MaxLength(256)]
        public string title { get; set; }

        [Display(Name = "副標題")]
        [MaxLength(256)]
        public string sub_title { get; set; }

        [Display(Name = "內文")]
        public string content { get; set; }

        [Display(Name = "精華")]
        public bool is_feature { get; set; }

        [Display(Name = "觀看數")]
        public int page_view { get; set; }

        [Column(TypeName ="datetime2")]
        public DateTime? created_at { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? updated_at { get; set; }

        [ForeignKey("user")]
        public string user_id { get; set; }

        public ICollection<Comment> Comments { get; set; }
        
        public ApplicationUser user { get; set; }
    }
}