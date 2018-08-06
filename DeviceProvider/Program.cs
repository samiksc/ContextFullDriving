using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Provisioning.Client;
using Microsoft.Azure.Devices.Provisioning.Client.Transport;
using Microsoft.Azure.Devices.Shared;

namespace DeviceProvider
{
    class Program
    {
        // read the Azure DPS global endpoint from config - if it changes from Azure in newer deployments, we need not rebuild this program.
        private static readonly string GlobalDeviceEndpoint = ConfigurationManager.AppSettings["GlobalDPEndpoint"];
        private static readonly string IdScope = ConfigurationManager.AppSettings["DpsIdScope"];

        static void Main(string[] args)
        {
            string[] certFilePaths = Directory.GetFiles(ConfigurationManager.AppSettings["CertDirectory"], "*.pfx");
            string plainTextPassword = ConfigurationManager.AppSettings["SecureString"];
            foreach (string certFilePath in certFilePaths)
            {
                using (X509Certificate2 cert = new X509Certificate2(certFilePath, plainTextPassword, X509KeyStorageFlags.UserKeySet))
                {
                    using (var security = new SecurityProviderX509Certificate(cert))
                    using (var transport = new ProvisioningTransportHandlerAmqp(TransportFallbackType.TcpOnly))
                    {
                        ProvisioningDeviceClient provClient =
                            ProvisioningDeviceClient.Create(GlobalDeviceEndpoint, IdScope, security, transport);

                        Console.WriteLine($"Registering device with RegistrationID = {security.GetRegistrationID()}");
                        DeviceRegistrationResult result = provClient.RegisterAsync().GetAwaiter().GetResult();

                        Console.WriteLine($"{result.Status}");
                        Console.WriteLine(
                            $"ProvisioningClient AssignedHub: {result.AssignedHub}; DeviceID: {result.DeviceId}");

                    }
                }
            }
        }
    }
}
