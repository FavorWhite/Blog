using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Blog.BLL.Interfaces;
using Blog.BLL.Services;
using Blog.View.Models;
using Blog.BLL.DTO;
using System.Threading;

namespace Blog.View.Controllers
{
    public class BlogController : Controller
    {
        AppService appService = new AppService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddArticle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(ArticleCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                if (!appService.GetCategories().Any(categ => categ.Title == model.CategoryName))
                {
                    appService.AddCategory(userName, model.CategoryName);
                }
                else
                {
                    appService.UpdateCategory(userName, model.CategoryName);
                }

                List<string> tagTitles = appService.ParseTagString(model.Tags);

                foreach (var item in tagTitles)
                {
                    if (!appService.GetTags().Any(tag => tag.Title ==item))
                    {
                        appService.AddTag(userName, item);
                    }
                }
                List<TagDTO> tagsForArticle = new List<TagDTO>();
                foreach (var item in tagTitles)
                {
                    tagsForArticle.Add(appService.GetTags().First(t => t.Title == item));
                }
                    
                ArticleDTO articleDto = new ArticleDTO
                {
                    Title = model.Title,
                    ShortDescription = model.ShortDescription,
                    ArticleText = model.ArticleText,
                    ArticleCreationTime = DateTime.Now,
                    ApplicationUserId = appService.GetUserIdByName(User.Identity.Name),
                    CategoryId= appService.GetCategories().First(categ=> categ.Title==model.CategoryName).Id,
                    ArticleTags = tagsForArticle
                };
                appService.AddArticle(articleDto);
           
                foreach (var item in tagTitles)
                {
                    if (appService.GetTags().Any(tag => tag.Title == item))
                    {
                        int artId = appService.GetArticles().First(art => art.Title == model.Title).Id;
                        appService.UpdateTag(userName, item, artId); 
                    }
                }
                return View("Index");
            }
            
