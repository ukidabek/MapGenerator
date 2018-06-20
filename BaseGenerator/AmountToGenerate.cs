using UnityEngine;

using System;

namespace MapGenetaroion
{
    [Serializable]
    public class AmountToGenerate
    {
        public enum Type
        {
            Static,
            Random
        }

        [SerializeField] private Type _type = Type.Static;

        [SerializeField] private int _count = 1;

        private bool _randomized = false;
        private int _countFromReandomization = 0;
        [SerializeField] private int _minCount = 1;
        [SerializeField] private int _maxCount = 3;

        public static implicit operator int(AmountToGenerate amount)
        {
            switch (amount._type)
            {
                case Type.Static:
                    return amount._count;
                case Type.Random:
                    if (!amount._randomized)
                    {
                        amount._randomized = true;
                        amount._countFromReandomization = UnityEngine.Random.Range(amount._minCount, amount._maxCount);
                    }
                    return amount._countFromReandomization;
            }
            return 0;
        }

        public void Reser()
        {
            _randomized = false;
        }
    }
}