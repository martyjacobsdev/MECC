using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Azure;
using System.Collections.Concurrent;
using BlazorApp.Shared;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net;

namespace BlazorApp.Api
{
    public static class PatientsFunction
    {
        private const string TableName = "Patients";
        private const string MECC = "MaterEmergencyCareCentre";


        [FunctionName("Patients")]
        public static IActionResult Patients(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);
            List<TableEntity> qEntities = new List<TableEntity>();

            Pageable<TableEntity> queryResultsMaxPerPage = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '1' or PartitionKey eq '2' or PartitionKey eq '3' or PartitionKey eq '4' or PartitionKey eq '5' or PartitionKey eq '6' or PartitionKey eq '7' or PartitionKey eq '8'", maxPerPage: 10);

            foreach (Page<TableEntity> page in queryResultsMaxPerPage.AsPages())
            {
                foreach (TableEntity qEntity in page.Values)
                {
                    qEntities.Add(qEntity);
                }
            }

            return new OkObjectResult(qEntities);
        }

        [FunctionName("UpdatePatient")]
        public static async Task<bool> UpdatePatientAsync(
[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "")] HttpRequest req,
ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);
            TableClient meccTableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", MECC);

            try
            {
                // Update the patient row 
                Patient patient = await JsonSerializer.DeserializeAsync<Patient>(req.Body);
                TableEntity qEntity = await tableClient.GetEntityAsync<TableEntity>(patient.PartitionKey, patient.RowKey);
                qEntity["Name"] = patient.Name;
                qEntity["PresentingIssue"] = patient.PresentingIssue;

                qEntity["NurseAllocated"] = patient.NurseAllocated;
                qEntity["URN"] = patient.URN;

                // Since no UpdateMode was passed, the request will default to Merge.
                await tableClient.UpdateEntityAsync(qEntity, qEntity.ETag);

                // Update the admitted patient count 
                TableEntity meccEntity = await meccTableClient.GetEntityAsync<TableEntity>("1", "0001");
                string currentTotal = meccEntity["TotalPatientsToday"].ToString();
                Console.WriteLine("current total" + currentTotal);

                int newTotal = int.Parse(currentTotal) + 1;

                meccEntity["TotalPatientsToday"] = newTotal.ToString();

                Console.WriteLine("new total" + newTotal);

                // Since no UpdateMode was passed, the request will default to Merge.
                await meccTableClient.UpdateEntityAsync(meccEntity, meccEntity.ETag);

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        [FunctionName("DischargePatient")]
        public static async Task<bool> DischargePatient(
[HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
ILogger log)
        {

            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);

            try
            {
                // Update the patient row 
                string id = req.Query["bedId"];
                Console.WriteLine("id" + id);
                TableEntity qEntity = await tableClient.GetEntityAsync<TableEntity>(id, "000" + id);

                qEntity["Name"] = "";
                qEntity["PresentingIssue"] = "";
                qEntity["NurseAllocated"] = "";
                qEntity["URN"] = "";
                //other values null 

                // Since no UpdateMode was passed, the request will default to Merge.
                await tableClient.UpdateEntityAsync(qEntity, qEntity.ETag);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
