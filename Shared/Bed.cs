﻿using Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
	public class Bed
	{
		public Bed( bool status, Patient patient)
		{
			Status = status;
			Patient = patient;
		}
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public string PatientURN { get; set; }

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
