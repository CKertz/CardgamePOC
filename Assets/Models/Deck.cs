﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Deck
    {
        public List<Card> CardList { get; set; }
        public Deck()
        {
            CardList = new List<Card>();
        }
    }
}
