using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.BaseGenerator
{
    public class SetSeed : MonoBehaviour, IGenerationInitalization
    {
        [SerializeField] private int _seed = 200;
        [SerializeField] private bool _setSeed = false;
        
        public void Initialize(BaseDungeonGenerator generator)
        {
            if (!_setSeed)
                return;

            UnityEngine.Random.InitState(_seed);    
        }
    }
}