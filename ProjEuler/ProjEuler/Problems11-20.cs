using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjEuler
{
    //LargestProductInAGrid
    //Console.WriteLine(Problem11.LargestProductInAGrid("Problem11",4));
    class Problem11
    {
        public static int LargestProductInAGrid(string n, int m)
        {
            int mult = 1;
            string[] strnums = FileFunctions.readfileintostringarr(n);
            int[][] nums = FileFunctions.stringtointarraySplit2d(strnums, ' ');
            for (int i = 0; i < nums.Count(); i++)
            {
                for (int j = 0; j < nums[0].Count(); j++)
                {
                    if (i < nums.Count() - m)
                    {
                        int tempmult = 1;
                        for (int k = 0; k < m; k++)
                        {
                            tempmult *= nums[i + k][j];
                        }
                        if (tempmult > mult)
                        {
                            mult = tempmult;
                        }
                    }
                    if (j < nums.Count() - m)
                    {
                        int tempmult = 1;
                        for (int k = 0; k < m; k++)
                        {
                            tempmult *= nums[i][j + k];
                        }
                        if (tempmult > mult)
                        {
                            mult = tempmult;
                        }
                    }
                    if (j < nums.Count() - m && i < nums.Count() - m)
                    {
                        int tempmult = 1;
                        for (int k = 0; k < m; k++)
                        {
                            tempmult *= nums[i + k][j + k];
                        }
                        if (tempmult > mult)
                        {
                            mult = tempmult;
                        }
                    }
                    if (j > m && i < nums.Count() - m)
                    {
                        int tempmult = 1;
                        for (int k = 0; k < m; k++)
                        {
                            tempmult *= nums[i + k][j - k];
                        }
                        if (tempmult > mult)
                        {
                            mult = tempmult;
                        }
                    }

                }
            }
            return mult;
        }
    }
    //Highly Divisible Triangular Number
    //Console.WriteLine(Problem12.HighlyDivisibleTriangularNumber(500));
    class Problem12
    {
        public static int HighlyDivisibleTriangularNumber(int n)
        {
            List<long> triangles = SpecialSequences.triangularnumbers(n * n);
            foreach (int triangle in triangles)
            {
                if (MiscFunctions.Divisors(triangle).Count() > n)
                {
                    return triangle;
                }
            }
            return 0;
        }
    }
    // Large Sum
    // Console.WriteLine(Problem13.LargeSum("Problem13"));
    class Problem13
    {
        public static string LargeSum(string n)
        {
            string[] nums = FileFunctions.readfileintostringarr(n);
            List<BigInteger> bigs = FileFunctions.stringarrtobigint(nums);
            return bigs.Aggregate(BigInteger.Add).ToString();
        }
    }
    //Collatz sequence
    //Console.WriteLine(Problem14.LongestCollatzSequence(1000000));
    class Problem14
    {
        public static long LongestCollatzSequence(int n)
        {
            SpecialSequences.generatecollatz(n);
            int max = SpecialSequences.CollatzSequences.Values.Max();
            return SpecialSequences.CollatzSequences.FirstOrDefault(x => x.Value == max).Key;
        }
    }
    // Lattice Paths
    // Console.WriteLine(Problem15.LatticePathsFast(20));
    class Problem15
    {
        public static int LatticePaths(int n)
        {
            return availablepaths(0, 0, n);
        }

        public static int availablepaths(int a, int b, int c)
        {
            int sum = 0;
            if (a < c)
            {
                sum += availablepaths(a + 1, b, c);
            }
            if (b < c)
            {
                sum += availablepaths(a, b + 1, c);
            }
            if (a == c && b == c)
            {
                return 1;
            }
            return sum;
        }
        public static string LatticePathsFast(int n)
        {
            return CombinatoricFunctions.binomialcoeff(2 * n, n).ToString();
        }
    }
    // Power Digit Sum
    // Console.WriteLine(Problem16.PowerDigitSum(2,10000)); 
    class Problem16
    {
        public static int PowerDigitSum(int n, int k)
        {
            BigInteger pow = BigInteger.Pow(n, k);
            return pow.ToString().Sum(c => int.Parse(new String(new char[] { c })));

        }
    }
    //Number Letter Counts
    //Console.WriteLine(Problem17.NumberLetterCounts(1000)); 
    class Problem17
    {
        public static int[] onethroughtwenty = new int[20] { 4, 3, 3, 5, 4, 4, 3, 5, 5, 4, 3, 6, 6, 8, 8, 7, 7, 9, 8, 8 };
        public static int[] tens = new int[10] { 4, 3, 6, 6, 5, 5, 5, 7, 6, 6 };
        public static int lettercount(int n)
        {
            int letters = 0;
            if (n == 1000)
            {
                return 11;
            }
            if (n >= 100)
            {
                int hundred = n / 100;
                letters += onethroughtwenty[hundred];
                letters += 10;
            }
            if (n % 100 == 0)
            {
                letters -= 3;
            }
            if (n % 100 >= 20)
            {
                int tensplace = (n % 100) / 10;
                letters += tens[tensplace];
                if (n % 10 >= 1)
                {
                    letters += onethroughtwenty[n % 10];
                }
            }
            else if (n % 100 >= 1)
            {
                letters += onethroughtwenty[n % 100];
            }

            return letters;
        }
        public static int NumberLetterCounts(int n)
        {
            int sum = 0;
            for (int i = 1; i <= n; i++)
            {
                sum += lettercount(i);
            }
            return sum;
        }

    }
    // Maximum Path Sum 1
    // Console.WriteLine(Problem18.MaximumPathSum1("Problem18")); 
    class Problem18
    {
        public static int MaximumPathSum1(string n)
        {
            string[] temp = FileFunctions.readfileintostringarr(n);
            int[][] tri = FileFunctions.stringtointarraySplit2d(temp, ' ');
            for (int j = tri.GetLength(0) - 2; j >= 0; j--)
            {
                for (int i = 0; i < tri[j].Length; i++)
                {
                    tri[j][i] += Math.Max(tri[j + 1][i], tri[j + 1][i + 1]);
                }
            }
            return tri[0][0];
        }
    }
    //Counting Sundays
    //Console.WriteLine(Problem19.CountingSundays());
    class Problem19
    {
        public static int CountingSundays()
        {
            int sum = 0;
            DateTime date1 = new DateTime(1901, 1, 1);

            DateTime date2 = new DateTime(2000, 12, 30);
            while (date1 != date2)
            {
                if (date1.DayOfWeek == DayOfWeek.Sunday && date1.Day == 1)
                {
                    sum++;
                }
                date1 = date1.AddDays(1);
            }
            return sum;
        }
    }
    //Factorial Digit Sum
    //Console.WriteLine(Problem20.FactorialDigitSum(100));
    class Problem20
    {
        public static int FactorialDigitSum(int n)
        {
            return MiscFunctions.SumOfDigits(CombinatoricFunctions.factorial(n));
        }
    }
}
