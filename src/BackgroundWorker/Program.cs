using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BackgroundWorker
{
    public class Program
    {
        public void Main(string[] args)
        {
            //Sample storage connection string format: DefaultEndpointsProtocol=https;AccountName=azurestore;AccountKey=KIQ1QGEUvKKYibPFMVPMjfEPoQQCE3HCr71yZp/A1YvSuHrMFMK0ZlvqvSrmAym4OA3DwT05suxBMHH3/zDNWQ==
            var storageAccount = CloudStorageAccount.Parse("[Replace-with-StorageConnectionString");
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queueReference = queueClient.GetQueueReference("messages");
            while (true)
            {
                var message = queueReference.GetMessageAsync().Result;
                if (message != null)
                {
                    var userContext = JsonConvert.DeserializeObject<UserContext>(message.AsString);
                    //Send Email here
                    Console.WriteLine(userContext.Email);
                    queueReference.DeleteMessageAsync(message);
                }
                Console.WriteLine("Waiting...");
                Thread.Sleep(5000);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserContext
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
