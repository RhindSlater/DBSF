using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZFinal
{
    public class WinPercentage
    {
        public int ID { get; set; }
        public string Player { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public decimal Percentage { get; set; }
        public string Character { get; set; }
    }
}
