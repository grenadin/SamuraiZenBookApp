using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Samurai
    {


        public Samurai()
        {
            Quotes = new List<Quote>(); //For ensure to used;
            SamuraiBattles = new List<SamuraiBattle>();//For ensure to used
        }

        //public Samurai() => SamuraiBattles = new List<SamuraiBattle>();

        public int Id { get; set; }
        public string Name { get; set; }


        public List<Quote> Quotes { get; private set; } //use list for benefit of collection
        public Clan Clan { get; set; } // refer key to clan 
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        public Horse Horse { get; set; }

        //use expression body for multiple constructor
        //public Samurai(List<SamuraiBattle> samuraiBattles, List<Quote> quotes) => (SamuraiBattles,Quotes) = (new List<SamuraiBattle>(),new List<Quote>());
        //


    }
}
