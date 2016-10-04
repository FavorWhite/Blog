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
using AutoMapper;
using Blog.BLL.DTO;

namespace Blog.View.Controllers
{
    public class HomeController : Controller
    {
        AppService appService = new AppService();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Contact()
        {
            return View();
        }
         public ActionResult GetArticles()
        {
            List<ArticleModel> articles = GetArticlesModels();
            return PartialView("_ArticleList", articles);
        }
        public ActionResult GetArticlesByCategory(int id)
        {
            IEnumerable<ArticleModel> articles = GetArticlesModels().FindAll(art => art.CategoryId == id);
            return PartialView("_ArticleList", articles);
        }
        public ActionResult GetArticlesByTag(int id)
        {
            IEnumerable<ArticleModel> articles = GetArticlesModels();
            List<ArticleModel> artModel = new List<ArticleModel>();
            foreach (var art in articles)
            {
                foreach (var tag in art.TagsIds)
                {
                    if (tag == id)
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
        public ActionResult GetCategories()
        {
            List<CategoryModel> categories = GetCategoriesModels();
            return PartialView("_CategoryList", categories);
        }
        public ActionResult GetTags()
        {
            List<TagModel> userTags = GetTagsModels();
            return PartialView("_TagList", userTags);
        }
        [HttpPost]
        public ActionResult AddComment(AddCommentModel model)
        {
            IEnumerable<CommentModel> commentView = new List<CommentModel>();
            CommentDTO comDTO = new CommentDTO
            {
                ArticleId = model.ArticleID,
                CommentText = model.CommentText,
                CommentCreationTime = DateTime.Now,
                ApplicationUserId =appService.GetUserIdByName( User.Identity.Name)
            };
           
                appService.AddComment(comDTO);
            commentView = GetCommentsModel(model.ArticleID).OrderByDescending(c => c.CommentCreationTime);

            return PartialView("_CommentView", commentView);
        }
        public ActionResult GetComments(int id)
        {
            List<CommentModel> commentView = new List<CommentModel>();
            commentView = GetCommentsModel(id).OrderByDescending(c=>c.CommentCreationTime).ToList();
            return PartialView("_CommentView", commentView);
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
                    ApplicationUserIds = catIdList
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
                    ArticleIds = artIdList
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
                    ApplicationUserId = item.ApplicationUserId,
                    TagsIds = tagIdList,
                    UserName=appService.GetUserNameById(item.ApplicationUserId),//
                    CategoryName=appService.GetCategories().First(c=>c.Id== item.CategoryId).Title//
                    
                });
            
            }
            return articles;
        }
        private List<CommentModel> GetCommentsModel(int articleId)
        {
            List<CommentModel> commentmodel = new List<CommentModel>();
            foreach (var item in appService.GetCommentsByArticleId(articleId))
            {
                commentmodel.Add(new CommentModel
                {
                    Id = item.Id,
                    CommentText = item.CommentText,
                    UserName=appService.GetUserNameById(item.ApplicationUserId),
                    CommentCreationTime=item.CommentCreationTime,
                    ArticleId=item.ArticleId
                });
            }
            return commentmodel;
        }
        [HttpPost]
        public ActionResult ArticleSearch(string title)
        {
            var articles = GetArticlesModels().Where(art => art.Title.ToLower().Contains(title.ToLower())).ToList();
            if (articles.Count <= 0)
            {
                return HttpNotFound();
            }
            return PartialView("_ArticleList", articles);
        }
        public ActionResult AutoCompleteArticleSearch(string term)
        {
            var articles = appService.GetArticles().OrderBy(c => c.Title).Where(c => c.Title.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a.Title })
                            .Distinct();

            return Json(articles, JsonRequestBehavior.AllowGet);
        }
    }
}