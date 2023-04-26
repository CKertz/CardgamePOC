using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public List<MenuItem> CustomerOrder { get; set; }
        public string CustomerSpritePath { get; set; }

        //not strings but each customer will need 1, 2 timers on them
        //public string OrderTakenPatienceTimer {get;set;}
        //public string OrderCompletionPatienceTimer{get;set;}
    }
}
