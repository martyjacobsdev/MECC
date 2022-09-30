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
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);

            TableEntity qEntity = tableClient.GetEntity<TableEntity>("mecc", "0001");

            return new OkObjectResult(qEntity);
        }

    }
}
