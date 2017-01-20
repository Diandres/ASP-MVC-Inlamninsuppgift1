using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IU1test.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace IU1test.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Post && (x.State == EntityState.Added || x.State == EntityState.Modified));

            

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Post)entity.Entity).DateCreated = DateTime.UtcNow;
                }

                ((Post)entity.Entity).DateModified = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
