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
    public static class CommentsFunction
    {
        private const string TableName = "Comments";

        [FunctionName("AddComment")]
        public static async Task AddComment(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "")] HttpRequest req,
      ILogger log)
        {
            TableClient tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=mecc;AccountKey=g0ccRGdcm9vJFhumv+vIJKhyM6CqJIOq+byy0s4IdXWXwKIOQU9H4wull8bAltEH93FjgD6woHCf+ASt2W4dUg==;EndpointSuffix=core.windows.net", TableName);

            try
            {
                Comment comment = await JsonSerializer.DeserializeAsync<Comment>(req.Body);
                TableEntity qEntity = await tableClient.GetEntityAsync<TableEntity>(comment.PartitionKey, comment.RowKey);
                qEntity["CommentDescription"] = comment.CommentDescription;
                qEntity["NurseAllocated"] = comment.NurseAllocated;
                qEntity["DateTimeOfComment"] = comment.DateTimeOfComment.ToUniversalTime();
  
                tableClient.AddEntity(qEntity);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }
}
