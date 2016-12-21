﻿using System.Collections.Generic;

namespace Tam.Core.Filters.RequestFiltering.Files
{
    public class FileExtensionsOptions: IRequestFilterOptions
    {
        public bool AllowUnlisted { get; set; } = true;
        public IList<FileExtensionElement> FileExtensionCollection { get; set; } = new List<FileExtensionElement>();
    }
}
