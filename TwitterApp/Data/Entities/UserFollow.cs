using System;
using System.Collections.Generic;

namespace TwitterApp.Data.Entities;

public partial class UserFollow
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FollowId { get; set; }

    public DateTime CreateDate { get; set; }

    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}
