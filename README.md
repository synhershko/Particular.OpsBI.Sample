Particular.OpsBI.Sample
=======================

This is a demo for Particular's OpsBI (https://github.com/synhershko/Particular.OpsBI), based on NServiceBus's VideoStore sample.

To run:

1. Clone this repository locally
2. Build: `.\.nuget\nuget.exe restore` to restore packages and then `MSBuild /t:Clean,Build` (assuming you have .NET v4 in your PATH; that would be `C:\Windows\Microsoft.NET\Framework\v4.0.30319`)
3. Run `VideoStore.starter.bat`
