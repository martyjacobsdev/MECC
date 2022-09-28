using BlazorApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Azure.Data.Tables.Models;
using Azure;
using System.Collections.Concurrent;
using System.Net;

namespace BlazorApp.Api
{
    public static class MECCFunction
    {

        private const string TableName = "MaterEmergencyCareCentre";

        [FunctionName("SummaryInformation")]
        public static IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
    ILogger log)
        {
            //var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            //TableClient tableClient = new TableClient(connectionString, TableName);
            //Pageable<MaterEmergencyCareCentre> queryResultsLINQ = tableClient.Query<MaterEmergencyCareCentre>(ent => ent.PartitionKey == "mecc");

             return new OkObjectResult("it got here");
        }

    }
}
