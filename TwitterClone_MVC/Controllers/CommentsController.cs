using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TwitterClone_MVC.Data;
using TwitterClone_MVC.Models;

namespace TwitterClone_MVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly TwitterContext _context;

        public CommentsController(TwitterContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var twitterContext = _context.Comment.Include(c => c.Tweet).Include(c => c.User);
            return View(await twitterContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Tweet)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.TweetID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["TweetID"] = new SelectList(_context.Tweets, "ID", "Message");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TweetID,UserID,Message")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TweetID"] = new SelectList(_context.Tweets, "ID", "Message", comment.TweetID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", comment.UserID);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["TweetID"] = new SelectList(_context.Tweets, "ID", "Message", comment.TweetID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", comment.UserID);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TweetID,UserID,Message")] Comment comment)
        {
            if (id != comment.TweetID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.TweetID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TweetID"] = new SelectList(_context.Tweets, "ID", "Message", comment.TweetID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", comment.UserID);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Tweet)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.TweetID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.TweetID == id);
        }
    }
}
