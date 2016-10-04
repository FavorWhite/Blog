using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IAppService
    {
        /// <summary>
        /// Adding Category
        /// </summary>
        /// <param name="userNAme"></param>
        /// <param name="categoryDTO"></param>
        void AddCategory(string userName, string categorytitle);
        /// <summary>
        /// Adding Article
        /// </summary>
        /// <param name="article"></param>
        void AddArticle(ArticleDTO articleDTO);
        /// <summary>
        /// Adding Commnet
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="userName"></param>
        /// <param name="commnet"></param>
        void AddComment(CommentDTO commnetDTO);
        /// <summary>
        /// Adding Tag
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tagtitle"></param>
        void AddTag(string userName, string tagtitle);
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="categorytitle"></param>
        void UpdateCategory(string userName, string categorytitle);
        /// <summary>
        /// Update Article
        /// </summary>
        /// <param name="articleDTO"></param>
        void UpdateArticle(ArticleDTO articleDTO);
        /// <summary>
        /// Update Tag
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tagtitle"></param>
        /// <param name="articleId"></param>
        void UpdateTag(string userName, string tagtitle, int articleId);
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        void DeleteCategory(int id);
        /// <summary>
        /// Delete Tag
        /// </summary>
        /// <param name="id"></param>
        void DeleteTag(int id);
        /// <summary>
        /// Delete Category's reference to User
        /// </summary>
        /// <param name="catId"></param>
        /// <param name="userID"></param>
        void DeleteCategoryRefToUser(int catId, string userID);
        /// <summary>
        /// Delete Tag's reference to User
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="userID"></param>
        void DeleteTagRefToUser(int tagId, string userID);
        /// <summary>
        /// Getting all Comments for the Article
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        IEnumerable<CommentDTO> GetCommentsByArticleId(int articleId);
        /// <summary>
        /// Getting User Name By Id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        string GetUserNameById(string userid);
        /// <summary>
        /// Get User Id By Name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        string GetUserIdByName(string userName);
        /// <summary>
        /// Getting Recent Articles
        /// </summary>
        /// <returns></returns>
        IEnumerable<ArticleDTO> GetArticles();
        /// <summary>
        /// Getting all categories
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IEnumerable<CategoryDTO> GetCategories();
        /// <summary>
        /// Getting all Tags
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        IEnumerable<TagDTO> GetTags();
    }
}
