using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public Samurai() => Quotes = new List<Quote>();//For ensure to used



        public int Id { get; set; }
        public string Name { get; set; }
        public List<Quote> Quotes { get; private set; } //use list for benefit of collection
        public Clan Clan { get; set; }
    }
}
