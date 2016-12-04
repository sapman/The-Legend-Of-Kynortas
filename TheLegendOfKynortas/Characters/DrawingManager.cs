using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using SAPDataStructures.Heap;
using Microsoft.Xna.Framework.Graphics;
using The_Legend_Of_Kynortas.Objects;

namespace The_Legend_Of_Kynortas
{
    static class CharacterManager
    {
        //static Heap<GameObject> drawHeap = new Heap<GameObject>(false);
        static List<GameObject> drawList = new List<GameObject>();
        static List<UsableObject> topList = new List<UsableObject>();
        public static void Add(GameObject dr)
        {
            //drawHeap.Add(dr);
            drawList.Add(dr);
        }
        public static void DrawNameAtTop(UsableObject obj)
        {
            topList.Add(obj);
        }

        public static void DrawAll(SpriteBatch spriteBatch)
        {
            //while (!drawHeap.IsEmpty())
            //{
            //    drawHeap.Extract().Draw(spriteBatch);
            //}
            //drawHeap = new Heap<GameObject>(false);

            BubbleSort(drawList);

            for (int i = 0; i < drawList.Count; i++)
            {
                drawList[i].Draw(spriteBatch);
            }

            drawList = new List<GameObject>();
        }

        public static void DrawTops(SpriteBatch spriteBatch)
        {
            foreach (var obj in topList)
            {
                obj.DrawName(spriteBatch);
            }

            topList = new List<UsableObject>();
        }

        private static void BubbleSort(List<GameObject> arr)
        {

            GameObject temp;

            for (int write = 0; write < arr.Count; write++)
            {
                for (int sort = 0; sort < arr.Count - 1; sort++)
                {
                    if (arr[sort].CompareTo(arr[sort + 1]) > 0)
                    {
                        temp = arr[sort + 1];
                        arr[sort + 1] = arr[sort];
                        arr[sort] = temp;
                    }
                }
            }
        }
    }
}
