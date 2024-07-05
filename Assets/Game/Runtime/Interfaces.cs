namespace Game
{
    public interface IFish
    {
        public float curAngel { get; }
        public float eyeAngel { get; }
        public float radius { get; }
        void CaughtOther(IFish other);
        void OnBeCaught();
    }
}