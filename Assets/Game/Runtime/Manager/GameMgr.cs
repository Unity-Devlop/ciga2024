using UnityEngine.SceneManagement;
using UnityToolkit;

namespace Game
{

    public class GameMgr : MonoSingleton<GameMgr>
    {
        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<GameHUDPanel>();
        }

        protected override void OnDispose()
        {
            UIRoot.Singleton.ClosePanel<GameHUDPanel>();
        }
    }
}