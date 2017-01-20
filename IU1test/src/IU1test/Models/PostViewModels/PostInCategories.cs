using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using IU1test.Models.PostViewModels;

namespace IU1test.Models.PostViewModels
{
    public class PostInCategories
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public int PostCount { get; set; }
        
    }
}
