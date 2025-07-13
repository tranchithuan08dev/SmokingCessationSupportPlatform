using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Repositories.Implementations
{
    public class BlogPostReposiory : IBlogPostRepository
    {
        public List<BlogPost> GetAllBlogPosts()
        => BlogPostDAO.GetAllBlogPosts();
    }
}
