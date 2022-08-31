using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankU.ColorSelection
{
    [CreateAssetMenu]
    public class ScriptableColor : ScriptableObject
    {
        [SerializeField] private ColorTankList[] _colors;

        public ColorTankList[] Colors => _colors;
    }
}
