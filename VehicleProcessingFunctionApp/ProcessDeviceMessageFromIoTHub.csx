#r "Newtonsoft.Json"
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class VehiclePosition
{
    public Guid Id { get; set; }
    public string VehicleId { get; set; }
    public string BrakeStatus { get; set; }
    public int Speed { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int FrontRightObjectDistance { get; set; }
    public int FrontLeftObjectDistance { get; set; }
    public int BackRightObjectDistance { get; set; }
    public int BackLeftObjectDistance { get; set; }
}

[JsonObject]
public class SensorMessage
{
    [JsonProperty(PropertyName = "vehicleId")]
    public string VehicleId { get; set; }
    [JsonProperty(PropertyName = "lat")]
    public double Latitude { get; set; }
    [JsonProperty(PropertyName = "long")]
    public double Longitude { get; set; }
    [JsonProperty(PropertyName = "sensor-type")]
    public string SensorType { get; set; }
    [JsonProperty(PropertyName = "sensor-position")]
    public string SensorPosition { get; set; }
    [JsonProperty(PropertyName = "distance")]
    public int Distance { get; set; }
    [JsonProperty(PropertyName = "object-position")]
    public string ObjectPosition { get; set; }
    [JsonProperty(PropertyName = "speed")]
    public int Speed { get; set; }
    [JsonProperty(PropertyName = "brake-status")]
    public string BrakeStatus { get; set; }
}
public static void Run(string myEventHubMessage, TraceWriter log, out VehiclePosition vehicleLocationData)
{
    log.Info($"C# Event Hub trigger function processed a message: {myEventHubMessage}");
    SensorMessage sensorMessage = JsonConvert.DeserializeObject<SensorMessage>(myEventHubMessage);
    int backLeftDist = 0;
    int backRightDist = 0;
    int frontLeftDist = 0;
    int frontRightDist = 0;
    if (!string.IsNullOrEmpty(sensorMessage.ObjectPosition))
    {
        backLeftDist = sensorMessage.ObjectPosition.Equals("back-left", StringComparison.OrdinalIgnoreCase)
            ? sensorMessage.Distance
            : 0;
        backRightDist =
            sensorMessage.ObjectPosition.Equals("back-right", StringComparison.OrdinalIgnoreCase)
                ? sensorMessage.Distance
                : 0;
        frontLeftDist =
            sensorMessage.ObjectPosition.Equals("front-left", StringComparison.OrdinalIgnoreCase)
                ? sensorMessage.Distance
                : 0;
        frontRightDist =
            sensorMessage.ObjectPosition.Equals("front-right", StringComparison.OrdinalIgnoreCase)
                ? sensorMessage.Distance
                : 0;
    }
    vehicleLocationData = new VehiclePosition
    {
        Id = Guid.NewGuid(),
        VehicleId = sensorMessage.VehicleId,
        BrakeStatus = sensorMessage.BrakeStatus ?? string.Empty,
        Speed = sensorMessage.Speed,
        Latitude = sensorMessage.Latitude,
        Longitude = sensorMessage.Longitude,
        BackLeftObjectDistance = backLeftDist,
        BackRightObjectDistance = backRightDist,
        FrontLeftObjectDistance = frontLeftDist,
        FrontRightObjectDistance = frontRightDist
    };
    log.Info("Created VehiclePosition object");
}
