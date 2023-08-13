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
        private const string s_connectionString_arm1 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm1;SharedAccessKey=WGeol59ie14i85GkBCQ9+EbEP9sqno+JwuFXdsQirmA=";
        private const string s_connectionString_arm2 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm2;SharedAccessKey=WGeol59ie14i85GkBCQ9+EbEP9sqno+JwuFXdsQirmA=";
        private const string s_connectionString_arm3 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm3;SharedAccessKey=WGeol59ie14i85GkBCQ9+EbEP9sqno+JwuFXdsQirmA=";
        private const string s_connectionString_arm4 = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=Arm4;SharedAccessKey=WGeol59ie14i85GkBCQ9+EbEP9sqno+JwuFXdsQirmA=";

        private static DeviceClient s_deviceClient;

        // Async method to send simulated telemetry
        private static async void SendDeviceToCloudMessagesAsync()
        {
            JArray deviceData = JArray.Parse(File.ReadAllText(".\\DeviceData\\DeviceData1.json"));
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
                    await s_deviceClient.SendEventAsync(message).ConfigureAwait(false);
                    await Task.Delay(10000).ConfigureAwait(false);
                    ++i; // Let's keep a count of messages sent
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ingest function: {ex.Message}");
            }
        }
        private static void Main()
        {
            Console.WriteLine("IoT Hub Quickstarts - Simulated device. Ctrl-C to exit.\n");

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString_arm2, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();


        }
    }
}
