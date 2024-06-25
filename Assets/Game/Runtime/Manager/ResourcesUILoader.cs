using System;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public struct ResourcesUILoader : IUILoader
    {
        public GameObject Load<T>() where T : IUIPanel
        {
            return Resources.Load<GameObject>($"UI/{typeof(T).Name}");
        }

        public void LoadAsync<T>(Action<GameObject> callback) where T : IUIPanel
        {
            var handle = Resources.LoadAsync<GameObject>($"UI/{typeof(T).Name}");
            handle.completed += operation => { callback(handle.asset as GameObject); };
        }
    }
}