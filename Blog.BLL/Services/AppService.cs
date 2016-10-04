using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;
using Blog.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Blog.BLL.Interfaces;
using Blog.DAL.Interfaces;
using Blog.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System;
using AutoMapper;
using System.Text.RegularExpressions;

namespace Blog.BLL.Services
{
    public class AppService :IAppService
    {
        IUnitOfWork Database = new IdentityUnitOfWork("DefaultConnection");
        public void AddCategory(string userName,string  categorytitle)
        {
            Category category = new Category
            {
                Title = categorytitle
            };

            category.ApplicationUsers.Add(Database.UserManager.FindByName(userName));
            Database.Categories.Create(category);
            Database.Save();

        }
        public void UpdateCategory(string userName, string categorytitle)
        {
            Category category = Database.Categories.GetAll().First(cat => cat.Title == categorytitle);
         
            if (!category.ApplicationUsers.Any(cat=>cat.UserName==userName))
            {
                category.ApplicationUsers.Add(Database.UserManager.FindByName(userName));
                Database.Categories.Update(category);
                Database.Save();
            }
        }
        public void AddTag(string userName, string tagtitle)
        {
            Tag tag = new Tag
            {
                Title = tagtitle
            };

            tag.ApplicationUsers.Add(Database.UserManager.FindByName(userName));
            Database.Tags.Create(tag);
            Database.Save();

        }
        public void UpdateTag(string userName, string tagtitle, int articleId)
        {
            Tag tag = Database.Tags.GetAll().First(t => t.Title == tagtitle);

            if (!tag.ApplicationUsers.Any(cat => cat.UserName == userName))
            {
                tag.ApplicationUsers.Add(Database.UserManager.FindByName(userName));
                Database.Tags.Update(tag);
                Database.Save();
            }
            if (!tag.Articles.Any(t => t.Id == articleId))
            {
                tag.Articles.Add(Database.Articles.Get(articleId));
                Database.Tags.Update(tag);
                Database.Save();
            }
        }
        public void DeleteCategory(int id)
        {

            Database.Categories.Delete(id);
            Database.Save();

        }
        public void DeleteTag(int id)
        {

            Database.Tags.Delete(id);
            Database.Save();

        }
        public void DeleteCategoryRefToUser(int catId, string userID)
        {
            Category category = Database.Categories.Get(catId);
            category.ApplicationUsers.Remove(Database.UserManager.FindById(userID));
            Database.Categories.Update(category);
            Database.Save();
        }
        public void DeleteTagRefToUser(int tagId, string userID)
        {
            Tag tag = Database.Tags.Get(tagId);
            tag.ApplicationUsers.Remove(Database.UserManager.FindById(userID));
            Database.Tags.Update(tag);
            Database.Save(); 
        }

        public void AddArticle(ArticleDTO articleDTO)
        {
            Article article = new Article
            {
                CategoryId = articleDTO.CategoryId,
                Title = articleDTO.Title,
                ArticleText = articleDTO.ArticleText,
                ShortDescription=articleDTO.ShortDescription,
                ArticleCreationTime = articleDTO.ArticleCreationTime,
                ApplicationUserId=articleDTO.ApplicationUserId
            };
            Database.Articles.Create(article);
            Database.Save();
        }

        public void AddComment(CommentDTO commnetDTO)
        {

            Comment commnet = new Comment
            {
                ApplicationUserId = commnetDTO.ApplicationUserId,
                ArticleId = commnetDTO.ArticleId,
                CommentText = commnetDTO.CommentText,
                CommentCreationTime = commnetDTO.CommentCreationTime
            };
            Database.Comments.Create(commnet);
            Database.Save();
        }

        public void UpdateArticle(ArticleDTO articleDTO)
        {
            Article article = new Article
            {
                Id=articleDTO.Id,
                CategoryId = articleDTO.CategoryId,
                Title = articleDTO.Title,
                ArticleText = articleDTO.ArticleText,
                ShortDescription = articleDTO.ShortDescription,
                ArticleCreationTime = articleDTO.ArticleCreationTime,
                ApplicationUserId = articleDTO.ApplicationUserId,
            };
            Database.Articles.Update(article);
            Database.Save();
        }
        
        public bool IsCategoryEmpty(int id)
        {
           return !Database.Categories.Get(id).Articles.Any();
        }
        public bool IsTagEmpty(int id)
        {
            return !Database.Tags.Get(id).Articles.Any();
        }
        public void DeleteArticle(int id)
        {
            Database.Articles.Delete(id);
            Database.Save();
        }
        public IEnumerable<CommentDTO> GetCommentsByArticleId(int articleId)
        {
            List<CommentDTO> comments = new List<CommentDTO>();
            Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDTO>());
            IEnumerable<CommentDTO> commentsDTO = Mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(Database.Comments.GetAll());

            foreach (var item in commentsDTO)
            {
                if (item.ArticleId == articleId)
                {
                    comments.Add(item);
                }
            }

            return comments;
        }
        public IEnumerable<ArticleDTO> GetArticles()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Article, ArticleDTO>());
            IEnumerable<ArticleDTO> articles = Mapper.Map<IEnumerable<Article>, List<ArticleDTO>>(Database.Articles.GetAll());

            foreach (var item in articles)
            {
                foreach (var tag in Database.Tags.GetAll())
                {
                    foreach (var art in tag.Articles)
                    {
                        if (art.Id == item.Id)
                        {
                            item.ArticleTags.Add(new TagDTO
                            {
                                Id = tag.Id
                            });
                        }
                    }
                }
            }

            return articles;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
            IEnumerable<CategoryDTO> cateories = Mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
            foreach (var item in cateories)
            {
                foreach (var user in Database.UserManager.Users)
                {
                    foreach (var usercat in user.Categories)
                    {
                        if (usercat.Id == item.Id)
                        {
                            item.Users.Add(new UserDTO
                            {
                                Id = user.Id,
                            });
                        }
                    }
                }
            }
            return cateories;
        }
        public IEnumerable<TagDTO> GetTags()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Tag, TagDTO>());
            IEnumerable<TagDTO> tags = Mapper.Map<IEnumerable<Tag>, List<TagDTO>>(Database.Tags.GetAll());
            foreach (var item in tags)
            {
                foreach (var user in Database.UserManager.Users)
                {
                    foreach (var usertag in user.Tags)
                    {
                        if (usertag.Id == item.Id)
                        {
                            item.Users.Add(new UserDTO
                            {
                                Id = user.Id,
                            });
                        }
                    }
                }
                foreach (var art in Database.Articles.GetAll())
                {
                    foreach (var arttag in art.Tags)
                    {
                        if (arttag.Id == item.Id)
                        {
                            item.TagArticles.Add(new ArticleDTO
                            {
                                Id = art.Id
                            });
                        }
                    }
                }
            }
            return tags;
        }
        public string GetUserNameById(string userid)
        {
            return Database.UserManager.FindById(userid).UserName;
        }
        public string GetUserIdByName(string userName)
        {
            return Database.UserManager.FindByName(userName).Id;

        }
        public List<string> ParseTagString(string tagString)
        {
            List<string> tagList = new List<string>(); 
            Regex regex = new Regex(@"\b\w+\b");
            MatchCollection matches = regex.Matches(tagString);
            foreach (Match match in matches)
            {
                tagList.Add(match.Value);
            }
            return tagList;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
