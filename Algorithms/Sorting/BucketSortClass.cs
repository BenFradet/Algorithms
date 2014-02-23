using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    static class BucketSortClass
    {
        public static float[] BucketSort(float[] input)
        {
            LinkedList<float>[] lists = new LinkedList<float>[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                lists[i] = new LinkedList<float>();
            }
            for (int i = 0; i < input.Length; i++)
            {
                lists[(int)(input.Length * input[i])].AddFirst(input[i]);
            }
            for (int i = 0; i < input.Length; i++)
            {
                float[] sorted = InsertionSortClass.InsertionSort(lists[i].ToArray());
                lists[i] = new LinkedList<float>(sorted);
            }
            float[] result = new float[input.Length];
            int index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                lists[i].CopyTo(result, index);
                index += lists[i].Count;
            }
            return result;
        }
    }
}
