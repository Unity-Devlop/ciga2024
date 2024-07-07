using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class EndManager : MonoSingleton<EndManager>
    {
        protected override void OnInit()
        {
            UIRoot.Singleton.OpenPanel<PeoplePanel>();
        }

        protected override void OnDispose()
        {
            UIRoot.Singleton.ClosePanel<PeoplePanel>();
        }
    }
}