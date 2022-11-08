using ARPGDemo.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    class ArrayHelperDemo:MonoBehaviour
    {
        void Start()
        {
            EnemyHealthy[] enemies = GameObject.FindObjectsOfType<EnemyHealthy>();
            EnemyHealthy minEnemyHealthy = FindMax(enemies);
            minEnemyHealthy.GetComponent<Renderer>().material.color = Color.yellow;


            List<EnemyHealthy> list =  FindLess20(enemies);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].GetComponent<Renderer>().material.color = Color.blue;
            }

            EnemyHealthy distance = FindMinDistance(enemies);
            distance.GetComponent<Renderer>().material.color = Color.red;

        }

        public EnemyHealthy FindMinDistance(EnemyHealthy[] enemies)
        {
            float minDistance = (enemies[0].transform.position - transform.position).magnitude;
            int index = 0;

            for (int i = 1; i < enemies.Length; i++)
            {
                float distance = (enemies[i].transform.position - transform.position).magnitude;
                if(minDistance > distance)
                {
                    minDistance = distance;
                    index = i;
                }

            }
            return enemies[index];
        }



        public static List<EnemyHealthy> FindLess20(EnemyHealthy[] enemies)
        {
            List<EnemyHealthy> ens = new List<EnemyHealthy>();
            foreach (var item in enemies)
            {
                if(item.HP > 20)
                {
                    ens.Add(item);
                }
            }
            return ens;
        }

        public static EnemyHealthy FindMax(EnemyHealthy[] enemies)
        {
            EnemyHealthy minEnemyHealthy = enemies[0];
            for (int i = 1; i < enemies.Length; i++)
            {
                if(minEnemyHealthy.HP > enemies[i].HP)
                {
                    minEnemyHealthy = enemies[i];
                }
            }
            return minEnemyHealthy;
        }
        

        //public static T[] BubbleSort(T[] sums)
        //{ 
        //    for (int i = 0; i < sums.Length; i++)
        //    {
        //        for (int j = 0; j < sums.Length - i - 1; j++)
        //        {
        //            if (sums[j].CompareTo(sums[j + 1]) > 0)
        //            {
        //                // 交换 第i个和第i+1的顺序
        //                T temp = sums[j];
        //                sums[j] = sums[j + 1];
        //                sums[j + 1] = temp;
        //            }
        //        }
        //    }
        //    return sums;
        //}
    }
}
