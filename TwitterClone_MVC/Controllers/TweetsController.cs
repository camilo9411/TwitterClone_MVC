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
    public class TweetsController : Controller
    {
        private readonly TwitterContext _context;

        public TweetsController(TwitterContext context)
        {
            _context = context;
        }

        // GET: Tweets
        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["Auth"] == null)
            {

                return RedirectToAction("Index", "Home");

            }

            List<Tweet> myFeed = new List<Tweet>();
            List<int> followingIDs = new List<int>();

            var user = await _context.Users
                .Include(t=> t.Tweets)
                .Include(t => t.Follows)
                .FirstOrDefaultAsync(m => m.ID == Int32.Parse(Request.Cookies["ID"]));

            var followingIDss = _context.Follow
                .Where<Follow>(m => m.UserID == user.ID);

            followingIDs.Add(user.ID);

            foreach (Follow f in followingIDss)
            {
                followingIDs.Add(f.FollowingID);

            }

            var followingTweets =  _context.Tweets
                .Include(t => t.User)
                .Include(t => t.Likes)
                .Include(t => t.Comments)
                .Where<Tweet>(m => followingIDs.Contains(m.UserID));


            myFeed = followingTweets.ToList<Tweet>();

            List<Tweet> SortedList = myFeed.OrderByDescending(t => t.CreatedOn).ToList();

            return View(SortedList);
        }

        // GET: Tweets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweets
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // GET: Tweets/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: Tweets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Message")] Tweet tweet)
        {
            tweet.UserID = Int32.Parse(Request.Cookies["ID"]);
            tweet.CreatedOn = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(tweet);
        }

        // GET: Tweets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweets.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", tweet.UserID);
            return View(tweet);
        }

        // POST: Tweets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,Message,CreatedOn")] Tweet tweet)
        {
            if (id != tweet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tweet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TweetExists(tweet.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", tweet.UserID);
            return View(tweet);
        }

        // GET: Tweets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweets
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // POST: Tweets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tweet = await _context.Tweets.FindAsync(id);
            _context.Tweets.Remove(tweet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TweetExists(int id)
        {
            return _context.Tweets.Any(e => e.ID == id);
        }
    }
}
