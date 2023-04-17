using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Models
{
    class ShopItem
    {
        public string CardName { get; set; }
        public float CardPrice { get; set; }
        //CardImagePath maybe should be Image item? 
        public string CardImagePath { get; set; }
    }
}
