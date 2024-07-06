using UnityEngine;

namespace Game
{
    public interface IControl
    {
        public void Set(Player player);
        public void Update();
    }

    public interface IObstacle
    {
    }

    public interface ISpring
    {
    }

    public interface IReborn
    {
        public Transform transform { get; }
        public void Active();
        public void UnActive();
    }

    public interface IBackDrop
    {
        
    }
}