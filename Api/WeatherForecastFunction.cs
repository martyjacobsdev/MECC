using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using BlazorApp.Shared;
using Azure.Data.Tables;

namespace BlazorApp.Api
{
    public static class WeatherForecastFunction
    {
        private static string GetSummary(int temp)
        {
            var summary = "Mild";

            if (temp >= 32)
            {
                summary = "Hot";
            }
            else if (temp <= 16 && temp > 0)
            {
                summary = "Cold";
            }
            else if (temp <= 0)
            {
                summary = "Freezing!";
            }

            return summary;
        }

        [FunctionName("WeatherForecast")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            
            // Construct a new TableClient using a TableSharedKeyCredential.
            var client = new TableClient(
                new Uri("https://mecc.table.core.windows.net/MaterEmergencyCareCentre"),
                "MaterEmergencyCareCentre",
                new TableSharedKeyCredential("mecc", connectionString));

            var result = client.GetEntity<MaterEmergencyCareCentre>("mecc", "0001");

            return new OkObjectResult(result);

        }
    }
}
