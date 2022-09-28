using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
	public class Bed
	{
		public Bed(int id, bool status, Patient patient)
		{
			Id = id;
			Status = status;
			Patient = patient;
		}

		public int Id { get; set; }
        public Patient Patient { get; set; }
        public bool Status { get; set; }
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
