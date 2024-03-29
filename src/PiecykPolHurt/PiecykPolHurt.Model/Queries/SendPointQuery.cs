﻿using PiecykPolHurt.Model.Enums;

namespace PiecykPolHurt.Model.Queries
{
    public class SendPointQuery : ListQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public SendPointSortOption? SortOption { get; set; }
    }
}
