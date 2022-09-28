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
using static System.Net.WebRequestMethods;
using System.IO;

namespace BlazorApp.Api
{
    public static class MECCFunction
    {
        [FunctionName("Demo")]
        public static async Task<IActionResult> Run(
          [HttpTrigger(AuthorizationLevel.Function, "get",
          "post", Route = null)] HttpRequest request,
            ILogger logger)
        {
            logger.LogInformation("An HTTP triggered Azure Function.");
            string requestBody = String.Empty;
            using (StreamReader streamReader = new StreamReader(request.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            return new OkObjectResult(!string.IsNullOrEmpty(requestBody));
        }

    }
}
