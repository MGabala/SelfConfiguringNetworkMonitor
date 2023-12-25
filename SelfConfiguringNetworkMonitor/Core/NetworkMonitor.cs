using Microsoft.Extensions.Configuration;
using SelfConfiguringNetworkMonitor.Configuration;

using System.Reflection;

namespace SelfConfiguringNetworkMonitor.Core
{
    public static class NetworkMonitor
    {
        private static NetworkMonitorSettings _networkMonitorSettings = new NetworkMonitorSettings();
        private static Type? _warningServiceType;
        private static MethodInfo? _warningServiceMethod;
        private static List<object> _warningServiceParametersValues = new List<object>();
        public static void BootstrapFromConfiguration()
        {
            var appsettingsConfiguration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            appsettingsConfiguration.Bind(key: "NetworkMonitorSettings", instance: _networkMonitorSettings);

            _warningServiceType = Assembly.GetExecutingAssembly().GetType(name: _networkMonitorSettings.WarningService);
            if(_warningServiceType == null)
            {
                throw new Exception(message: "Configuration is invalid - warning service not found");
            }

            _warningServiceMethod = _warningServiceType.GetMethod(name: _networkMonitorSettings.MethodToExecute);
            if(_warningServiceMethod == null)
            {
                throw new Exception(message: "Configuration is invalid - method to execute on warning service not found");
            }
            
            foreach(ParameterInfo parameter in _warningServiceMethod.GetParameters())
            {
                if(!_networkMonitorSettings.PropertyBag.TryGetValue(key: parameter.Name, 
                                                                    value: out object? parameterValue))
                {
                    throw new Exception($"Configuration is invalid - parameter {parameter.Name} not found");
                }
                try
                {
                    object? value = Convert.ChangeType(value: parameterValue, conversionType: parameter.ParameterType);
                    _warningServiceParametersValues.Add(item: value);
                }
                catch(Exception ex)
                {
                    throw new Exception(message: $"Configuration is invalid - parameter {parameter.Name} cannot be converted to expected type {parameter.ParameterType}.");
                }
            }
        }
    }
}
