using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IU1test.Data;
using IU1test.Models;
using Microsoft.EntityFrameworkCore;
using IU1test.Models.PostViewModels;
using System.Collections;

namespace IU1test.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogContext _context;

        public HomeController(BlogContext context)
        {
            _context = context;
        }

        // GET: Posts
        public IActionResult Index(string searchString)
        {
            ViewModelDemoVM vm = new ViewModelDemoVM();

            vm.allPosts = GetPosts();


            vm.allCategories = GetCategories();
            
            vm.allPosts.OrderBy(x => x.DateCreated);


            var posts = from p in _context.Posts
                        select p;

            if (!String.IsNullOrEmpty(searchString))
            {

                vm.allPosts.Find(s => s.Titel.Contains(searchString)
                                       || s.Details.Contains(searchString));
            }

            return View(vm);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,DateCreated,DateModified,Details,Titel")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostID,CategoryID,DateCreated,DateModified,Details,Titel")] Post post)
        {
            if (id != post.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.PostID == id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }

        public ActionResult CategoriesPostCount()
        {

            ViewModelDemoVM vm = new ViewModelDemoVM();
            vm.allCategories = GetCategories();
            vm.allPosts = GetPosts();
            vm.allPosts.OrderBy(x => x.DateCreated);

            return View(vm);


            //    IQueryable<PostInCategories> data =
            //        from post in _context.Posts
            //        group post by post.CategoryID into PostCateg
            //        select new PostInCategories()
            //        {
            //            CategoryName = (from category in _context.Categories where category.ID == PostCateg.Key select category.Name).First().ToString(),
            //            PostCount = PostCateg.Count()
            //        };
            //    var newdata = data.AsNoTracking().ToListAsync();

            //    var posts = from p in _context.Posts
            //                select p;

            //    if (!String.IsNullOrEmpty(searchString))
            //    {

            //        posts = posts.Where(s => s.Titel.Contains(searchString)
            //                               || s.Details.Contains(searchString));
            //    }
            //    else
            //    {
            //        posts = _context.Posts.Include(p => p.Category);

            //    }

            //    var newposts = from p in _context.Posts
            //                   select p;

            //    return View(Tuple.Create(posts, newdata));
        }

        public List <Post>GetPosts()
        {
            List <Post> newposts = (from p in _context.Posts select p).ToList();
            return newposts;
        }

        public List<PostInCategories> GetCategories()
        {
            IQueryable<PostInCategories> data =
                from post in _context.Posts
                group post by post.CategoryID into PostCateg
                select new PostInCategories()
                {
                    ID = PostCateg.Key,
                    CategoryName = (from category in _context.Categories where category.ID == PostCateg.Key select category.Name).First().ToString(),
                    PostCount = PostCateg.Count()
                };
            var newdata = data.AsNoTracking().ToList();
            return newdata;
        }

    }
}
