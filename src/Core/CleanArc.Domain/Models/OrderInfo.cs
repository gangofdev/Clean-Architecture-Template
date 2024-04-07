using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Models
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
