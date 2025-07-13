using SmokingCessationSupportPlatform.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.Services.Interfaces
{
    public interface IBlogPostService
    {
        List<BlogPost> GetAllBlogPosts();
    }
}
