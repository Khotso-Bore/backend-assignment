using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.Settings
{
    public class RabbitSettings
    {
        public string User {  get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName {  get; set; }
        public string RoutingKey { get; set; }
    }
}
