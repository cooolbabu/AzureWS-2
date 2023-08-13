namespace JsonTestersLib;

using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonDeviceParser
{
    public static void JsonDeviceParserTest()
    {
        try
        {
            JArray o1 = JArray.Parse(File.ReadAllText("..\\Jsonfiles\\DeviceData1.json"));

            // Use the objects
            Console.WriteLine("-------------------DeviceData: ");

            string deviceId = "";
            foreach (JObject obj in o1)
            {
                deviceId = obj["DeviceId"].ToString();
                Console.WriteLine("DeviceId: " + deviceId);
                Console.WriteLine("Telemetry: " + obj["Telemetry"].ToString());

                switch (deviceId)
                {
                    case "Arm1":
                        Console.WriteLine("Message to Arm1");
                        break;
                    case "Arm2":
                        Console.WriteLine("Message to Arm2");
                        break;
                    default: break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}

public class MyObject
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsStudent { get; set; }
}

