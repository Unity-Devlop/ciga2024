using UnityToolkit;

namespace Game
{
    public class InfGameMgr : GameMgr
    {
        protected override void OnInit()
        {
            base.OnInit();
            UIRoot.Singleton.ClosePanel<GamePanel>();
        }
    }
}