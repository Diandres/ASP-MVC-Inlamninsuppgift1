using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IU1test.Data;
using IU1test.Models;

namespace IU1test.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogContext _context;

        public PostsController(BlogContext context)
        {
            _context = context;    
        }

        // GET: Posts
        //public async Task<IActionResult> Index()
        //{
        //    var blogContext = _context.Posts.Include(p => p.Category);
        //    return View(await blogContext.OrderByDescending(d=> d.DateCreated).ToListAsync());


        //}

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitelSortParm"] = sortOrder == "Name"? "name_desc" : "Name";
            ViewData["DateCreatedSortParm"] = sortOrder == "DateC" ? "dateC_desc" : "DateC";
            ViewData["DateEditedSortParm"] = sortOrder == "DateE" ? "dateE_desc" : "DateE";
            ViewData["CurrentFilter"] = searchString;

            //var posts = from p in _context.Posts
            //               select p;

            var posts = _context.Posts
        .Include(c => c.Category)
        .AsNoTracking();
            


            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Titel.Contains(searchString)
                                       || s.Details.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    posts = posts.OrderBy(s => s.Titel);
                    break;
                case "name_desc":
                    posts = posts.OrderByDescending(s => s.Titel);
                    break;
                case "DateC":
                    posts = posts.OrderBy(s => s.DateCreated);
                    break;
                case "dateC_desc":
                    posts = posts.OrderByDescending(s => s.DateCreated);
                    break;
                case "DateE":
                    posts = posts.OrderBy(s => s.DateCreated);
                    break;
                case "dateE_desc":
                    posts = posts.OrderByDescending(s => s.DateCreated);
                    break;
               
            }
            return View(await posts.AsNoTracking().ToListAsync());
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
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
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
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
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
    }
}
