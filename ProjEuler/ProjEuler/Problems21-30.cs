using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjEuler
{
    // Amicable Numbers
    //Console.WriteLine(Problem21.AmicableNumbers(10000)); 
    class Problem21
    {
        public static int AmicableNumbers(int n)
        {
            int sum = 0;
            for (int i = 1; i < n; i++)
            {
                if (Amicability(i))
                {
                    sum += i;
                }
            }
            return sum;
        }
        public static bool Amicability(int n)
        {
            int a = MiscFunctions.ProperDivisors(n).Sum();
            if (a != n)
            {
                return MiscFunctions.ProperDivisors(a).Sum() == n;
            }
            else
            {
                return false;
            }
        }
    }
    //Names Scores
    //Console.WriteLine(Problem22.NamesScores("Problem22")); 
    class Problem22
    {
        public static long NamesScores(string n)
        {
            long sum = 0;
            string names = FileFunctions.readfileintostring(n);
            string[] namessplit = names.Split(',');
            Array.Sort(namessplit, StringComparer.InvariantCulture);
            for (int i = 0; i < namessplit.Length; i++)
            {
                int tempsum = 0;
                for (int j = 1; j < namessplit[i].Length - 1; j++)
                {
                    string m = namessplit[i];
                    char k = m[j];
                    tempsum += namessplit[i][j] - 64;
                }
                sum += (tempsum * (i + 1));
            }
            return sum;
        }
    }
    //Non-Abundant Sums
    //Console.WriteLine(Problem23.NonAbundantSums(28124)) ; 
    class Problem23
    {
        public static HashSet<int> abundantnums = new HashSet<int>();
        public static int NonAbundantSums(int n)
        {
            GenerateAbundantNums(n);
            int sum = 0;
            for (int i = 1; i < n; i++)
            {
                bool notfound = true;
                foreach (int k in abundantnums)
                {
                    if (abundantnums.Contains(i - k))
                    {
                        notfound = false;
                        break;
                    }
                }
                if (notfound)
                {
                    sum += i;
                }
            }
            return sum;
        }
        public static void GenerateAbundantNums(int n)
        {
            for (int i = 1; i < n; i++)
            {
                if (Abundant(i))
                {
                    abundantnums.Add(i);
                }
            }
        }
        public static bool Abundant(int n)
        {
            return (MiscFunctions.ProperDivisors(n).Sum() > n);
        }
    }
    //Lexicographic Permutations
    class Problem24
    {
        public static string LexicographicPermutations(int n, int k)
        {
            List<int> nums = new List<int>();
            for (int i = 0; i <= n; i++)
            {
                nums.Add(i);
            }
            //IEnumerable<IEnumerable<int>> numper = CombinatoricFunctions.GetPermutations(nums, n+1);
            IEnumerable<IEnumerable<int>> numper = SomeExtensions.GetPermutations(nums);
            List<List<int>> list = numper.Select(c => c.ToList()).ToList();


            return MiscFunctions.ListToString(list[k - 1]);

        }
    }
    //Fibonacci Number
    class Problem25
    {
        public static int fibbonacciNumber(int n)
        {
            SpecialSequences.GenerateFibbonacciDigits(n);
            return SpecialSequences.fibbonaccisequence.Count + 1;
        }
    }
    //Reciprocal Cycles
    class Problem26
    {
        public static List<List<int>> digitcycles = new List<List<int>>();
        public static int ReciprocalCycles(int n)
        {
            int max = 0;
            int k = 0;
            for (int i = 2; i < n; i++)
            {
                List<int> cycle = new List<int>();
                bool found = true;

                Decimal p = 1;
                int curr = 0;
                while (found)
                {
                    Math.Floor(p *= 10);
                    cycle.Add(Convert.ToInt32(Math.Floor((p / i))));

                    p = p % i;
                    if (cycle.Count > 11)
                    {
                        if ((cycle[curr] == 0 && cycle[curr - 1] == 0 && cycle[curr - 2] == 0) || (cycle[curr - 2] == cycle[8] && cycle[curr - 1] == cycle[9] && cycle[curr] == cycle[10]))
                        {
                            found = false;
                            digitcycles.Add(cycle);
                        }
                        if (curr > max)
                        {
                            max = curr;
                            k = i;
                        }

                    }

                    curr++;
                }

            }


            return k;
        }
    }
    //Quadratic Primes
    class Problem27
    {
        public static int QuadraticPrimes(int n)
        {
            PrimeFunctions.GeneratePrimesTillNToList(n * n);
            PrimeFunctions.ConvertToHash();
            int maxi = 0;
            int maxj = 0;
            int maxprimes = 0;
            for (int i = (n * -1); i < n; i++)
            {
                for (int j = (n * -1); j < n; j++)
                {
                    if (numquadprimes(i, j) > maxprimes)
                    {
                        maxprimes = numquadprimes(i, j);
                        maxi = i;
                        maxj = j;
                    }
                }
            }
            return (maxi * maxj);
        }
        public static int numquadprimes(int a, int b)
        {
            bool prime = true;
            int n = 0;
            while (prime)
            {
                if (!PrimeFunctions.PrimeListHash.Contains((n * n) + (a * n) + (b)))
                {
                    prime = false;
                }
                n++;
            }
            return n;
        }
    }
    //Number Spiral Diagonals
    class Problem28
    {
        public static int NumberSpiralDiagonals(int n)
        {
            return SpecialSequences.SpiralNumbers((n + 1) / 2).Sum();
        }
    }
    //Distinct Powers
    class Problem29
    {
        public static int DistinctPowers(int n)
        {
            HashSet<BigInteger> distinctterms = new HashSet<BigInteger>();
            for (int i = 2; i <= n; i++)
            {
                for (int j = 2; j <= n; j++)
                {
                    distinctterms.Add(BigInteger.Pow(i, j));
                }
            }
            return distinctterms.Count();
        }
    }
    //Digit Fifth Powers
    class Problem30
    {
        public static int DigitFifthPowers(int n)
        {
            List<int> digits = new List<int>();
            int limit = (int)Math.Pow(10, n) * 100;
            for (int i = 2; i < limit; i++)
            {
                int[] arr = MiscFunctions.DigitsFromInt(i);
                if (PowerSum(arr, n) == i)
                {
                    digits.Add(i);
                }
            }
            return digits.Sum();
        }
        public static int PowerSum(int[] arr, int n)
        {
            int sum = 0;
            foreach (int k in arr)
            {
                sum += (int)Math.Pow(k, n);
            }
            return sum;
        }
    }
}
