using UnityEngine;

namespace Game
{
    
    public interface IConditionVisual
    {
        void SetVisible(bool dataConditionalVisual);
    }
    public interface IControl
    {
        public void Set(Player player);
        public void Update();
        void RecoverDash();
    }

    public interface IObstacle
    {
        public int hitPoint { get; }
        public void Destroy();
        public void Recover();
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