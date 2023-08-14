// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This application uses the Azure IoT Hub device SDK for .NET
// For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples

using System;
using System.IO;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulateDevices
{
    class SimulatedDevice
    {


        //private const string s_connectionString = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=thermostat67;SharedAccessKey=bLlpSITjHHY4DZHkRXhk579nl7HG4KMu0KTE94Yx+SY=";
        private const string s_connectionString_arm1 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm1;SharedAccessKey=fvGD7JRwPiw/YHKWw8So37nCQlbfsaLVwOF7gGX/P8Y=";
        private const string s_connectionString_arm2 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm2;SharedAccessKey=WGeol59ie14i85GkBCQ9+EbEP9sqno+JwuFXdsQirmA=";
        private const string s_connectionString_arm3 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm3;SharedAccessKey=WjyB/vYITmxyXn1y9PZUm9SqW2261dC+f5vyYR99M/8=";
        private const string s_connectionString_arm4 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm4;SharedAccessKey=flQbJx7R5tVP5CJeZ0rKAWth1wh/dmhVhXhTaF881ow=";
        private const string s_connectionString_arm5 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm5;SharedAccessKey=4dtoJqjIqwHYRTChS+wOD8YJpyBo2GDiVYkdN4KcsSQ=";
        private const string s_connectionString_arm6 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm6;SharedAccessKey=M7dzws5zOxGBZKKPEqDNH8wW8V13+EK93KyP4K++9ZY=";

        private static DeviceClient s_deviceClient_arm1;
        private static DeviceClient s_deviceClient_arm2;
        private static DeviceClient s_deviceClient_arm3;
        private static DeviceClient s_deviceClient_arm4;
        private static DeviceClient s_deviceClient_arm5;
        private static DeviceClient s_deviceClient_arm6;

        private static string fileName ="";

        // Async method to send simulated telemetry
        private static async void SendDeviceToCloudMessagesAsync()
        {
            JArray deviceData = JArray.Parse(File.ReadAllText(fileName));
            string deviceId = "";
            int i = 1;

            try
            {
                foreach (JObject deviceDataEntry in deviceData)
                {
                    deviceId = deviceDataEntry["DeviceId"].ToString();
                    var messageString = deviceDataEntry["Telemetry"].ToString();
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(messageString))
                    {
                        ContentType = "application/json",
                        ContentEncoding = "utf-8"
                    };

                    Console.WriteLine("{0}: {1}: Message to: {2} {3}", i, DateTime.Now, deviceId, messageString);
                    //Console.WriteLine(Encoding.UTF8.GetString(message.GetBytes()));
                    // Send the telemetry message

                    switch(deviceId){
                        case "Arm1":
                            Console.WriteLine("Sending Message to Arm1");
                            await s_deviceClient_arm1.SendEventAsync(message).ConfigureAwait(false);
                            break;
                        case "Arm2":
                            Console.WriteLine("Sending Message to Arm2"); 
                            await s_deviceClient_arm2.SendEventAsync(message).ConfigureAwait(false);
                            break;

                        case "Arm3":
                            Console.WriteLine("Sending Message to Arm3"); 
                            await s_deviceClient_arm3.SendEventAsync(message).ConfigureAwait(false);
                            break;
                        case "Arm4":
                            Console.WriteLine("Sending Message to Arm4"); 
                            await s_deviceClient_arm4.SendEventAsync(message).ConfigureAwait(false);
                            break;
                        case "Arm5":
                            Console.WriteLine("Sending Message to Arm5"); 
                            await s_deviceClient_arm5.SendEventAsync(message).ConfigureAwait(false);
                            break;
                        case "Arm6":
                            Console.WriteLine("Sending Message to Arm6"); 
                            await s_deviceClient_arm6.SendEventAsync(message).ConfigureAwait(false);
                            break;
                    }
                    //
                    await Task.Delay(3000).ConfigureAwait(false);
                    ++i; // Let's keep a count of messages sent
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ingest function: {ex.Message}");
            }
        }
        private static void Main(string[] args)
        {
            Console.WriteLine("IoT Hub Quickstarts - Simulated device. Ctrl-C to exit.\n");
            fileName = args[0];

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient_arm1 = DeviceClient.CreateFromConnectionString(s_connectionString_arm1, TransportType.Mqtt);
            s_deviceClient_arm2 = DeviceClient.CreateFromConnectionString(s_connectionString_arm2, TransportType.Mqtt);
            s_deviceClient_arm3 = DeviceClient.CreateFromConnectionString(s_connectionString_arm3, TransportType.Mqtt);
            s_deviceClient_arm4 = DeviceClient.CreateFromConnectionString(s_connectionString_arm4, TransportType.Mqtt);
            s_deviceClient_arm5 = DeviceClient.CreateFromConnectionString(s_connectionString_arm5, TransportType.Mqtt);
            s_deviceClient_arm6 = DeviceClient.CreateFromConnectionString(s_connectionString_arm6, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();


        }
    }
}
