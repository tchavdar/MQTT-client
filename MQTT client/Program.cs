using System;
using System.Text;
using Microsoft.Data.Entity;
using MQTT_client.Data;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MQTT_client
{
    class MQQT
    {
        public RawMQTTDataModel Db { get; set; }

        public MqttClient client { get; set; }

        public void Connect(string broker)
        {
            client = new MqttClient(broker);
            byte code = client.Connect("DOT NET");
        }

        public void Publish(string s)
        {
            ushort msgId = client.Publish("myhome/test/humidity", Encoding.UTF8.GetBytes(s), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            client.MqttMsgPublished += client_MqttMsgPublished;
        }

        public void Subscribe(string topic)
        {
            ushort msgId = client.Subscribe(new[] {topic}, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE});
            client.MqttMsgSubscribed += client_MqttMsgSubscribed;
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        }

        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);
            using (var db = new RawMQTTDataModel())
            {
                var message = new MQTTMessage{
                    Topic=e.Topic, Message = Encoding.UTF8.GetString(e.Message), ReceivedDateTime = DateTime.Now
                };
                
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Console.WriteLine("Subscribed for id = " + e.MessageId);
        }
        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
        }

        public void Disconnect()
        {
            client.Disconnect();
        }


    }

    static class Program
    {
        
        static void Main(string[] args)
        {
            using (var db = new RawMQTTDataModel())
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
            MQQT client = new MQQT();
            client.Connect("192.168.2.122");
            client.Subscribe("myhome/test/#");
            client.Publish("TEST NET");
            Console.ReadLine();
            client.Disconnect();

        }
    }
}
