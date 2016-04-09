using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack_greedy
{
    public class Attributes
    {
        int greater_weight = 0;
        /// <summary>
        ///  greater weight
        /// </summary>
        public int GreaterWeight
        {
            get { return greater_weight; }
            set { greater_weight = value; }
        }

        int greater_price = 0;
        /// <summary>
        /// greater price
        /// </summary>
        public int GreaterPrice
        {
            get { return greater_price; }
            set { greater_price = value; }
        }

        int seq;
        /// <summary>
        /// list index number of the answer
        /// </summary>
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        int listSize;
        /// <summary>
        /// list size
        /// </summary>
        public int ListSize
        {
            get { return listSize; }
            set { listSize = value; }
        }


        /// <summary>
        /// Quantity of products
        /// </summary>
        public static int productsQuantity;

        /// <summary>
        /// Bag Size
        /// </summary>
        public static int Size;

        /// <summary>
        /// Vector of weights who was readed from the archive
        /// </summary>
        public static int[] Weight;

        /// <summary>
        /// Vector of prices who was readed from the archive
        /// </summary>
        public static int[] Price;

        public void printValues()
        {
            Console.WriteLine("Greater Weight: " + GreaterWeight + "  GreaterPrice: " + GreaterPrice +"  List Size: " + ListSize);

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Resolution of Knapsack problem using greedy algorithm\n\n");

            Calcs calc = new Calcs();
            Program prog = new Program();

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            //Read Archive
            prog.ReadArc();

            //Calculate the answer
            calc.calculate();

            // Stop timing.
            stopwatch.Stop();

            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            Console.ReadKey();
        }
        private void ReadArc()
        {
            StreamReader arc = new StreamReader("archive.txt");

            //read the number of products
            Attributes.productsQuantity = int.Parse(arc.ReadLine());

            //read the bag Size
            Attributes.Size = int.Parse(arc.ReadLine());

            Attributes.Weight = new int[Attributes.productsQuantity];
            Attributes.Price = new int[Attributes.productsQuantity];

            for (int i = 0; i < Attributes.productsQuantity; i++)
            {
                string[] value = arc.ReadLine().Split(' ');
                Attributes.Weight[i] = int.Parse(value[0]);
                Attributes.Price[i] = int.Parse(value[1]);
            }
        }

    }
    public class Val : IComparable
    {
        public int position;
        public int reason;

        public Val(int pos, int re)
        {
            position = pos;
            reason = re;
        }
        // implement IComparable interface
        public int CompareTo(object obj)
        {
            if (obj is Val)
            {
                return this.reason == (obj as Val).reason ? 0 : (Math.Max(this.reason, (obj as Val).reason) == this.reason ? -1 : 1);
            }
            throw new ArgumentException("Fail");
        }
    }
    public class Calcs : Attributes
    {


        public void calculate()
        {
            List<Val> answer = new List<Val>();
            Val[] wp = new Val[Weight.Count()];

            for (int i = 0; i < Weight.Count(); i++)
            {
                wp[i] = new Val(i, Price[i] / Weight[i]);


            }

            Array.Sort(wp);

            bool fim = false;
            while (fim == false)
            {
                int i = 0;


                if (Weight[wp[i].position] < Size - GreaterWeight)
                {
                    answer.Add(wp[i]);
                    GreaterPrice += Price[wp[i].position];
                    GreaterWeight += Weight[wp[i].position];
                    i++;
                }
                else
                {
                    ListSize = answer.Count();
                    fim = true;
                }

            }

            printValues();
        }

    }
}
