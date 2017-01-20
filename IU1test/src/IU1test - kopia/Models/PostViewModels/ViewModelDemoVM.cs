using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IU1test.Models.PostViewModels
{
    public class ViewModelDemoVM
    {
        public List<Post> allPosts { get; set; }
        public List<PostInCategories> allCategories { get; set; }
    }
}
