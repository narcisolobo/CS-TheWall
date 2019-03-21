using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheWall.Models;

namespace TheWall.Controllers {
    public class HomeController : Controller {
        private TheWallContext dbContext;

        public HomeController (TheWallContext context) {
            dbContext = context;
        }

        [HttpGet]
        [Route ("")]
        public IActionResult Index () {
            return View ();
        }

        [HttpPost]
        [Route ("register")]
        public IActionResult Register (User newUser) {
            if (ModelState.IsValid) {
                if (dbContext.Users.Any (u => u.Email == newUser.Email)) {
                    ModelState.AddModelError ("Email",
                        "Email already in use. Please log in.");
                    return View ("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User> ();
                newUser.Password = Hasher.HashPassword (newUser, newUser.Password);
                dbContext.Users.Add (newUser);
                dbContext.SaveChanges ();
                User loggedUser = dbContext.Users.FirstOrDefault ((u => u.Email == newUser.Email));
                HttpContext.Session.SetInt32 ("logged", loggedUser.UserId);
                return RedirectToAction ("Home");
            } else {
                return View ("Index");
            }
        }

        [HttpPost]
        [Route ("login")]
        public IActionResult Login (LoginUser userSubmission) {
            if (ModelState.IsValid) {
                var userInDb = dbContext.Users.FirstOrDefault (u => u.Email == userSubmission.LoginEmail);
                if (userInDb == null) {
                    ModelState.AddModelError ("Email", "Invalid Email");
                    return View ("Index");
                }
                var hasher = new PasswordHasher<LoginUser> ();
                var result = hasher.VerifyHashedPassword (userSubmission, userInDb.Password, userSubmission.LoginPassword);
                if (result == 0) {
                    ModelState.AddModelError ("Password", "Invalid Password");
                    return View ("Index");
                }
                HttpContext.Session.SetInt32 ("logged", userInDb.UserId);
                return RedirectToAction ("Home");
            } else {
                return View ("Index");
            }
        }

        [HttpGet]
        [Route ("home")]
        public IActionResult Home () {
            if (HttpContext.Session.GetInt32 ("logged") == null) {
                return View ("Index");
            }
            BagTheUser ();
            BagTheMessages();
            BagTheComments();
            return View ("Home");
        }

        [HttpGet]
        [Route ("logout")]
        public IActionResult Logout () {
            HttpContext.Session.Clear ();
            return View ("Index");
        }

        [HttpPost]
        [Route ("postmessage")]
        public IActionResult PostMessage (Message postedMessage) {
            if (ModelState.IsValid) {
                User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("logged"));
                postedMessage.UserId = loggedUser.UserId;
                postedMessage.User = loggedUser;
                dbContext.Messages.Add (postedMessage);
                dbContext.SaveChanges ();
                return RedirectToAction ("Home");
            }
            BagTheUser ();
            BagTheMessages();
            BagTheComments();
            return View ("Home");
        }

        [HttpPost]
        [Route ("postcomment/{MessageId}")]
        public IActionResult PostComment (Comment postedComment, int messageID) {
            if (ModelState.IsValid) {
                User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("logged"));
                Message retrievedMessage = dbContext.Messages.FirstOrDefault(m => m.MessageId == messageID);
                postedComment.UserId = loggedUser.UserId;
                postedComment.User = loggedUser;
                postedComment.MessageId = retrievedMessage.MessageId;
                postedComment.Message = retrievedMessage;
                dbContext.Comments.Add (postedComment);
                dbContext.SaveChanges ();
                return RedirectToAction ("Home");
            }
            BagTheUser ();
            BagTheMessages();
            BagTheComments();
            return View ("Home");
        }

        public void BagTheUser () {
            User loggedUser = dbContext.Users.FirstOrDefault (u => u.UserId == HttpContext.Session.GetInt32 ("logged"));
            ViewBag.LoggedUser = loggedUser;
            ViewBag.LoggedUserName = $"{loggedUser.FirstName} {loggedUser.LastName}";
        }

        public void BagTheMessages() {
            List<Message> allMessages = dbContext.Messages
                .Include(m => m.User)
                .OrderBy(m => m.CreatedAt)
                .ToList();
            allMessages.Reverse();
            ViewBag.allMessages = allMessages;
        }

        public void BagTheComments() {
            List<Comment> allComments = dbContext.Comments
                .Include(c => c.User)
                .Include(m => m.Message)
                .ToList();
            ViewBag.AllComments = allComments;
        }
    }
}