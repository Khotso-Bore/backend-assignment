using OT.Assessment.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        void Publish<T>(T obj);
    }
}
