using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankU.ColorSelection
{
    [System.Serializable]
    public struct ColorTankList
    {
        [SerializeField] private Type _colorType;
        [SerializeField] private Color _color;

        public Type colorType => _colorType;
        public Color color => _color;

        public enum Type
        {
            Free,
            Premium
        }
    }
}
