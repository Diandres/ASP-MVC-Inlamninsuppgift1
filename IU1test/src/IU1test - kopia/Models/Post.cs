using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IU1test.Models
{
    public class Post
    {
        public int PostID { get; set; }        

        [Required]
        [StringLength(50)]
        public string Titel { get; set; }

        [Required]
        [StringLength(2000)]
        public string Details { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }


    }
}
