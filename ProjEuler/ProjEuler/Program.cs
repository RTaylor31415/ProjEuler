using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections;

namespace ProjEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine(Problem4.LargestPalindromeProduct(1000));
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            Console.ReadLine();
        }

    }
    //All my Prime Functions
    class PrimeFunctions
    {
        public static List<int> PrimeList = new List<int>();
        public static List<int> PrimeFactor(long n)
        {
            PrimeList = GeneratePrimesSieveOfEratosthenes((int)Math.Sqrt((double)n));
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

        public static List<int> GeneratePrimesSieveOfEratosthenes(int n)
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
    }
    class CombinatoricFunctions
    {
        public static bool IsPalindrome(long l)
        {
            IEnumerable<char> forwards = l.ToString().ToCharArray();
            return forwards.SequenceEqual(forwards.Reverse());
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
            //Fibbonacci sequence, untill n, each number is equal to the last two combined.
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
            //Loop through every combination of numbers untill n
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
}

