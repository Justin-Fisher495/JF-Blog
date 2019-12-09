using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JF_Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string AuthorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        [Display(Name = "Comment")]
        public string CommentBody { get; set; }
        public string UpdateReason { get; set; }

        //Virtual Navigation section

        public virtual BlogPost BlogPost { get; set; }
        public virtual ApplicationUser Author { get; set; }




    }
}