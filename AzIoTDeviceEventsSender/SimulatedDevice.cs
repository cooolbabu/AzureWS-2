// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This application uses the Azure IoT Hub device SDK for .NET
// For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples

using System;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace simulatedDevice
{
    class SimulatedDevice
    {
        private static DeviceClient s_deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        private const string s_connectionString = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=thermostat67;SharedAccessKey=bLlpSITjHHY4DZHkRXhk579nl7HG4KMu0KTE94Yx+SY=";

        // Async method to send simulated telemetry
        private static async void SendDeviceToCloudMessagesAsync()
        {
            // Initial telemetry values
            double minTemperature = 70;
            double minHumidity = 60;
            Random rand = new Random();

            for (int i = 0; i < 20; ++i)
            {
                double currentTemperature = Math.Round(minTemperature + rand.NextDouble() * 30, 1);
                double currentHumidity = minHumidity + rand.NextDouble() * 20;

                // Create JSON message
                var telemetryDataPoint = new
                {
                    Temperature = currentTemperature
                    //humidity = currentHumidity
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(messageString))
                {
                    ContentType = "application/json",
                    ContentEncoding = "utf-8"
                };

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                Console.WriteLine("{0}: Sending message: {1}", DateTime.Now, messageString);
                //Console.WriteLine(Encoding.UTF8.GetString(message.GetBytes()));
                // Send the telemetry message
                await s_deviceClient.SendEventAsync(message).ConfigureAwait(false);


                await Task.Delay(10000).ConfigureAwait(false);
            }
        }

        private static void Main()
        {
            Console.WriteLine("IoT Hub Quickstarts - Simulated device. Ctrl-C to exit.\n");

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
    }
}
