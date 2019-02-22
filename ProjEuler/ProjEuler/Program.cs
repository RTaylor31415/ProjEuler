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

            Console.WriteLine(Problem16.PowerDigitSum(2,1000));
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            Console.ReadLine();
        }

    }
    //Multiples of 3 and 5
    //Console.WriteLine(Problem1.SumOfMult(3,5,1000));
    class Problem1
    {
        public static int SumOfMult(int a, int b, int c)
        {
            int sum = 0;
            for (int i = 1; i < c; i++)
            {
                //If a number is divisible by a or b add it to the sum
                if (i % a == 0 || i % b == 0)
                {
                    sum += i;
                }
            }
            return sum;
        }
    }
    //Even Fibonnaci Numbers
    //Console.WriteLine(Problem2.EvenFibonacciNumbers(4000000));
    class Problem2
    {
        public static List<int> FibbonacciTerms = new List<int>();
        public static int EvenFibonacciNumbers(int n)
        {
            int sum = 0;
            GenerateFibbonacciNumbers(n);
            //Every third number is even starting on the second number, loop through these and add them to sum
            for (int i = 1; i < FibbonacciTerms.Count; i += 3)
            {
                sum += FibbonacciTerms[i];
            }
            return sum;
        }
        public static void GenerateFibbonacciNumbers(int n)
        {
            int a = 1;
            int b = 1;
            FibbonacciTerms.Add(b);
            //Fibbonacci sequence, until n, each number is equal to the last two combined.
            for (int c = 2; c < n; c = a + b)
            {
                a = b;
                b = c;
                FibbonacciTerms.Add(c);
            }
        }
    }
    //Largest Prime Factor
    //Console.WriteLine(Problem3.LargestPrimeFactor(600851475143L));
    class Problem3
    {
        public static int LargestPrimeFactor(long n)
        {
            //Return the largest number in the list of prime factors
            return PrimeFunctions.PrimeFactor(n).Max();
        }
    }
    //Largest Palindrome Product
    //Console.WriteLine(Problem4.LargestPalindromeProduct(1000));
    class Problem4
    {
        public static int LargestPalindromeProduct(int n)
        {
            int largest = 0;
            //Loop through every combination of numbers until n
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //If i*j is a palindrome then check if it's largest then the largest so far and set it to an int
                    if (CombinatoricFunctions.IsPalindrome(i * j))
                    {
                        if ((i * j) > largest)
                        {
                            largest = i * j;
                        }
                    }
                }
            }
            return largest;
        }
    }
    // Smallest Multiple
    //Console.WriteLine(Problem5.SmallestMultiple(20));
    class Problem5
    {
        public static int SmallestMultiple(int n)
        {
            PrimeFunctions.GeneratePrimesTillNToList(n);
            List<int> FactorList = new List<int>();
            //Loop until highest multiple
            for (int i = 2; i <= n; i++)
            {
                //Find all the factors of the number
                List<int> factors = PrimeFunctions.PrimeFactor(i, false);
                //If the list of factors doesn't contain the factor or as many factors, then add it to the list
                foreach (int factor in factors)
                {
                    if (MiscFunctions.ReturnDistinctCountList(factors, factor) > MiscFunctions.ReturnDistinctCountList(FactorList, factor))
                    {
                        FactorList.Add(factor);
                    }
                }

            }
            //Loop through the minimum required factors and multiply them all
            int mult = 1;
            foreach (int factor in FactorList)
            {
                mult *= factor;
            }
            return mult;
        }
    }
    //SumSquareDifference
    //Console.WriteLine(Problem6.SumSquareDifference(100));
    class Problem6
    {
        public static long SumSquareDifference(int n)
        {
            long sum = MiscFunctions.AddFrom1toN(n);
            long squaresum = sum * sum;

            long sumsquare = 0;
            for (int i = 1; i <= n; i++)
            {
                sumsquare += (i * i);
            }
            return squaresum - sumsquare;
        }
    }
    //10001st Prime
    //Console.WriteLine(Problem7.TenthousandstPrime(10001));
    class Problem7
    {
        public static int TenthousandstPrime(int n)
        {
            return PrimeFunctions.NthPrime(n);
        }
    }
    //LargestProductInASeries
    //Console.WriteLine(Problem8.LargestProductInASeries("Problem8",13));
    class Problem8
    {
        public static long LargestProductInASeries(string n, int m)
        {
            long maxmult = 1;
            long mult = 1;
            string file = FileFunctions.readfileintostring("Problem8");
            int[] filenums = FileFunctions.stringtointarray(file);
            for (int i = 0; i < filenums.Length - m; i++)
            {
                mult = 1;
                for (int j = i; j < i + m; j++)
                {
                    mult *= filenums[j];
                }
                if (mult > maxmult)
                {
                    maxmult = mult;
                }
            }

            return maxmult;
        }
    }
    //SpecialPythagoreanTriplet
    //Console.WriteLine(Problem9.SpecialPythagoreanTriplet(1000));
    class Problem9
    {
        public static int SpecialPythagoreanTriplet(int n)
        {
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    double k = Math.Sqrt((i * i) + (j * j));
                    if (Math.Abs(k - (int)k) < double.Epsilon)
                    {
                        if (i + j + (int)k == 1000)
                        {
                            return (i * j * (int)k);
                        }
                    }
                }
            }
            return 0;
        }
        public bool IsTriplet(int a, int b, int c)
        {
            return ((a * a) + (b * b) == (c * c));
        }
    }
    //Summation of Primes
    //Console.WriteLine(Problem10.SummationOfPrimes(2000000));
    class Problem10
    {
        public static long SummationOfPrimes(int n)
        {
            List<int> primes = PrimeFunctions.GeneratePrimesTillN(2 * n);
            long k = 0;
            for (int i = 0; primes[i] < n; i++)
            {
                k += primes[i];
            }
            return k;
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

