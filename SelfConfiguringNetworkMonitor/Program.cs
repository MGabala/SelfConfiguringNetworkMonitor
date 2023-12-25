using SelfConfiguringNetworkMonitor.Core;

Console.Title = "Self-configuring Network Monitor";
Console.WriteLine("Bootstrapping configuration with reflection..");
NetworkMonitor.BootstrapFromConfiguration();
Console.WriteLine("Unusual traffic detected");
NetworkMonitor.Warn();