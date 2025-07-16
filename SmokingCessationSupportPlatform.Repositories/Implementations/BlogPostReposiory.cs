using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.Repositories.Implementations
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public List<BlogPost> GetAllBlogPosts()
            => BlogPostDAO.GetAllBlogPosts();

        public void CreateBlogPost(BlogPost post)
            => BlogPostDAO.CreateBlogPost(post);
    }
}
