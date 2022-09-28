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

namespace BlazorApp.Api
{
    public static class MECCFunction
    {
        [FunctionName("SummaryInformation")]
        public static IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
    ILogger log)
        {
                var tableClient = new TableClient(
                new Uri("https://mecc.table.core.windows.net/MaterEmergencyCareCentre"),
                "MaterEmergencyCareCentre",
                new TableSharedKeyCredential("mecc", "g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg=="));

                // Create the table in the service.
                tableClient.Create();

                Pageable<MaterEmergencyCareCentre> queryResultsLINQ = tableClient.Query<MaterEmergencyCareCentre>(ent => ent.PartitionKey == "mecc");
                
                return new OkObjectResult(queryResultsLINQ);
        }

    }
}
