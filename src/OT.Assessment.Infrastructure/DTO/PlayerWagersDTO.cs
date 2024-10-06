using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Infrastructure.DTO
{
    public class PlayerWagersDTO
    {
        public List<WagerDTO> Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        
    }

    public class WagerDTO
    {
        public Guid WagerId { get; set; }
        public string Game { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }


}
