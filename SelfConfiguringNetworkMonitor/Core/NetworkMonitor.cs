using Microsoft.Extensions.Configuration;
using SelfConfiguringNetworkMonitor.Configuration;

namespace SelfConfiguringNetworkMonitor.Core
{
    public static class NetworkMonitor
    {
        private static NetworkMonitorSettings _networkMonitorSettings = new NetworkMonitorSettings();
        public static void BootstrapFromConfiguration()
        {
            var appsettingsConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            appsettingsConfiguration.Bind("NetworkMonitorSettings", _networkMonitorSettings);
        }
    }
}
