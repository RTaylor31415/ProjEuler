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
            Console.WriteLine(Problem47.DistinctPrimeFactors());
            Console.WriteLine();
            stopWatch.Stop();
            Console.WriteLine(stopWatch.Elapsed);
            Console.ReadLine();
        }

    }

    
    class Problem41
    {
        public static int PandigitalPrime(int n)
        {
            List<int> PanPrimes = new List<int>();
            PrimeFunctions.GeneratePrimesTillNToList(n);
            for(int i=0;i<PrimeFunctions.PrimeList.Count;i++)
            {
                if(MiscFunctions.IsPandigital(PrimeFunctions.PrimeList[i]))
                {
                    PanPrimes.Add(PrimeFunctions.PrimeList[i]);
                }
            }
            return PanPrimes.Max();
        }
    }

    class Problem42
    {
        public static int CodedTriangleNumbers(string n)
        {
            int count = 0;
            string str = FileFunctions.readfileintostring("Problem42");
            str = str.Replace("/", "");
            str = str.Replace("\"", "");
            string[] arr = str.Split(',');
            List<long> triangles = SpecialSequences.triangularnumbers(1000);
            foreach(string word in arr)
            {
                if(triangles.Contains(MiscFunctions.UppercaseWordValue(word)))
                {
                    count++;
                }
            }
            return count;
        }
    }

    class Problem43
    {
        public static List<int> nums = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static List<int> primes = PrimeFunctions.GeneratePrimesTillN(50);
        public static long SubstringDivisibility()
        {
            long sum = 0;
            
            var perm = SomeExtensions.GetPermutations(nums);
            List<List<int>> list = FileFunctions.ienumtolist(perm);
            foreach(List<int> num in list)
            {
                if(SubStringTest(num))
                {
                    sum += MiscFunctions.ListToLong(num);
                }
            }
            return sum;
        }
        public static bool SubStringTest(List<int> num)
        {
            if (num[0] == 0)
            {
                return false;
            }
            for (int i = 3; i < num.Count; i++)
            {
                int temp = (num[i - 2] * 100) + (num[i - 1] * 10) + num[i];
                if (temp % primes[i - 3] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    //Console.WriteLine(Problem44.PentagonNumbers(10000));
    class Problem44
    {
        public static int PentagonNumbers(int n)
        {
            int min = int.MaxValue;
            List<long> pentagons = SpecialSequences.pentagonalnumbers(n);
            HashSet<long> hashpent =new HashSet<long>(pentagons);
            foreach(int k in pentagons)
            {
                foreach(int j in pentagons)
                {
                    int tempsum = k + j;
                    if(!hashpent.Contains(tempsum))
                    {
                        continue;
                    }
                    int tempmin = k - j;
                    if(!hashpent.Contains(tempmin))
                    {
                        continue;
                    }
                    if(tempmin<min)
                    {
                        min = tempmin;
                    }
                }
            }
            return min;
        }
    }

    class Problem45
    {
        public static long TriangularPentagonalHexagonal(int n)
        {
            List<long> pentagon = SpecialSequences.pentagonalnumbers(n);
            List<long> hexagon = SpecialSequences.hexagonalnumbers(n);
            HashSet<long> penthash = new HashSet<long>(pentagon);
            for(int i=143;i<hexagon.Count;i++)
            {
                if (penthash.Contains(hexagon[i]))
                {
                    return hexagon[i] ;
                }
            }
            return 0;
        }
    }

    class Problem46
    {
        public static int OddComposite()
        {
            int comp = 0;
            List<int> primes = PrimeFunctions.GeneratePrimes(10000);
            HashSet<int> primeshash = MiscFunctions.ListToHash(primes);
            List<int> squares = SpecialSequences.squares(1000);

            HashSet<int> squareshash = MiscFunctions.ListToHash(squares);
            int i = 3;
            bool found = false;
            while(comp==0)
            {
                found = false;
                i += 2;
                if (primeshash.Contains(i))
                {
                    continue;
                }
                for(int j=0;j<primes.Count;j++)
                {
                    if(primes[j]>i)
                    {
                        break;
                    }
                    for(int k=0;k<squares.Count;k++)
                    {
                        if(squares[k]*2>i)
                        {
                            break;
                        }
                        if(primes[j]+(squares[k]*2)==i)
                        {
                            found = true;
                        }
                    }
                }
                if (!found)
                {
                    comp = i;
                }
            }
            return comp;
        }
    }

    class Problem47
    {
        public static int DistinctPrimeFactors()
        {
            PrimeFunctions.GeneratePrimesToList(100000);
            int i = 2;
            int count = 0;
            while (count < 4)
            {
                i++;
                List<int> primes = PrimeFunctions.PrimeFactor(i);
                HashSet<int> factors = MiscFunctions.ListToHash(primes);
                if(factors.Count==4)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }
            }
            return i - 3;
        }
    }

    //All my Prime Functions
    class PrimeFunctions
    {
        public static List<int> PrimeList = new List<int>();
        public static HashSet<int> PrimeListHash = new HashSet<int>();
        public static int NthPrime(int n)
        {
            GeneratePrimesToList(n + 1);
            return PrimeList[n - 1];
        }
        public static void ConvertToHash()
        {
            PrimeListHash = new HashSet<int>(PrimeList);
        }
        public static List<int> PrimeFactor(long n, bool t)
        {
            if (t)
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
            BitArray bits = SieveOfEratosthenes(n + 1);
            List<int> primes = new List<int>();
            for (int i = 0, found = 0; i < n; i++)
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
            for (int i = 0, found = 0; i < n; i++)
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
        public static long concatenatenum(long a,long b)
        {
            string astr = a.ToString();
            string bstr = b.ToString();
            return Int64.Parse( String.Concat(astr,bstr));
        }
        public static List<int> truncations (int n)
        {
            List<int> trunc = new List<int>();
            string t = n.ToString();
            for(int i=0;i<t.Length;i++)
            {
                trunc.Add(Int32.Parse((t.Substring(t.Length - i - 1, i + 1))));
                trunc.Add(Int32.Parse((t.Substring(0, i + 1))));
            }
            return trunc;
        }
        public static List<BigInteger> facts = new List<BigInteger>();
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        public static List<int> rotations(int n)
        {
            List<int> rots = new List<int>();
            List<int> startingint = MiscFunctions.DigitsFromInt(n).ToList();
            rots.Add(n);
            for(int i=1;i<startingint.Count;i++)
            {
                startingint.Add(startingint[0]);
                startingint.RemoveAt(0);
                rots.Add(MiscFunctions.ListToInt(startingint));
            }
            return rots;
        }
        public static bool IsPalindrome(long l)
        {
            IEnumerable<char> forwards = l.ToString().ToCharArray();
            return forwards.SequenceEqual(forwards.Reverse());
        }
        public static bool IsPalindrome(string l)
        {
            return l.SequenceEqual(l.Reverse());
        }

        public static void generatefactorial(int n)
        {
            facts.Add(1);
            BigInteger result = 1;
            for (int i = 1; i <= n; i++)
            {
                result = result * i;
                facts.Add(result);
            }
        }
        public static BigInteger factorial(int n)
        {
            if(n<facts.Count)
            {
                return facts[n];
            }
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
        // Enumerate all possible m-size combinations of [0, 1, ..., n-1] array
        // in lexicographic order (first [0, 1, 2, ..., m-1]).
        private static IEnumerable<int[]> CombinationsRosettaWoRecursion(int m, int n)
        {
            int[] result = new int[m];
            Stack<int> stack = new Stack<int>(m);
            stack.Push(0);
            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();
                while (value < n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index != m) continue;
                    yield return (int[])result.Clone(); // thanks to @xanatos
                                                        //yield return result;
                    break;
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Subsets<T>(IEnumerable<T> source)
        {
            List<T> list = source.ToList();
            int length = list.Count;
            int max = (int)Math.Pow(2, list.Count);

            for (int count = 0; count < max; count++)
            {
                List<T> subset = new List<T>();
                uint rs = 0;
                while (rs < length)
                {
                    if ((count & (1u << (int)rs)) > 0)
                    {
                        subset.Add(list[(int)rs]);
                    }
                    rs++;
                }
                yield return subset;
            }
        }
        public static IEnumerable<T[]> CombinationsRosettaWoRecursion<T>(T[] array, int m)
        {
            if (array.Length < m)
                throw new ArgumentException("Array length can't be less than number of selected elements");
            if (m < 1)
                throw new ArgumentException("Number of selected elements can't be less than 1");
            T[] result = new T[m];
            foreach (int[] j in CombinationsRosettaWoRecursion(m, array.Length))
            {
                for (int i = 0; i < m; i++)
                {
                    result[i] = array[j[i]];
                }
                yield return result;
            }
        }
    }

    //Uncatagorized useful functions
    class SpecialSequences
    {
        public static Dictionary<long, int> CollatzSequences = new Dictionary<long, int>();
        public static List<BigInteger> fibbonaccisequence = new List<BigInteger>();


        public static List<List<int>> GenerateTriangles(int n)
        {
            List<int> squares = new List<int>();
            List<List<int>> triangles = new List<List<int>>();
            for(int i=1;i<=n;i++)
            {
                squares.Add(i * i);
            }
            for(int i=0;i<n;i++)
            {

                for(int j=0;j<n;j++)
                {
                    if (squares.Contains(squares[i] - squares[j]))
                    {
                        List<int> triple = new List<int>();
                        triple.Add((int)Math.Sqrt(squares[i]));
                        triple.Add((int)Math.Sqrt(squares[j]));
                        triple.Add((int)Math.Sqrt(squares[i] - squares[j]));

                        triple.Sort();
                        triangles.Add(triple);
                    }
                }
            }

            var finalList = MiscFunctions.removedupes(triangles);
            return finalList;
        }
        public static List<int> SpiralNumbers(int n)
        {

            List<int> spirals = new List<int>();
            spirals.Add(1);
            for (int i = 2; i < n + 1; i++)
            {
                spirals.Add(4 * ((4 * i * i) - (7 * i) + 4));
            }
            return spirals;
        }

        public static List<long> triangularnumbers(long n)
        {
            List<long> triangles = new List<long>();
            for (long i = 1; i < n; i++)
            {
                triangles.Add((i *(i+1))/2);
            }
            return triangles;
        }
        public static List<long> pentagonalnumbers(long n)
        {
            List<long> numbers = new List<long>();
            for (long i = 1; i < n; i++)
            {
                numbers.Add((i*(3*i-1))/2);
            }
            return numbers;
        }
        public static List<long> hexagonalnumbers(long n)
        {
            List<long> numbers = new List<long>();
            for (long i = 1; i < n; i++)
            {
                numbers.Add((i * ((2 * i) - 1)));
            }
            return numbers;
        }
        public static List<int> squares(int n)
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i < n; i++)
            {
                numbers.Add(i*i);
            }
            return numbers;
        }
        public static void GenerateFibbonacciDigits(int n)
        {
            BigInteger goal = BigInteger.Pow(10, n - 1);
            BigInteger nextnumber = 1;
            BigInteger thisnumber = 1;
            fibbonaccisequence.Add(thisnumber);
            for (BigInteger thirdnumber = 1; thirdnumber < goal; thirdnumber = (nextnumber + thisnumber))
            {
                fibbonaccisequence.Add(thirdnumber);
                thisnumber = nextnumber;
                nextnumber = thirdnumber;
            }
        }
        public static void generatecollatz(int n)
        {
            for (int i = 2; i < n; i++)
            {
                long temp = i;
                int count = 1;
                while (temp > 1)
                {
                    if (CollatzSequences.ContainsKey(temp))
                    {
                        count += CollatzSequences[temp] - 1;
                        temp = 1;
                    }
                    else
                    {
                        if (temp % 2 == 0)
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
        public static HashSet<int> ListToHash(List<int> list)
        {
            var hashSet = new HashSet<int>(list);
            return hashSet;
        }
        public static double CancelOutSameDigit(int a,int b)
        {
            List<int> digitsa = MiscFunctions.DigitsFromInt(a).ToList();
            List<int> digitsb = MiscFunctions.DigitsFromInt(b).ToList();
            List<int> intersect = digitsa.Intersect(digitsb).ToList();
            if (intersect.Count == 1)
            {
                digitsa.Remove(intersect[0]);
                digitsb.Remove(intersect[0]);
                
                if(digitsb[0]!=0)
                { 

                    return (double)digitsa[0] / (double)digitsb[0];

                }
            }
            
                return 0;

        }
        public static List<List<int>> removedupes(List<List<int>> list)
        {
            var finalList = list.GroupBy(x => String.Join(",", x))
                         .Select(x => x.First().ToList())
                         .ToList();
            return finalList;
        }

        public static int UppercaseWordValue(string n)
        {
            int sum = 0;
            foreach (char letter in n)
            {
                sum += (letter - '@');
            }
            return sum;
        }
        public static void Simplify(int[] numbers)
        {
            int gcd = GCD(numbers);
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] /= gcd;
        }
        public static int GCD(int a, int b)
        {
            while (b > 0)
            {
                int rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }
        public static int GCD(int[] args)
        {
            // using LINQ:
            return args.Aggregate((gcd, arg) => GCD(gcd, arg));
        }
        public static bool IsPandigital(int n)
        {
            if(n>1000000000)
            {
                return false;
            }
            int[] nums = MiscFunctions.DigitsFromInt(n);
            int[] dig = new int[nums.Length];
            for(int i=0;i<dig.Length;i++)
            {
                dig[i] = i + 1;
            }
            return (nums.OrderBy(x => x).ToArray().SequenceEqual( dig));

        }
        public static bool IsPandigital(List<int> n)
        {
            if(n.Count()!=9)
            {
                return false;
            }
            if(n.Distinct().Count()!=9)
            {
                return false;
            }
            int[] dig = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            return (n.OrderBy(x => x).ToArray().SequenceEqual(dig));

        }
        public static string ListToString(List<int> list)
        {
            StringBuilder a = new StringBuilder();
            foreach (int i in list)
            {
                a.Append(i);
            }
            return a.ToString();
        }
        public static int ListToInt(List<int> list)
        {
            string n = ListToString(list);
            return Int32.Parse(n);
        }
        public static long ListToLong(List<int> list)
        {
            string n = ListToString(list);
            return Int64.Parse(n);
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
            for (int i = 1; i <= Math.Sqrt(n); i++)
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
        public static int[] DigitsFromInt(int n)
        {
            return FileFunctions.stringtointarray(n.ToString());
        }
        public static int SumOfDigits(BigInteger n)
        {
            int[] nums = FileFunctions.stringtointarray(n.ToString());
            return nums.Sum();
        }
    }
    class FileFunctions
    {
        public static List<List<int>> ienumtolist (IEnumerable<IEnumerable<int>> T)
        {
            var list = T.Select(c => c.ToList()).ToList(); //last ToList to get all conversions in a single list
            return list;

        }

        public static string readfileintostring(string n)
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Ryan\source\repos\ProjEuler\ProjEuler\ProjEuler\bin\" + n + ".txt");
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
            foreach (string bleh in arr)
            {
                big.Add(BigInteger.Parse(bleh));
            }
            return big;
        }
        public static int[][] stringtointarraySplit2d(string[] n, char p)
        {
            string[] t = n[0].Split(' ');
            int[][] arr = new int[n.Count()][];
            for (int i = 0; i < n.Count(); i++)
            {
                string[] temp = n[i].Split(' ');
                arr[i] = Array.ConvertAll(temp, int.Parse);
            }
            return arr;

        }
    }

}

