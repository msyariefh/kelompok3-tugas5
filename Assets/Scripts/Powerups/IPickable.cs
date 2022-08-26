using System;
using UnityEngine;

namespace TankU.PowerUP
{
    interface IPickable
    {
        public event Action<int, PowerUpController.Type> OnPlayerPicked;
    }
}
