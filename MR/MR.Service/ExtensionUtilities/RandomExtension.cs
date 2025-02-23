using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MR.Service.ExtensionUtilities
{
    public class RandomExtension
    {
        public static Random GetRandom()
        {
            long tick = DateTime.Now.Ticks;
            var rand = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return rand;
        }
    }
}
