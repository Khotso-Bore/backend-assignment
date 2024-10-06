using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.MessageHandler
{
    public interface IMessageHandler
    {
        Task HandleMessage(byte[] bytes);
    }
}
