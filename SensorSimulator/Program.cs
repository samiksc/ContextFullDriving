using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Serialization;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace SensorSimulator
{
    class DeviceDetails
    {
        internal string DeviceId { get; set; }
        internal string VehicleId { get; set; }
        internal X509Certificate2 Certificate { get; set; }
    }
    class Program
    {
        #region Device Data

        internal static List<DeviceDetails> CurrentDevices = new List<DeviceDetails>
        {
            new DeviceDetails
            {
                DeviceId = "device1-tenant1",
                VehicleId = "MH12-3456",
                Certificate = new X509Certificate2(@"D:\Documents\cert\device1.pfx", "MyDevice$123")
            },
            new DeviceDetails
            {
                DeviceId = "device2-tenant1",
                VehicleId = "MH12-3123",
                Certificate = new X509Certificate2(@"D:\Documents\cert\device2.pfx", "MyDevice$123")
            },
            new DeviceDetails
            {
                DeviceId = "device3-tenant1",
                VehicleId = "MH12-7834",
                Certificate = new X509Certificate2(@"D:\Documents\cert\device3.pfx", "MyDevice$123")
            },
            new DeviceDetails
            {
                DeviceId = "device4-tenant1",
                VehicleId = "MH12-1267",
                Certificate = new X509Certificate2(@"D:\Documents\cert\device4.pfx", "MyDevice$123")
            },
            new DeviceDetails
            {
                DeviceId = "device5-tenant1",
                VehicleId = "MH12-3451",
                Certificate = new X509Certificate2(@"D:\Documents\cert\device5.pfx", "MyDevice$123")
            },
            new DeviceDetails
            {
                DeviceId = "device6-tenant1",
                VehicleId = "MH12-8920",
                Certificate = new X509Certificate2(@"D:\Documents\cert\device6.pfx", "MyDevice$123")
            },

        };
        #endregion Device Data

        internal static string IotHub = "Sam-Tenant1-IoTHub.azure-devices.net";
        static void Main(string[] args)
        {
            while (true)
            {
                foreach (DeviceDetails device in Program.CurrentDevices)
                {
                    IAuthenticationMethod auth =
                        new DeviceAuthenticationWithX509Certificate(device.DeviceId, device.Certificate);

                    #region Random data generation
                    int ran = new Random().Next(1, 10);
                    double lat = 18.5204 + (ran / 1000.0);
                    double lng = 73.8567 + (ran / 1000.0);
                    int dist = ran * 10;
                    int speed = ran * 8;
                    int brakeRandom = new Random().Next(1, 5);
                    string brakeStatus = "none";
                    switch (brakeRandom)
                    {
                        case 1:
                            brakeStatus = "mild";
                            break;
                        case 2:
                            brakeStatus = "moderate";
                            break;
                        case 3:
                            brakeStatus = "high";
                            break;
                        case 4:
                            brakeStatus = "hard";
                            break;
                        case 5:
                            brakeStatus = "none";
                            break;
                    }
                    #endregion

                    using (DeviceClient iotClient = DeviceClient.Create(Program.IotHub, auth))
                    {
                        iotClient.OpenAsync().GetAwaiter().GetResult();

                        #region Messages from all sensors of a device (Car)
                        string jsonMessage = string.Format(
                            "{{ \"vehicleId\": \"{0}\", \"lat\": \"{1}\", \"long\": \"{2}\", \"sensor-type\": \"distance-sensor\", \"sensor-position:\": \"back-right\", \"distance\": \"{3}\", \"object-position\": \"back-right\" }}",
                            device.VehicleId, lat, lng, dist);
                        Console.WriteLine(jsonMessage);
                        List<Message> sensorMessages = new List<Message>
                        {
                            new Message(Encoding.UTF8.GetBytes(jsonMessage)),
                            new Message(Encoding.UTF8.GetBytes(
                                string.Format(
                                    "{{ \"vehicleId\": \"{0}\", \"lat\": \"{1}\", \"long\": \"{2}\", \"sensor-type\": \"distance-sensor\", \"sensor-position:\": \"back-left\", \"distance\": \"{3}\", \"object-position\": \"back-right\" }}",
                                     device.VehicleId, lat, lng, dist))),
                            new Message(Encoding.UTF8.GetBytes(
                                string.Format(
                                    "{{ \"vehicleId\": \"{0}\", \"lat\": \"{1}\", \"long\": \"{2}\", \"sensor-type\": \"distance-sensor\", \"sensor-position:\": \"front-right\", \"distance\": \"{3}\", \"object-position\": \"front-right\" }}",
                                    device.VehicleId, lat, lng, dist))),
                            new Message(Encoding.UTF8.GetBytes(
                                string.Format(
                                    "{{ \"vehicleId\": \"{0}\", \"lat\": \"{1}\", \"long\": \"{2}\", \"sensor-type\": \"distance-sensor\", \"sensor-position:\": \"front-left\", \"distance\": \"{3}\", \"object-position\": \"back-left\" }}",
                                    device.VehicleId, lat, lng, dist))),
                            new Message(Encoding.UTF8.GetBytes(
                                string.Format(
                                    "{{ \"vehicleId\": \"{0}\", \"lat\": \"{1}\", \"long\": \"{2}\", \"sensor-type\": \"accleration-sensor\", \"speed\": \"{3}\", }}",
                                    device.VehicleId, lat, lng, speed))),
                            new Message(Encoding.UTF8.GetBytes(
                                string.Format(
                                    "{{ \"vehicleId\": \"{0}\", \"lat\": \"{1}\", \"long\": \"{2}\", \"sensor-type\": \"brake-sensor\", \"brake-status\": \"{3}\" }}",
                                    device.VehicleId, lat, lng, brakeStatus)))
                        };
                        #endregion
                        iotClient.SendEventBatchAsync(sensorMessages.AsEnumerable()).GetAwaiter()
                            .GetResult();
                        iotClient.CloseAsync().GetAwaiter().GetResult();
                        Console.WriteLine(string.Format("Sent message for {0}", device.VehicleId));
                    }
                }
            }
        }
    }
}
