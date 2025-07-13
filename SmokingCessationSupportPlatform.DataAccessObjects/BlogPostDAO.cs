using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.DataAccessObjects
{
    public class BlogPostDAO
    {
        public static List<BlogPost> GetAllBlogPosts()
        {
            try
            {
                using var context = new SmokingCessationSupportPlatformContext();
                return context.BlogPosts.ToList();
            }
            catch(Exception ex)
            {
              throw new Exception("An error occurred while retrieving blog posts.", ex);
            }
        }
    }
}
