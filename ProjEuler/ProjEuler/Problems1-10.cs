using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjEuler
{
    class Problems1_10
    {

        //Multiples of 3 and 5
        //Console.WriteLine(Problem1.SumOfMult(3,5,1000));
        public class Problem1
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
        public class Problem2
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
        public class Problem3
        {
            public static int LargestPrimeFactor(long n)
            {
                //Return the largest number in the list of prime factors
                return PrimeFunctions.PrimeFactor(n).Max();
            }
        }
        //Largest Palindrome Product
        //Console.WriteLine(Problem4.LargestPalindromeProduct(1000));
        public class Problem4
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
        public class Problem5
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
        public class Problem6
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
        public class Problem7
        {
            public static int TenthousandstPrime(int n)
            {
                return PrimeFunctions.NthPrime(n);
            }
        }
        //LargestProductInASeries
        //Console.WriteLine(Problem8.LargestProductInASeries("Problem8",13));
        public class Problem8
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
        public class Problem9
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
        public class Problem10
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
    }
}
