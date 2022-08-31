using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace TankU.ColorSelection
{
    [CreateAssetMenu]
    public class ScriptableColor : ScriptableObject
    {
        [SerializeField] private ColorTankList[] _colors;

        public ColorTankList[] Colors => _colors;

        public ColorTankList[] GetFreeColors()
        {
            return Array.FindAll(_colors, c => c.colorType == ColorTankList.Type.Free);
        }
        public ColorTankList[] GetPremiumColors()
        {
            return Array.FindAll(_colors, c => c.colorType == ColorTankList.Type.Premium);
        }
    }
}
