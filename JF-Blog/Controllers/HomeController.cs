using JF_Blog.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JF_Blog.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public IQueryable<BlogPost> IndexSearch(string searchStr)
        {
            IQueryable<BlogPost> result = null;
            if (searchStr != null)
            {
                result = db.BlogPosts.AsQueryable();
                result = result.Where(p => p.Title.Contains(searchStr) ||
                                      p.BlogPostBody.Contains(searchStr) ||
                                      p.Comments.Any(c => c.CommentBody.Contains(searchStr)
                                                          || c.Author.FirstName.Contains(searchStr)
                                                          || c.Author.LastName.Contains(searchStr)
                                                          || c.Author.DisplayName.Contains(searchStr)
                                                          || c.Author.Email.Contains(searchStr)));
            }
            else
            {
                result = db.BlogPosts.AsQueryable();
            }
            return result.OrderByDescending(p => p.Created);
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? page, string searchStr)
        {
            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr);

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //var listPosts = db.BlogPosts.AsQueryable();
            return View(blogList.OrderByDescending(p => p.Created).ToPagedList(pageNumber, pageSize));            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            EmailModel model = new EmailModel();

            return View(model);
        }
   

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Contact(EmailModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var body = "<p>Email From: <bold>{0}</bold>({1})</p><p>Message:</p><p>{2}</p>";
                var from = "MyPortfolio<example@email.com>";
                model.Body = "This is a message from your portfolio site. The name and the email if the contacting person is above.";
                var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                {
                    Subject = "Portfolio Contact Email",
                    Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                    IsBodyHtml = true
                };

                var svc = new PersonalEmail();
                await svc.SendAsync(email);

                return View(new EmailModel());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.FromResult(0);
            }
        }
            return View(model);
    }

 }
}