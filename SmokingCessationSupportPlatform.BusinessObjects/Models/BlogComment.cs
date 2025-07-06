using System;
using System.Collections.Generic;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models;

public partial class BlogComment
{
    public int CommentId { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public string CommentContent { get; set; } = null!;

    public DateTime? CommentDate { get; set; }

    public virtual BlogPost Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
