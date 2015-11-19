using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using MQTT_client.Data;

namespace MQTT_client.Migrations
{
    [DbContext(typeof(RawMQTTDataModel))]
    partial class RawMQTTDataModelModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964");

            modelBuilder.Entity("MQTT_client.Data.MQTTMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.Property<DateTime>("ReceivedDateTime");

                    b.Property<string>("Topic");

                    b.HasKey("Id");
                });
        }
    }
}
