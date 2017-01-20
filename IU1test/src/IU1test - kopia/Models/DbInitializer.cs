using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IU1test.Data;
using IU1test.Models;

namespace IU1test.Models
{
    public static class DbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category{Name="Love"},
            new Category{Name="Hate" }
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var posts = new Post[]
            {
            new Post{Titel="Carson",CategoryID=1,DateCreated=DateTime.Parse("2005-09-01"),DateModified=DateTime.Parse("2005-09-01"),Details="My first post"},
            new Post{Titel="Segundo",CategoryID=1,DateCreated=DateTime.Parse("2005-09-01"),DateModified=DateTime.Parse("2005-09-01"),Details="My first post"},
            new Post{Titel="Tercero",CategoryID=2,DateCreated=DateTime.Parse("2005-09-01"),DateModified=DateTime.Parse("2005-09-01"),Details="My first post"},

            };
            foreach (Post p in posts)
            {
                context.Posts.Add(p);
            }
            context.SaveChanges();

            
        }
    }
}
