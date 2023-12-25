using SelfConfiguringNetworkMonitor.Core;
using System.Reflection;

Console.Title = "Self-configuring Network Monitor";

NetworkMonitor.BootstrapFromConfiguration();

//var testService = Type.GetType("SelfConfiguringNetworkMonitor.Services.MailService");
//var test = Activator.CreateInstance(testService) as MailService;
//test.SendMail("test", "xd");