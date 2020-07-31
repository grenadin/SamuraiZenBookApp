using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class SamuraiBattle
    {
        public int SamuraiId { get; set; }//If not create the EF will generate it in DB Table
        //But Julia told it required key values
        public int BattleId { get; set; }//If not create the EF will generate it in DB Table
        //But Julia told it required key values
        public Samurai Samurai { get; set; } //Navigation key are optional
        public Battle Battle { get; set; }   //Navigation key are optional
    }
}
