using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 유전_알고리즘
{
    public delegate int RandomDelgate(int a, int b);/*ArrayMaker 내부에서 Random 메소드의 
                                                      인스턴스를 생성하면 단 하나의 랜덤 값이 
                                                      반복되기 때문에 델리게이터로 전달해야했다.*/
    class Connecters
    {
        //해당 범위 내에서 숫자를 무작위로 뽑아 가변배열을 만든다.
        public int[][] ArrayMaker(int BigArrayLength, int SmellArrayLength, RandomDelgate RD, int minNumber, int maxNumber)
        {
            int[][] A = new int[BigArrayLength][];
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = new int[SmellArrayLength];
                for (int j = 0; j < A[i].Length; j++)
                {
                    A[i][j] = RD(minNumber, maxNumber + 1);
                }
            }
            return A;
        }
    }
    public class MyComper : System.Collections.IComparer
    {
        int sortKey;
        public MyComper(int a)//a는 가변배열 속 배열의 요소 번호 ex: a=0 일 때 int[0][0], int[1][0]을 열람하여 비교한다.
        {
            sortKey = a;
        }
        public int Compare(object x, object y)//x,y는 배열 속 요소들을 임의로 나타낸 것
        {
            IComparable cX = (IComparable)((Array)x).GetValue(sortKey);
            IComparable cY = (IComparable)((Array)y).GetValue(sortKey);

            return cY.CompareTo(cX);
        }
    }
    class Selecters //ArrayMaker에서 만든 가변배열을 정렬하는 곳
    {
        public int[] OutPutArray;
        public int[][] SelectMethod(int[][] BigArray, int MostSmallValue, out int[][] ErasedBigArray)
        {
            int[] Q = new int[BigArray.Length];
            for (int i = 0; i < BigArray.Length; i++)
            {
                for (int j = 0; j < BigArray[i].Length; j++)
                {
                    Q[i] = BigArray[i][j] + Q[i];
                }
            }
            this.OutPutArray = Q; //테스트 할 때 내보내 보려고 
            int[][] willComparedBigArray = new int[Q.Length][];
            for (int i = 0; i < willComparedBigArray.Length; i++)
            {
                willComparedBigArray[i] = new int[2];
            }
            for (int i = 0; i < Q.Length; i++)
            {
                if (Q[i] >= MostSmallValue)
                {
                    willComparedBigArray[i][0] = Q[i];
                    willComparedBigArray[i][1] = i;
                }
                else
                {
                    willComparedBigArray[i][0] = 0;
                    willComparedBigArray[i][1] = i;
                }
            }
            Array.Sort(willComparedBigArray, new MyComper(0));//MyComper의 규칙에 따라 willComparedBigArray를 정렬
            int[][] W = new int[Q.Length][];                  //
            int p = 1;
            for (int i = 0; i < Q.Length; i++)
            {
                W[i] = BigArray[willComparedBigArray[i][1]];
                if (willComparedBigArray[i][0] != 0) //조건 만족 여부 검사
                {
                    p++;
                }
            }
            int[][] E = new int[p][];
            int v = 0;
            for (int i = 0; i < Q.Length; i++)
            {
                if (willComparedBigArray[i][0] != 0)
                {
                    E[v] = BigArray[willComparedBigArray[i][0]];
                    v++;
                }
            }
            ErasedBigArray = E;
            return W;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Connecters littleConnecters = new Connecters();
            Random littleRandom = new Random();
            RandomDelgate littleRDelgate = new RandomDelgate(littleRandom.Next);
            Selecters littleSelecters = new Selecters();
            int[][] startBigArray = littleConnecters.ArrayMaker(4, 5, littleRDelgate, 1, 10);
            foreach (var item in startBigArray)
            {
                Console.WriteLine("");
                foreach (var itemo in item)
                {
                    Console.Write(itemo + " ");
                }
            }
            Console.WriteLine("");
            int[][] willErasedArray;
            int[][] lessArray = littleSelecters.SelectMethod(startBigArray, 10, out willErasedArray);
            foreach (var item in lessArray)
            {
                Console.WriteLine("");
                foreach (var itemo in item)
                {
                    Console.Write(itemo + " ");
                }
            }
            Console.WriteLine("");
        }
    }
}