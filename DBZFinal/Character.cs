using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZFinal
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackDamage { get; set; }
        public int PowerCost { get; set; }
        public int UpgradeCost { get; set; }
        public bool Upgradable { get; set; }
        public int Form { get; set; }
        public int UltDamage { get; set; }
        public bool CanUlt { get; set; }
        public int UltCost { get; set; }
        public int PassiveChance { get; set; }
        public Passive Passives { get; set; }
        public bool PassiveActive { get; set; }
        public string Portrait { get; set; }
        public string PortraitLeft { get; set; }
        public string PortraitRight { get; set; }
    }
}
