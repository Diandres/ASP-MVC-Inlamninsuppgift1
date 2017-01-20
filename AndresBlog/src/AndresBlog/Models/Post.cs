using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AndresBlogg.Models
{
    public class Post
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }


        [StringLength(2000, MinimumLength = 10)]
        public string Description { get; set; }

      
        public bool Published { get; set; }

        [Display(Name = "Posted On")]
        [DataType(DataType.Date)]
        public DateTime PostedOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Modified { get; set; }

        public int CategoryId { get; set; }


    }
}

