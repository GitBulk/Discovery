﻿using System.Collections.Generic;

namespace UndoRedo.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; set; }
    }
}
