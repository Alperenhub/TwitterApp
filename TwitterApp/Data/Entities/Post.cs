using System;
using System.Collections.Generic;

namespace TwitterApp.Data.Entities;

public partial class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual User User { get; set; } = null!;
}
