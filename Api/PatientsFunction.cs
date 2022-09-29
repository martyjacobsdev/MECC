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
using Azure;
using System.Collections.Concurrent;
using BlazorApp.Shared;

namespace BlazorApp.Api
{
    public static class PatientsFunction
    {
        private const string TableName = "Patients";

        [FunctionName("Patients")]
        public static IActionResult Patients(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);
            List<TableEntity> qEntities = new List<TableEntity>();

            Pageable<TableEntity> queryResultsMaxPerPage = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '1' or PartitionKey eq '2'", maxPerPage: 10);

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
        [Route("UpdatePatient/{partitionKey}/{rowKey}/{URN}/{Name}/{DoB}/{PresentingIssue}/{NurseAllocated}")]
        public static async Task<bool> UpdatePatientAsync(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "UpdatePatient/{partitionKey}/{URN}/{Name}/{DoB}/{PresentingIssue}/{NurseAllocated}")] HttpRequest req, string partitionKey, string rowKey, string URN, string Name, string DoB, string PresentingIssue, string NurseAllocated,
ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);

            try
            {
                TableEntity qEntity = await tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);
                qEntity["Name"] = Name;

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
