# AzureWS

WorkSpace Repo for all work on Azure

Intro on Youtube
https://youtu.be/rLlqnt_RQ2s


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

```console
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
    dotnet new webapp -o chup-adt-webapp
    dotnet publish -o publish
    Compress-Archive . publish.zip || Compress-Archive * publish.zip
```
[Microsoft Graph Explore](https://developer.microsoft.com/en-us/graph/graph-explorer)
[Microsoft Graph Fundamentals](https://learn.microsoft.com/en-us/training/paths/m365-msgraph-fundamentals/?WT.mc_id=m365-16105-cxa)
