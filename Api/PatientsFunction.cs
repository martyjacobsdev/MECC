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

namespace BlazorApp.Api
{
    public static class PatientsFunction
	{
        private const string TableName = "Patients";

        [FunctionName("Patients")]
        public static IActionResult GetAllPatients(
[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);
            List<TableEntity> qEntities = new List<TableEntity>();

            for (int i = 1; i <= 8; i++)
            {
                TableEntity qEntity = tableClient.GetEntity<TableEntity>("Patient000" + i.ToString(), "000" + i.ToString());
                qEntities.Add(qEntity);
            }

            return new OkObjectResult(qEntities);
        }
    }
}
