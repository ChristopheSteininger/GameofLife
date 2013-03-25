using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameofLife
{
    static class PowerTwos
    {
        private const int SIZE = 15;

        private static int[] powers = new int[SIZE];

        public static void Initialise()
        {
            powers[0] = 1;

            for (int i = 1; i < SIZE; i++)
            {
                powers[i] = powers[i - 1] << 1;
            }
        }

        public static int Get(int exponent)
        {
            return powers[exponent];
        }
    }
}
