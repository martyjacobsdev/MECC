using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
	public class Comment
	{
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public DateTime DateTimeOfComment { get; set; }
        public string NurseAllocated { get; set; }
        public string CommentDescription { get; set; }

    }
}
