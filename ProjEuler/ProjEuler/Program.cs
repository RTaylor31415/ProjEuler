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
            Console.WriteLine(Problem25.fibbonacciNumber(1000)); 
            Console.WriteLine();
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            Console.ReadLine();
        }

    }


    
    // Amicable Numbers
    //Console.WriteLine(Problem21.AmicableNumbers(10000)); 
    class Problem21
    {
        public static int AmicableNumbers(int n)
        {
            int sum = 0;
            for(int i=1;i<n;i++)
            {
                if(Amicability(i))
                {
                    sum += i;
                }
            }
            return sum;
        }
        public static bool Amicability(int n)
        {
            int a = MiscFunctions.ProperDivisors(n).Sum() ;
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
            for (int i=0;i<namessplit.Length;i++)
            {
                int tempsum = 0;
                for(int j=1;j<namessplit[i].Length-1;j++)
                {
                    string m = namessplit[i];
                    char k = m[j];
                    tempsum += namessplit[i][j]-64;
                }
                sum += (tempsum*(i+1));
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
            for(int i=1;i<n;i++)
            {
                bool notfound = true;
                foreach(int k in abundantnums)
                {
                    if(abundantnums.Contains(i - k))
                    {
                        notfound = false;
                        break;
                    }
                }
                if(notfound)
                {
                    sum += i;
                }
            }
            return sum;
        }
        public static void GenerateAbundantNums(int n)
        {
            for(int i=1;i<n;i++)
            {
                if(Abundant(i))
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
        public static string LexicographicPermutations(int n,int k)
        {
            List<int> nums= new List<int>();
            for(int i=0;i<=n;i++)
            {
                nums.Add(i);
            }
            //IEnumerable<IEnumerable<int>> numper = CombinatoricFunctions.GetPermutations(nums, n+1);
            IEnumerable<IEnumerable<int>> numper = SomeExtensions.GetPermutations(nums);
            List<List<int>> list = numper.Select(c => c.ToList()).ToList();


            return MiscFunctions.ListToString(list[k-1]);

        }
    }
    class Problem25
    {
        public static int fibbonacciNumber(int n)
        {
            SpecialSequences.GenerateFibbonacciDigits(n);
            return SpecialSequences.fibbonaccisequence.Count+1;
        }
    }
    class Problem26
    {

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
    public static class SomeExtensions
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            var factorials = Enumerable.Range(0, array.Length + 1)
                .Select(Factorial)
                .ToArray();

            for (var i = 0L; i < factorials[array.Length]; i++)
            {
                var sequence = GenerateSequence(i, array.Length - 1, factorials);

                yield return GeneratePermutation(array, sequence);
            }
        }

        private static IEnumerable<T> GeneratePermutation<T>(T[] array, IReadOnlyList<int> sequence)
        {
            var clone = (T[])array.Clone();

            for (int i = 0; i < clone.Length - 1; i++)
            {
                Swap(ref clone[i], ref clone[i + sequence[i]]);
            }

            return clone;
        }

        private static int[] GenerateSequence(long number, int size, IReadOnlyList<long> factorials)
        {
            var sequence = new int[size];

            for (var j = 0; j < sequence.Length; j++)
            {
                var facto = factorials[sequence.Length - j];

                sequence[j] = (int)(number / facto);
                number = (int)(number % facto);
            }

            return sequence;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        private static long Factorial(int n)
        {
            long result = n;

            for (int i = 1; i < n; i++)
            {
                result = result * i;
            }

            return result;
        }
    }
    //Combinatoric functions
    class CombinatoricFunctions
    {
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        public static bool IsPalindrome(long l)
        {
            IEnumerable<char> forwards = l.ToString().ToCharArray();
            return forwards.SequenceEqual(forwards.Reverse());
        }
        public static BigInteger factorial(int n)
        {
            BigInteger result = 1;
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
        public static List<BigInteger> fibbonaccisequence = new List<BigInteger>();
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
        public static void GenerateFibbonacciDigits(int n)
        {
            BigInteger goal = BigInteger.Pow(10,n-1);
            BigInteger nextnumber=1;
            BigInteger thisnumber=1;
            fibbonaccisequence.Add(thisnumber);
            for(BigInteger thirdnumber = 1;thirdnumber<goal;thirdnumber=(nextnumber+thisnumber))
            {
                fibbonaccisequence.Add(thirdnumber);
                thisnumber = nextnumber;
                nextnumber = thirdnumber;
            }
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
        public static string ListToString(List<int> list)
        {
            StringBuilder a = new StringBuilder();
            foreach(int i in list)
            {
                a.Append(i);
            }
            return a.ToString();
        }
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
        public static List<int> ProperDivisors(int n)
        {
            List<int> divisors = Divisors(n);
            
            divisors.Remove(n);
            return divisors;
        }
        public static int SumOfDigits(BigInteger n)
        {
             int[] nums = FileFunctions.stringtointarray(n.ToString());
            return nums.Sum();
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
            string[] strings = n.Split(p);
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

