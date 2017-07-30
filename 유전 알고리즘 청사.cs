using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public delegate int Randomio(int a, int b);
    class Connecters
    {
        public int[][] Maker(int BigLength, int SmellLength, Randomio c, int d, int e)
        {
            int[][] A = new int[BigLength][];
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = new int[SmellLength];
                for (int j = 0; j < A[i].Length; j++)
                {
                    A[i][j] = c(d, e+1);
                }
            }
            return A;
        }
    }
    public class MyComper : System.Collections.IComparer
    {
        int sortKey;
        public MyComper(int a)
        {
            sortKey = a;
        }
        public int Compare(object x, object y)
        {
            IComparable cX = (IComparable)((Array)x).GetValue(sortKey);
            IComparable cY = (IComparable)((Array)y).GetValue(sortKey);
        
            return cY.CompareTo(cX);
        }
    }
    class Selecter
    {
        public int[][] BeC;
        public int[] Ec;
        public int[][] SelectMethod(int[][] a, int LowestValue, out int[][] EresingValue )
        {
            this.BeC = a;
            int[] Q = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                {
                    Q[i] = a[i][j] + Q[i];
                }
            }
            this.Ec = Q;
            int[][] RaC = new int[Q.Length][];
            for (int i = 0; i < RaC.Length; i++)
            {
                RaC[i] = new int[2];
            }
            for (int i = 0; i < Q.Length; i++)
            {
                if (Q[i] >= LowestValue)
                {
                    RaC[i][0] = Q[i];
                    RaC[i][1] = i;
                }
                else
                {
                    RaC[i][0] = 0;
                    RaC[i][1] = i;
                }
            }
            Array.Sort(RaC, new MyComper(0));
            int[][] G = new int[Q.Length][];
            int p = 1;
            for (int i = 0; i < Q.Length; i++)
            {
                G[i] = BeC[RaC[i][1]];
                if (RaC[i][0]!=0) //조건 만족
                {
                    p++;
                }
            }
            int[][] H = new int[p][];
            int v = 0;
            for (int i = 0; i < Q.Length; i++)
            {
                if (RaC[i][0] != 0) 
                {
                    H[v] = BeC[RaC[i][0]];
                    v++;
                }
            }
            EresingValue = H;
            return G;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Connecters con = new Connecters();
            Random ran = new Random();
            Randomio rD = new Randomio(ran.Next);
            Selecter sel = new Selecter();
            int[][] arr = con.Maker(4, 5, rD, 1, 10);
            foreach (var item in arr)
            {
                Console.WriteLine("");
                foreach (var itemo in item)
                {
                    Console.Write(itemo + " ");
                }
            }
            Console.WriteLine("");
            int[][] JJj;
            int[][]BbL=sel.SelectMethod(arr,10, out JJj);
            foreach (var item in BbL)
            {
                    Console.WriteLine("");
                    foreach (var itemo in item)
                    {
                        Console.Write(itemo + " ");
                    }
            }
            /*foreach (var item in R)
            {
                foreach (var itemo in item)
                {
                    Console.Write(itemo + " ");
                }
            }*/
            Console.WriteLine("");
        }
    }
}
