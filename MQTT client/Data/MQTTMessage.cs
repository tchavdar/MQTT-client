using System;

namespace MQTT_client.Data
{
    public class MQTTMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public string Topic { get; set; }
        public DateTime ReceivedDateTime { get; set; }

    }
}
