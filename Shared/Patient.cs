﻿using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class Patient : ITableEntity
    {
        public Patient()
        {
        }

        public Patient(string bed)
        {
            Bed = bed;
        }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string Bed { get; set; }

        public string URN { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string PresentingIssue { get; set; }

        public string NurseAllocated { get; set; }

        public List<Comment> CommentHistory { get; set; }
        public Comment GetLastComment() {
            Comment lastComment = CommentHistory.FindLast(x => (x.CommentDescription != "Admitted" || x.CommentDescription != "Discharged"));
            return lastComment;
        }


    }
}
