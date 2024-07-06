using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityToolkit;

namespace Game
{
    public class GameMgr : MonoSingleton<GameMgr>
    {
        [field: SerializeField] public Player Local { get; private set; }
        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<PlayerRightMainPanel>();
        }

        protected override void OnDispose()
        {
            UIRoot.Singleton.ClosePanel<PlayerRightMainPanel>();
        }
    }
}