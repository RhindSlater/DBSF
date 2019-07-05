using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZFinal
{
    public class Context : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<WinPercentage> WinPercentages { get; set; }
        public DbSet<Passive> Passives { get; set; }
    }
}