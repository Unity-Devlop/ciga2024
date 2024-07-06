using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class ConditionalVisual : MonoBehaviour, IConditionVisual
    {
        public void Start()
        {
            GameMgr.Systems.Get<ConditionalVisualSystem>().Add(this);
        }

        private void OnDestroy()
        {
            if (GameMgr.SingletonNullable == null) return;
            GameMgr.Systems.Get<ConditionalVisualSystem>().Remove(this);
        }

        public void SetVisible(bool dataConditionalVisual)
        {
            gameObject.SetActive(!dataConditionalVisual);
        }
    }
}