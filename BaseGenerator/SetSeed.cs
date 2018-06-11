using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenetaroion.BaseGenerator
{
    public class SetSeed : MonoBehaviour, IGenerationInitalization
    {
        [SerializeField] private int _seed = 200;
        [SerializeField] private bool _setSeed = false;
        
        public void Initialize(LevelGenerator generator, params object[] generationData)
        {
            if (!_setSeed)
                return;

            UnityEngine.Random.InitState(_seed);    
        }
    }
}