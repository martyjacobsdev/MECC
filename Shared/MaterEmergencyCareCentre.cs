using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class MaterEmergencyCareCentre : ITableEntity
    {
        public MaterEmergencyCareCentre()
        {
            Beds = new List<Bed>();
        }
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }

        public List<Bed> Beds { get; set; }

        public List<Patient> Patients { get; set; }
      
        public int TotalPatientsToday { get; set; }

        public int TotalBeds { get; set; }

        public int BedsInUse { get; set; }

        public int BedsFree { get; set; }

        public int GetBedsFree()
        {
            int bedsFree = 0;

            for(int i = 0; i < Beds.Count; i++)
            {
                if(Beds[i] != null && Beds[i].Status == true)
                {
                    bedsFree += 1;
                }
            }
            return bedsFree;
        }

        public int GetBedsInUse()
        {
            int bedsInUse = 0;

            for (int i = 0; i < Beds.Count; i++)
            {
                if (Beds[i] != null && Beds[i].Status == false)
                {
                    bedsInUse += 1;
                }
            }
            return bedsInUse;
        }


    }

}