            return View(model);
        }
        public ActionResult GetUserArticles()
        {
          List<ArticleModel> articles = GetArticlesModels().FindAll(art => art.ApplicationUserId == appService.GetUserIdByName(User.Identity.Name));
            return PartialView("_ArticleList", articles);
        }
        public ActionResult GetArticlesByCategory(int id)
        {
            IEnumerable<ArticleModel> articles = GetArticlesModels().FindAll(art => art.CategoryId == id && art.ApplicationUserId == appService.GetUserIdByName(User.Identity.Name));
            return PartialView("_ArticleList", articles);
        }
        public ActionResult GetArticlesByTag(int id)
        {
            IEnumerable<ArticleModel> articles = GetArticlesModels().FindAll(art=>art.ApplicationUserId == appService.GetUserIdByName(User.Identity.Name));
            List<ArticleModel> artModel = new List<ArticleModel>();
            foreach (var art in articles)
            {
                foreach (var tag in art.TagsIds)
                {
                    if (tag==id)
                    {
                        artModel.Add(art);
                    }
                }
            }
            return PartialView("_ArticleList", artModel);
        }
        public ActionResult GetArticle(int id)
        {
            ArticleModel article = GetArticlesModels().Find(art => art.Id == id);
            return PartialView("_ArticleView", article);
        }
        public ActionResult GetUserCategories()
        {
            string userID = appService.GetUserIdByName(User.Identity.Name);
            List<CategoryModel> userCategories = new List<CategoryModel>();
            
            foreach (var item in GetCategoriesModels())
            {
                foreach (var user in item.ApplicationUserIds)
                {
                    if (user== userID)
                    {
                        userCategories.Add(item);
                    }
                }
            }
            return PartialView("_CategoryList", userCategories);
        }
        public ActionResult GetUserTags()
        {
            string userID = appService.GetUserIdByName(User.Identity.Name);
            List<TagModel> userTags = new List<TagModel>();
            foreach (var tag in GetTagsModels())
            {
                foreach (var user in tag.ApplicationUserIds)
                    {
                        if (user == userID)
                        {
                            userTags.Add(tag);
                        }
                    }
                }
            return PartialView("_TagList", userTags);
        }
        [HttpPost]
        public ActionResult ArticleSearch(string title)
        {
            List<ArticleModel> articles = GetArticlesModels().Where(art=>art.Title.ToLower().Contains(title.ToLower())).Where(art => art.ApplicationUserId == appService.GetUserIdByName(User.Identity.Name)).ToList();
            if (articles.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView("_ArticleList", articles);
        }
        public ActionResult AutoCompleteUserArticleSearch(string term)
        {
            var articles = appService.GetArticles().Where(art => art.ApplicationUserId == appService.GetUserIdByName(User.Identity.Name)).OrderBy(c => c.Title).Where(c => c.Title.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a.Title })
                            .Distinct();

            return Json(articles, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoCompleteCategory(string term)
        {
            var categories = appService.GetCategories().OrderBy(c=>c.Title).Where(c => c.Title.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a.Title })
                            .Distinct();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        private List<CategoryModel> GetCategoriesModels()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            foreach (var item in appService.GetCategories())
            {
                List<string> catIdList = new List<string>();
                foreach (var user in item.Users)
                {
                    catIdList.Add(user.Id);
                }
                categories.Add(new CategoryModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    ApplicationUserIds=catIdList
                });

            }
            return categories;
        }
        private List<TagModel> GetTagsModels()
        {
            List<TagModel> tags = new List<TagModel>();

            foreach (var item in appService.GetTags())
            {
                List<string> userIdList = new List<string>();
                List<int> artIdList = new List<int>();
                foreach (var user in item.Users)
                {
                    userIdList.Add(user.Id);
                }
                foreach (var art in item.TagArticles)
                {
                    artIdList.Add(art.Id);
                }
                tags.Add(new TagModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    ApplicationUserIds = userIdList,
                    ArticleIds= artIdList
                });
            }
            return tags;
        }
        private List<ArticleModel> GetArticlesModels()
        {
            List<ArticleModel> articles = new List<ArticleModel>();

            foreach (var item in appService.GetArticles().OrderBy(art => art.ArticleCreationTime))
            {
                List<int> tagIdList = new List<int>();
                foreach (var tag in item.ArticleTags)
                {
                    tagIdList.Add(tag.Id);
                }
                articles.Add(new ArticleModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    ArticleText = item.ArticleText,
                    ArticleCreationTime = item.ArticleCreationTime,
                    CategoryId = item.CategoryId,
                    ApplicationUserId=item.ApplicationUserId,
                    TagsIds= tagIdList,
                    UserName = appService.GetUserNameById(item.ApplicationUserId),//
                    CategoryName = appService.GetCategories().First(c => c.Id == item.CategoryId).Title//
                });
            }
            return articles;
        }
        public ActionResult EditArticle(int id)
        {
            ArticleModel article = GetArticlesModels().First(art=>art.Id==id);

            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }
        [HttpPost]
        public ActionResult EditArticle(ArticleModel article)
        {
            if (ModelState.IsValid)
            {
                ArticleDTO articleDto = new ArticleDTO
                {
                    Id=article.Id,
                    Title = article.Title,
                    ShortDescription = article.ShortDescription,
                    ArticleText = article.ArticleText,
                    ArticleCreationTime = DateTime.Now,
                    ApplicationUserId = article.ApplicationUserId,
                    CategoryId = article.CategoryId,   
                };
                appService.UpdateArticle(articleDto);
                return RedirectToAction("Index");
            }

            return View(article);
        }
        public ActionResult DeleteArticle(int id)
        {
            int categoryId = appService.GetArticles().First(art => art.Id == id).CategoryId;
            ICollection<TagDTO> Tags = appService.GetArticles().First(art => art.Id == id).ArticleTags;
            string userid =appService.GetUserIdByName( User.Identity.Name);

            appService.DeleteArticle(id);

            if (appService.IsCategoryEmpty(categoryId))
            {
                appService.DeleteCategory(categoryId);
            }
            else
            {
                CategoryDTO category = appService.GetCategories().First(tagart => tagart.Id == categoryId);
                if (category.Users.Any(cat => cat.Id == userid))
                {
                    appService.DeleteCategoryRefToUser(category.Id, userid);
                }
            }
            
            foreach (var item in Tags)
            {
                if (appService.IsTagEmpty(item.Id))
                {
                    appService.DeleteTag(item.Id);
                    continue;
                }
                if (!item.TagArticles.Any(tagart=>tagart.ApplicationUserId== userid))
                {
                    appService.DeleteTagRefToUser(item.Id, userid);
                }
            }
            return RedirectToAction("Index");
        }
    }
}