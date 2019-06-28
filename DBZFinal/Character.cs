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
        public bool PassiveDodge { get; set; }
        public bool PassiveDoubleDamage { get; set; }
        public bool PassiveHalfDamage { get; set; }
        public bool PassiveTriplePowerup { get; set; }
        public bool PassiveSteal { get; set; }
        public bool PassiveAbsorb { get; set; }
        public bool PassiveUnblockable { get; set; }
        public bool PassiveSkip { get; set; }
        public string Portrait { get; set; }
        public string PortraitLeft { get; set; }
        public string PortraitRight { get; set; }
    }
}
