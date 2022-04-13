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
    public class FollowsController : Controller
    {
        private readonly TwitterContext _context;

        public FollowsController(TwitterContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Create([Bind("UserID, FollowingID")] Follow follow)
        {
            bool flag = _context.Follow.Any(e => e.FollowingID == follow.FollowingID && e.UserID == follow.UserID);

            if (flag == false)
            {

                if (ModelState.IsValid)
                {
                    _context.Add(follow);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Users");
                }

                return RedirectToAction("Index", "Users");

            }
            else {


                return RedirectToAction("Delete", new { UserID = follow.UserID, FollowingID = follow.FollowingID});

            }

            
        }

        public async Task<IActionResult> Delete(int UserID, int FollowingID)
        {

            var follow = _context.Follow
                .Include(f => f.Following)
                .FirstOrDefault<Follow>(m => m.UserID == UserID && m.FollowingID == FollowingID);


            if (follow == null)
            {
                return NotFound();
            }

            _context.Follow.Remove(follow);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Users");
        }


        private bool FollowExists(int id)
        {
            return _context.Follow.Any(e => e.FollowingID == id);
        }
    }
}
