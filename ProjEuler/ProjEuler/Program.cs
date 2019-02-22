using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections;
using System.Numerics;

namespace ProjEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine(Problem16.PowerDigitSum(2,10000)); 
            Console.WriteLine();
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            Console.ReadLine();
        }

    }


    //LargestProductInAGrid
    //Console.WriteLine(Problem11.LargestProductInAGrid("Problem11",4));
    class Problem11
    {
        public static int LargestProductInAGrid(string n, int m)
        {
            int mult = 1;
            string[] strnums = FileFunctions.readfileintostringarr(n);
            int[][] nums = FileFunctions.stringtointarraySplit2d(strnums, ' ');
            for(int i=0;i<nums.Count();i++)
            {
                for(int j=0;j<nums[0].Count();j++)
                {
                    if(i<nums.Count()-m)
                    {
                        int tempmult = 1;
                        for(int k=0;k<m;k++)
                        {
                            tempmult *= nums[i + k][j];
                        }
                        if(tempmult>mult)
                        {
                            mult = tempmult;
                        }
                    }
                    if(j<nums.Count()-m)
                    {
                        int tempmult = 1;
                        for (int k = 0; k < m; k++)
                        {
                            tempmult *= nums[i][j+k];
                        }
                        if (tempmult > mult)
                        {
                            mult = tempmult;
                        }
                    }
                    if (j < nums.Count() - m&&i<nums.Count()-m)
                    {
                        int tempmult = 1;
                        for (int k = 0; k < m; k++)
                        {
                            tempmult *= nums[i+k][j + k];
                        }
                        if (tempmult > mult)
                        {
                            mult = tempmult;
                        }
                    }
                    if (j >  m && i < nums.Count() - m)
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
            List<int> triangles = SpecialSequences.triangularnumbers(n * n);
            foreach(int triangle in triangles)
            {
                if(MiscFunctions.Divisors(triangle).Count()>n)
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
        
        public static int availablepaths(int a, int b,int c)
        {
            int sum=0;
            if(a<c)
            {
                sum += availablepaths(a + 1, b, c);
            }
            if(b<c)
            {
                sum += availablepaths(a, b + 1, c);
            }
            if(a==c&&b==c)
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
    class Problem16
    {
        public static int PowerDigitSum(int n, int k)
        {
            BigInteger pow = BigInteger.Pow(n, k);
            return pow.ToString().Sum(c => int.Parse(new String(new char[] { c })));

        }
    }
    
    
    //All my Prime Functions
    class PrimeFunctions
    {
        public static List<int> PrimeList = new List<int>();
        public static int NthPrime(int n)
        {
            GeneratePrimesToList(n+1);
            return PrimeList[n - 1];
        }
        public static List<int> PrimeFactor(long n, bool t)
        {
            if(t)
            { 
                PrimeList = GeneratePrimes((int)Math.Sqrt((double)n));
            }
            List<int> factors = new List<int>();
            while (n > 1)
            {
                for (int i = 0; i < PrimeList.Count(); i++)
                {
                    if (n % PrimeList[i] == 0)
                    {
                        factors.Add(PrimeList[i]);
                        n /= PrimeList[i];
                        break;
                    }
                }
            }
            return factors;
        }
        public static List<int> PrimeFactor(long n)
        {
            
            List<int> factors = new List<int>();
            while (n > 1)
            {
                for (int i = 0; i < PrimeList.Count(); i++)
                {
                    if (n % PrimeList[i] == 0)
                    {
                        factors.Add(PrimeList[i]);
                        n /= PrimeList[i];
                        break;
                    }
                }
            }
            return factors;
        }
        public static int ApproximateNthPrime(int nn)
        {
            double n = (double)nn;
            double p;
            if (nn >= 7022)
            {
                p = n * Math.Log(n) + n * (Math.Log(Math.Log(n)) - 0.9385);
            }
            else if (nn >= 6)
            {
                p = n * Math.Log(n) + n * Math.Log(Math.Log(n));
            }
            else if (nn > 0)
            {
                p = new int[] { 2, 3, 5, 7, 11 }[nn - 1];
            }
            else
            {
                p = 0;
            }
            return (int)p;
        }

        // Find all primes up to and including the limit
        public static BitArray SieveOfEratosthenes(int limit)
        {
            BitArray bits = new BitArray(limit + 1, true);
            bits[0] = false;
            bits[1] = false;
            for (int i = 0; i * i <= limit; i++)
            {
                if (bits[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        bits[j] = false;
                    }
                }
            }
            return bits;
        }
        public static void GeneratePrimesToList(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfEratosthenes(limit);
            for (int i = 0, found = 0; i < limit && found < n; i++)
            {
                if (bits[i])
                {
                    PrimeList.Add(i);
                    found++;
                }
            }
        }
        public static List<int> GeneratePrimes(int n)
        {
            int limit = ApproximateNthPrime(n);
            BitArray bits = SieveOfEratosthenes(limit);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0; i < limit && found < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(i);
                    found++;
                }
            }
            return primes;
        }
        public static List<int> GeneratePrimesTillN(int n)
        {
            BitArray bits = SieveOfEratosthenes(n+1);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0;i < n; i++)
            {
                if (bits[i])
                {
                    primes.Add(i);
                    found++;
                }
            }
            return primes;
        }
        public static void GeneratePrimesTillNToList(int n)
        {
            BitArray bits = SieveOfEratosthenes(n + 1);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0;i< n; i++)
            {
                if (bits[i])
                {
                    PrimeList.Add(i);
                    found++;
                }
            }
        }
    }

    //Combinatoric functions
    class CombinatoricFunctions
    {
        public static bool IsPalindrome(long l)
        {
            IEnumerable<char> forwards = l.ToString().ToCharArray();
            return forwards.SequenceEqual(forwards.Reverse());
        }
        public static BigInteger factorial(int n)
        {
            BigInteger result=1;
            for (int i = 1; i <= n; i++)
            {
                result = result * i;
            }
            return result;
        }
        public static BigInteger binomialcoeff(int n, int k)
        {
            BigInteger top = factorial(n);
            BigInteger bottom = factorial(n - k) * factorial(k);
            return top / bottom;
        }
    }
    //Uncatagorized useful functions
    class SpecialSequences
    {

        public static Dictionary<long, int> CollatzSequences = new Dictionary<long, int>();
        public static List<int> triangularnumbers(int n)
        {
            List<int> triangles = new List<int>();

            triangles.Add(1);
            for (int i=2;i<n;i++)
            {
                triangles.Add(i+triangles[i-2]);
                
            }
            return triangles;
        }
        public static void generatecollatz(int n)
        {
            for(int i=2;i<n;i++)
            {
                long temp = i;
                int count = 1;
                while(temp>1)
                {
                    if (CollatzSequences.ContainsKey(temp))
                    {
                        count += CollatzSequences[temp]-1;
                        temp = 1;
                    }
                    else
                    {
                        if(temp%2==0)
                        {
                            temp /= 2;
                        }
                        else
                        {
                            temp *= 3;
                            temp++;
                        }
                        count++;
                    }
                }

                CollatzSequences.Add(i, count);
            }
        }
    }
    class MiscFunctions
    {
        public static int ReturnDistinctCountList(List<int> list, int a)
        {
            return list.Where(x => x.Equals(a)).Count();
        }
        public static long AddFrom1toN(long n)
        {
            return ((n * (n + 1)) / 2);
        }
        public static List<int> Divisors(int n)
        {
            List<int> divisors = new List<int>();
            // Note that this loop runs  
            // till square root 
            for (int i = 1; i <= Math.Sqrt(n);i++)
            {
                if (n % i == 0)
                {

                    // If divisors are equal, 
                    if (n / i == i)
                    { 
                        divisors.Add(i);
                    }
                    else
                    {
                        divisors.Add(i);
                        divisors.Add(n / i);
                    }
                }
            }
            return divisors;
        }
    }
    class FileFunctions
    { 
        public static string readfileintostring(string n)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Ryan\source\repos\ProjEuler\ProjEuler\ProjEuler\bin\" + n+ ".txt");
            return text;
        }
        public static string[] readfileintostringarr(string n)
        {
            string[] text = System.IO.File.ReadAllLines(@"C:\Users\Ryan\source\repos\ProjEuler\ProjEuler\ProjEuler\bin\" + n + ".txt");
            return text;
        }
        public static int[] stringtointarray(string n)
        {
            char[] temp = n.ToCharArray();
            int[] numbers = Array.ConvertAll(temp, c => (int)Char.GetNumericValue(c));
            int numToRemove = -1;
            numbers = numbers.Where(val => val != numToRemove).ToArray();
            return numbers;
        }
        public static int[] stringtointarraySplit(string n, char p)
        {
            string[] strings = n.Split(' ');
            return Array.ConvertAll(strings, int.Parse);
        }
        public static List<BigInteger> stringarrtobigint(string[] arr)
        {
            List<BigInteger> big = new List<BigInteger>();
            foreach(string bleh in arr)
            {
                big.Add(BigInteger.Parse(bleh));
            }
            return big;
        }
        public static int[][] stringtointarraySplit2d(string[] n, char p)
        {
            string[] t = n[0].Split(' ');
            int [][] arr = new int[n.Count()][];
            for(int i=0;i<n.Count();i++)
            {
                string[] temp = n[i].Split(' ');
                arr[i] = Array.ConvertAll(temp, int.Parse);
            }
            return arr;

        }
    }

}

