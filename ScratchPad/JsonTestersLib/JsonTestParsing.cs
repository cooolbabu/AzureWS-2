namespace JsonTestersLib;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonTestParsing
{
    public string jsonString { get; set; }
    public class Account
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<string> Roles { get; set; }
    }

    public void SimpleTest()
    {
        Account accountX = new Account
        {
            Email = "james@example.com",
            Active = true,
            CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
            Roles = new List<string> { "User", "Admin" }
        };
        string json = JsonConvert.SerializeObject(accountX, Formatting.Indented);
        Console.WriteLine(json);

        Account accountY = JsonConvert.DeserializeObject<Account>(json);
        Console.WriteLine(accountY.Email);

        //string message = "{'systemProperties': {'iothub-connection-device-id': 'rbtx1'}, 'body': {'temperature':22.5}}";
        string message = "{\u0022DataStr\u0022:\u0022{\u0027systemProperties\u0027: {\u0027iothub-connection-device-id\u0027: \u0027rbtx1\u0027}, \u0027body\u0027: {\u0027temperature\u0027:11.5}}\u0022}";
        Console.WriteLine(message);

        JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(message);
        var temperature = deviceMessage["body"]["temperature"];
        Console.WriteLine(temperature.Value<double>());
        Console.WriteLine(deviceMessage["systemProperties"]["iothub-connection-device-id"]);
    }



}
