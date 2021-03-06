﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureCoreOne
{
    public class SystemSettings
    {
        public string ApplicationName { get; set; } = "The Great Application";
        public int MaxItemsPerList { get; set; } = 15;

        public List<MyRoute> Routes { get; set; }
        //public ConcurrentBag<MyRoute> Routes { get; set; }
    }

    public class MyRoute
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
