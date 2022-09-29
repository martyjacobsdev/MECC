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
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net;

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
[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "")] HttpRequest req, string partitionKey, string rowKey, string URN, string Name, string DoB, string PresentingIssue, string NurseAllocated,
ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);

            try
            {
                TableEntity qEntity = await tableClient.GetEntityAsync<TableEntity>(partitionKey, rowKey);
                Console.WriteLine(qEntity["Name"]);

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


        [FunctionName("PatientTest")]
        public static async Task<HttpResponseMessage> PatientTest(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "orchestrators/contoso_function01/{id:int}/{username:alpha}")]
    HttpRequestMessage req,
    int id,
    string username,
    TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            return (id == 0 || string.IsNullOrEmpty(username))
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + id + " " + username);
        }



    }
}
