using System;

namespace TankU.Projectile
{
    interface IHitable
    {
        public event Action<int> OnHitPlayer;
    }
}
