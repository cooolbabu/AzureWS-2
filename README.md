# AzureWS

 WorkSpace Repo for all work on Azure

### Azure IoT to ADT

# Azure-IoT-ADT
Project containing Azure IoT to Azure ADT using VS Code

This work is based on public material. My objective is get the solution working on .NET 6. At some point in the future upgrade to .NET 7

Development tool is VS Code.

Basic steps


```console
    > dotnet --list-sdk
    3.1.426 [C:\Program Files\dotnet\sdk]
    6.0.412 [C:\Program Files\dotnet\sdk]
    7.0.306 [C:\Program Files\dotnet\sdk]
    PS C:\Users\coool\Documents\AzureWS\AzureEvents>
```
**Packages**
``` console
    dotnet add package Azure.Messaging.EventGrid
    dotnet add package Newtonsoft.Json
```

**Project References**
```console
    dotnet add PublishEvents/PublishEvents.cproj reference ReadFiles/ReadFiles.csproj
    dotnet add PublishEvents/PublishEvents.cproj reference ReadFiles/ReadFiles.csproj 
    dotnet add PublishEvents/PublishEvents.csproj reference DeviceEvents/DeviceEvents.csproj
```

Other unimportant stuff
```console
    dotnet new class -n ReadCSV
    dotnet sln add ReadFiles/ReadFiles.csproj
    dotnet new classlib -o ReadFiles
```
