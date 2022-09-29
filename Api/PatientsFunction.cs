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



    }
}
