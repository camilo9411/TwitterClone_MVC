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
    public class LikesController : Controller
    {
        private readonly TwitterContext _context;

        public LikesController(TwitterContext context)
        {
            _context = context;
        }

        // GET: Likes
        public async Task<IActionResult> Index()
        {
            var twitterContext = _context.Likes.Include(l => l.Tweet).Include(l => l.User);
            return View(await twitterContext.ToListAsync());
        }

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var like = await _context.Likes
                .Include(l => l.Tweet)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.TweetID == id);
            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        // GET: Likes/Create
        public IActionResult Create()
        {
            ViewData["TweetID"] = new SelectList(_context.Tweets, "ID", "Message");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create([Bind("TweetID,UserID")] Like like)
        {
            var likeExtist = await _context.Likes
                .Include(l => l.Tweet)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.TweetID == like.TweetID && m.UserID == like.UserID);

            if (likeExtist == null)
            {

                if (ModelState.IsValid)
                {
                    _context.Add(like);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Tweets");
                }
                return RedirectToAction("Index","Tweets");

            }
            else { 
                
                return RedirectToAction("Delete", new { TweetId = like.TweetID, UserId = Int32.Parse(Request.Cookies["ID"]) }); 
            
            }

        }

        // GET: Likes/Delete/5
        public async Task<IActionResult> Delete(int TweetId, int UserId)
        {
            var like = await _context.Likes
                .Include(l => l.Tweet)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.TweetID == TweetId && m.UserID == UserId );

            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tweets");
        }


        private bool LikeExists(int id)
        {
            return _context.Likes.Any(e => e.TweetID == id);
        }
    }
}