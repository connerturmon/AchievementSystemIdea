using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchievementSystemIdea.Core
{
    public class ProgressBar
    {
        public int Current { get; set; }
        public int Total { get; set; }
        public ProgressBar(int total, int current = 0)
        {
            Current = current;
            Total = total;
        }
    }
}
