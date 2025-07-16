using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmokingCessationSupportPlatform.BusinessObjects.Models;
using SmokingCessationSupportPlatform.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SmokingCessationSupportPlatform.Web.Pages.Coach
{
    public class CreateBlogModel : PageModel
    {
        private readonly IBlogPostService _blogPostService;

        public CreateBlogModel(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }

        [BindProperty]
        public InputModel Blog { get; set; }

        public class InputModel
        {
            [Required]
            public string Title { get; set; }

            [Required]
            public string Content { get; set; }

            [Required]
            public string Category { get; set; }
        }

        public void OnGet()
        {
            // N?u b?n mu?n load d? li?u nào ?ó khi vào trang thì ?? ? ?ây
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var newPost = new BlogPost
            {
                UserId = 10,
                Title = Blog.Title,
                Content = Blog.Content,
                Category = Blog.Category,
                PostDate = DateTime.Now
            };

            _blogPostService.CreateBlogPost(newPost);

            return RedirectToPage("/Index");
        }
    }
}
