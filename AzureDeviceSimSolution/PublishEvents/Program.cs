// See https://aka.ms/new-console-template for more information
using DeviceEvents;
using ReadFiles;
using Azure;
using Azure.Messaging.EventGrid;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

// Sample CSV File read for future use. Incorporate CSV Helper
ReadCSV readCSVFile = new ReadCSV();
readCSVFile.FileName = "./ReadFiles/sample.csv";

// Create Events
List<RobotX2EventStruct> rbtX2Events = new List<RobotX2EventStruct>()
{
    new RobotX2EventStruct() { temperature = 11.5f},
    new RobotX2EventStruct() { temperature = 22.5f},
    new RobotX2EventStruct() { temperature = 33.5f},

};

Uri topicEndPoint = new("https://iot-to-adtfunction-test.eastus-1.eventgrid.azure.net/api/events");
AzureKeyCredential azureKeyCredential = new AzureKeyCredential("IYNO7TjIXGoF4jX0egzkSc1xttidgrQY2c8c0DvUFKs=");
EventGridPublisherClient eventGridPublisherClient = new EventGridPublisherClient(topicEndPoint, azureKeyCredential);

string subject = "Message from Robotic arm";
string eventType = "rbt-arm-iot";
string dataVersion = "1.0";

// This path to group and send events
//List<EventGridEvent> events = new List<EventGridEvent>();
foreach (RobotX2EventStruct rbtX2Event in rbtX2Events)
{
    EventGridEvent eventGridEvent = new EventGridEvent(subject, eventType, dataVersion, JsonConvert.SerializeObject(rbtX2Event));
    //events.Add(eventGridEvent);
    eventGridPublisherClient.SendEventAsync(eventGridEvent).Wait();
    // Send event(s) can be used to group events
    // eventGridPublisherClient.SendEventsAsync(eventGridEvent).Wait();
    //
    Console.WriteLine("Sending message: " + eventGridEvent.Data.ToString());

    Thread.Sleep(2000);

}

//eventGridPublisherClient.SendEventsAsync(events).Wait();
Console.WriteLine("Events Sent");




