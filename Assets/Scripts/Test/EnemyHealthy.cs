using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.Character
{
    class EnemyHealthy : MonoBehaviour
    {
        public int HP;

        public int SP;
               
        public void Awake()
        {
            HP = Random.Range(0, 50);
        }

       
        
       

    }
}

