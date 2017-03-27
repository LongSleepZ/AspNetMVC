using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;

namespace AspNetMVC.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //我的文章 
        public ActionResult My()
        {
            ViewBag.PostType = "我的文章";
            var userId = GetCurrentUserId();
            var post = db.Posts.Where(x => x.user_id == userId).OrderByDescending(x=>x.created_at);

            return View("Index", post.ToList());
        }
        //使用者的文章
        public ActionResult _User(string id)
        {
            var user = db.Users.Where(x => x.Id == id).First();
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.PostType =user.UserName + "的文章";

            var post = db.Posts.Where(x => x.user_id == id).OrderByDescending(x => x.created_at); ;

            return View("Index", post.ToList());
        }
        //熱門文章
        public ActionResult Hot() {
            ViewBag.PostType = "熱門文章";
            var posts = db.Posts
                        .Where(x => x.page_view >= 50)
                        .OrderByDescending(x => x.page_view )
                        .OrderByDescending(x => x.created_at);
            return View("Index", posts.ToList());
        }
        //隨機文章
        public ActionResult Random()
        {
            ViewBag.PostType = "隨機文章";
            var posts = db.Posts.OrderBy(x => Guid.NewGuid()).Take(5);

            return View("Index", posts.ToList());
        }
        //留言
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(int id,[Bind(Include = "name,email,content")] Comment comment)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                comment.post_id = id;
                comment.created_at = DateTime.Now;
                comment.updated_at = DateTime.Now;
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details",new { id=comment.post_id});
            }

            PostsDetailsViewModel viewModel = new PostsDetailsViewModel();
            viewModel.post = post;
            viewModel.comment =  comment;
            return View("Details", viewModel);
        }

        // GET: Posts
        public ActionResult Index(int page=1)
        {
            ViewBag.PostType = "文章總覽";
            var posts = db.Posts.OrderByDescending(x => x.created_at);
            return View(posts.ToPagedList(page, 5));
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            PostsDetailsViewModel viewModel=new PostsDetailsViewModel();
            viewModel.post = post;
            viewModel.comment = new Comment();
            return View(viewModel);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,sub_title,content,is_feature")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.page_view = 0;
                post.created_at = DateTime.Now;
                post.updated_at = DateTime.Now;
                post.user_id = GetCurrentUserId();
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,sub_title,content,is_feature,page_view")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                post.updated_at = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public String GetCurrentUserId() {
            return new Guid(User.Identity.GetUserId()).ToString();
        }

        private bool EnsureIsUserPost(Post post) {
            return (post.user_id == GetCurrentUserId());

        }
    }
}
