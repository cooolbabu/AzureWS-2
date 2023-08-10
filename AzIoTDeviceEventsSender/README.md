### Send messages to IoT hub.

- s_connectionString = "HostName=adt-101-chupoza.azure-devices.net;DeviceId=thermostat67;SharedAccessKey=bLlpSIT.........";
- Device Id is: thermostat67. Sends a single attribute: Temperature
- Event is defined on IoT hub. The Subscript on the event points to Azure Function: Chup_IoT_ADT_Functions()
- Check Logstream for messages.
- Azure functions sends the message to Azure Digital Twin

1. Run `dotnet restore` to install the required packages.
2. Run `dotnet run` to build and run the simulated device application.
