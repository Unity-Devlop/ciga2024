using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class HomeMgr : MonoSingleton<HomeMgr>
    {
        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<HomePanel>();
        }

        protected override void OnDispose()
        {
            UIRoot.Singleton.ClosePanel<HomePanel>();
        }
    }
}