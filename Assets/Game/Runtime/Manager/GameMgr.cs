using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolkit;

namespace Game
{
    public class GameMgr : MonoSingleton<GameMgr>
    {
        [field: SerializeField] public Player Local { get; private set; }

        protected override void OnInit()
        {
            GameHUDPanel gameHUDPanel = UIRoot.Singleton.OpenPanel<GameHUDPanel>();
            gameHUDPanel.Bind(Local.data);
        }

        protected override void OnDispose()
        {
            if (UIRoot.Singleton.GetOpenedPanel(out GameHUDPanel hudPanel))
            {
                hudPanel.UnBind();
            }
            
            UIRoot.Singleton.ClosePanel<GameHUDPanel>();
        }
    }
}