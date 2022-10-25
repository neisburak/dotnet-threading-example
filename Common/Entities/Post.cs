using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
}