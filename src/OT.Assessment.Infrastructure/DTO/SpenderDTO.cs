using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.DTO
{
    public class SpenderDTO
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public decimal TotalAmountSpend { get; set; }
    }
}
