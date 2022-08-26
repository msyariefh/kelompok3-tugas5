namespace TankU.Timer
{
    interface IPausable
    {
        public void OnGamePaused();
        public void OnGameResumed();
        public void OnGameOver();
    }
}
