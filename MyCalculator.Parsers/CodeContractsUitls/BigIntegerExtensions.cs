using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MyCalculator.Parsers.CodeContractsUitls
{
    public static class BigIntegerExtensions
    {
        public static BigInteger SquareRoot(this BigInteger number)
        {
            BigInteger n = 0, p = 0;
            if (number == BigInteger.Zero)
            {
                return BigInteger.Zero;
            }
            var high = number >> 1;
            var low = BigInteger.Zero;
            while (high > low + 1)
            {
                n = (high + low) << 1;
                p = n* n;
                if (number < p)
                {
                    high = n;
                }
                else if (number > p)
                {
                    low = n;
                }
                else
                {
                    break;
                }
            }
            return number == p ? n : low;
        }

    }
}
