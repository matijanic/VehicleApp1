using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using VehicleProject.Models;

namespace VehicleProject.Util
{
    public static class Sender
    {

        public static void SendMessage (VehicleContainer newOwner)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var conncetion = factory.CreateConnection();
            using var chanel = conncetion.CreateModel();

            chanel.QueueDeclare("vehicle",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newOwner));

            chanel.BasicPublish(exchange: "",
                routingKey: "vehicle",
                basicProperties: null,
                body: body);
        
        }

        

    }
        
}
