using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared
{
    public class Patient : ITableEntity
    {
        public Patient()
        {
        }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        [Required]
        [StringLength(7, ErrorMessage = "Patient URN must be 7 characters long.", MinimumLength = 7)]
        public string URN { get; set; }
  
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string PresentingIssue { get; set; }

        [Required]
        public string NurseAllocated { get; set; }

        public List<Comment> CommentHistory { get; set; }

        public string GetDoB()
        {
            if(DateOfBirth != null)
            {
                return DateOfBirth.Value.ToShortDateString();
            }
            return "";
        }
        public Comment GetLastComment() {
            Comment lastComment = CommentHistory.FindLast(x => (x.CommentDescription != "Admitted" || x.CommentDescription != "Discharged"));
            return lastComment;
        }


    }
}
