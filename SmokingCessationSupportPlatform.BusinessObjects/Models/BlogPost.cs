using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class BlogPost
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime? PostDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? Category { get; set; }

    public bool? IsPublished { get; set; }

    public virtual ICollection<BlogComment> BlogComments { get; set; } = new List<BlogComment>();

    public virtual User User { get; set; } = null!;
}
