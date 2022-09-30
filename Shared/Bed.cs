using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
	public class Bed : ITableEntity
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string PatientURN { get; set; }

        public string LastComment { get; set; }


        public bool Status { get; set; }

        public Patient Patient { get; set; }

        public string GetStatusLabel()
		{
			string statusLabel = "";

			if(Status == false)
			{
				statusLabel = "In Use";
			} else
			{
				statusLabel = "Free";
			}
			return statusLabel;
		}

	}
}
