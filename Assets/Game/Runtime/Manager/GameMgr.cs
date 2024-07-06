using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityToolkit;

namespace Game
{
    public class GameMgr : MonoSingleton<GameMgr>
    {
        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<PlayerRightMainPanel>();
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Click"));
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/FoodCreateManager"));
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/CountDown"));
        }

        protected override void OnDispose()
        {
            UIRoot.Singleton.ClosePanel<PlayerRightMainPanel>();
        }

    }
}