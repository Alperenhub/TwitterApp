using System;
using System.Collections.Generic;

namespace TwitterApp.Data.Entities;

public partial class PostLike
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsActive { get; set; }

    public virtual Post Post { get; set; } = null!;
}
