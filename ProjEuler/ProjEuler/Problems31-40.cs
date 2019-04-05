using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjEuler
{
    class Problem31
    {
        public static List<int> coins = new List<int>() { 1, 2, 5, 10, 20, 50, 100, 200 };

        public static int CoinSums(int n)
        {
            int[,] table = new int[n + 1, coins.Count];
            for (int i = 0; i < n + 1; i++)
            {
                table[i, 0] = 1;
            }
            for (int i = 0; i < n + 1; i++)
            {
                for (int j = 1; j < coins.Count; j++)
                {
                    table[i, j] = table[i, j - 1];
                    if (coins[j] <= i)
                    {
                        table[i, j] += table[i - coins[j], j];
                    }
                }
            }
            return table[n, coins.Count - 1];
        }
    }

    class Problem32
    {
        public static int PandigitalProducts()
        {
            HashSet<int> products = new HashSet<int>();
            for (int i = 1; i < 5000; i++)
            {
                for (int j = 1; j < 5000; j++)
                {
                    List<int> tempdigits = new List<int>();
                    tempdigits.AddRange(MiscFunctions.DigitsFromInt(i));
                    tempdigits.AddRange(MiscFunctions.DigitsFromInt(j));
                    tempdigits.AddRange(MiscFunctions.DigitsFromInt(i * j));

                    if (MiscFunctions.IsPandigital(tempdigits))
                    {
                        products.Add(i * j);
                    }
                }
            }
            return products.Sum();
        }
    }

    class Problem33
    {
        public static int DigitCancellingFractions(int n)
        {
            int numerator = 1;
            int denominator = 1;
            for (int i = 10; i < 99; i++)
            {
                for (int j = i + 1; j < 99; j++)
                {
                    if (i % 10 == 0 && j % 10 == 0)
                    {
                        break;
                    }
                    double frac = (double)i / (double)j;

                    double frac2 = MiscFunctions.CancelOutSameDigit(i, j);
                    if (Math.Abs(frac - frac2) < 0.001)
                    {
                        Console.WriteLine("{0} {1}", i, j);
                        numerator *= i;
                        denominator *= j;
                    }
                }
            }
            int[] fracarr = new int[2] { numerator, denominator };
            MiscFunctions.Simplify(fracarr);
            return fracarr[1];
        }
    }

    class Problem34
    {
        public static int DigitFactorials(int n)
        {
            int sum = 0;
            CombinatoricFunctions.generatefactorial(10);
            for (int i = 3; i < n; i++)
            {
                int[] digits = MiscFunctions.DigitsFromInt(i);
                BigInteger tempsum = 0;
                foreach (int digit in digits)
                {
                    tempsum += CombinatoricFunctions.factorial(digit);
                }
                if (tempsum == i)
                {
                    sum += i;
                }
            }
            return sum;
        }
    }

    class Problem35
    {
        public static int CirculuarPrimes(int n)
        {
            PrimeFunctions.GeneratePrimesTillNToList(1000000);
            PrimeFunctions.ConvertToHash();

            int count = 0;
            for (int i = 2; i < n; i++)
            {
                List<int> rots = CombinatoricFunctions.rotations(i);
                bool prime = true;
                foreach (int rot in rots)
                {
                    if (!PrimeFunctions.PrimeListHash.Contains(rot))
                    {
                        prime = false;
                        break;
                    }
                }
                if (prime)
                {
                    count++;
                    //Console.WriteLine(i);
                }

            }
            return count;
        }
    }

    class Problem36
    {
        public static int DoubleBasePalindromes(int n)
        {
            int sum = 0;
            for (int i = 1; i < n; i++)
            {
                string binary = Convert.ToString(i, 2);
                if (CombinatoricFunctions.IsPalindrome(i) && CombinatoricFunctions.IsPalindrome(binary))
                {
                    sum += i;
                }
            }
            return sum;
        }
    }

    class Problem37
    {
        public static int truncatableprimes(int n)
        {
            int count = 0;
            int sum = 0;
            PrimeFunctions.GeneratePrimesToList(1000000);
            PrimeFunctions.ConvertToHash();
            int i = 4;
            while (count < n)
            {
                bool isprime = true;
                List<int> trunc = CombinatoricFunctions.truncations(PrimeFunctions.PrimeList[i]);
                foreach (int truncation in trunc)
                {
                    if (!PrimeFunctions.PrimeListHash.Contains(truncation))
                    {
                        isprime = false;
                        break;
                    }

                }
                if (isprime)
                {
                    count++;
                    sum += PrimeFunctions.PrimeList[i];
                }
                i++;
            }
            return sum;
        }
    }

    class Problem38
    {
        public static long PandigitalMultiples(long n)
        {
            long maxpandigital = 1;
            for (int i = 1; i < n; i++)
            {

                long mult = i;
                int k = 2;
                while (mult < 1000000000)
                {
                    mult = CombinatoricFunctions.concatenatenum(mult, k * i);
                    if (mult > 100000000 && mult < 1000000000)
                    {
                        if (MiscFunctions.IsPandigital((int)mult))
                        {
                            if (mult > maxpandigital)
                            {
                                maxpandigital = mult;
                            }
                        }
                    }
                    k++;
                }
            }
            return maxpandigital;
        }
    }

    class Problem39
    {
        public static int IntegerRightTriangles(int n)
        {
            List<List<int>> righttriangles = SpecialSequences.GenerateTriangles(n);
            List<int> perimeters = new List<int>();

            foreach (List<int> triangle in righttriangles)
            {
                if (triangle.Sum() <= 1000)
                {
                    perimeters.Add(triangle.Sum());
                }
            }
            var most = perimeters.GroupBy(i => i).OrderByDescending(grp => grp.Count())
      .Select(grp => grp.Key).First();

            return most;
        }
    }

    class Problem40
    {
        public static int ChampernowneConstant(int n)
        {
            int mult = 1;
            StringBuilder t = new StringBuilder();
            for (int i = 1; t.Length < n; i++)
            {
                t.Append(i.ToString());
            }
            for (int i = 1; i <= n; i *= 10)
            {
                mult *= Int32.Parse(t[i - 1].ToString());
            }
            return mult;
        }
    }
}
