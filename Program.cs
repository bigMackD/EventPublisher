using System;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.EventGrid;

namespace EventPublisher
{
    public class Program
    {
        //Connection Strings from newly created Event Grid Topic on Azure
        private const string topicEndpoint = "https://hrtopicmaciek.eastus-1.eventgrid.azure.net/api/events";
        private const string topicKey = "Dtr39scvnXbTa5H05/aCc/fTE1SpcjLJld5AgWYkzQ8=";

        public static async Task Main(string[] args)
        {
            Uri endpoint = new Uri(topicEndpoint);
            AzureKeyCredential credential = new AzureKeyCredential(topicKey);
            EventGridPublisherClient client = new EventGridPublisherClient(endpoint, credential);

            EventGridEvent firstEvent = new EventGridEvent(
                subject: $"New Employee: Maciek Drozdowicz",
                eventType: "Employees.Registration.New",
                dataVersion: "1.0",
                data: new
                {
                    FullName = "Maciek Drozdowicz",
                    Address = "4567 Pine Avenue, Edison, WA 97202"
                }
            );

            EventGridEvent secondEvent = new EventGridEvent(
                subject: $"New Employee: Pan Maciej",
                eventType: "Employees.Registration.New",
                dataVersion: "1.0",
                data: new
                {
                    FullName = "Pan Maciej",
                    Address = "456 College Street, Bow, WA 98107"
                }
            );

            await client.SendEventAsync(firstEvent);
            Console.WriteLine("First event published");

            await client.SendEventAsync(secondEvent);
            Console.WriteLine("Second event published");
        }
    }
}
