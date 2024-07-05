using System;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class AiFish : MonoBehaviour, IFish
    {
        public float curAngel { get; private set; }
        public float eyeAngel { get; private set; }
        public float radius { get; private set; }

        private void Update()
        {
        }

        public void CaughtOther(IFish other)
        {
            throw new System.NotImplementedException();
        }

        public void OnBeCaught()
        {
            GameObjectPoolManager.Release(nameof(AiFish), gameObject);
        }
    }
}