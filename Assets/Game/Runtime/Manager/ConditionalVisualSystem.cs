using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class ConditionalVisualSystem : ISystem
    {
        private HashSet<IConditionVisual> _conditionVisuals;
        private bool _visual = false;
        public void OnInit()
        {
            _conditionVisuals = new HashSet<IConditionVisual>(128);
            GameMgr.Singleton.Local.data.Bind.Listen(OnData);
            OnData(GameMgr.Singleton.Local.data);
        }

        private void OnData(PlayerData data)
        {
            if (_visual == data.conditionalVisual) return;
            _visual = data.conditionalVisual;
            foreach (var conditionVisual in _conditionVisuals)
            {
                SetVisual(data.conditionalVisual, conditionVisual);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetVisual(bool visual, IConditionVisual conditionVisual)
        {
            conditionVisual.SetVisible(visual);
        }

        public void Dispose()
        {
            GameMgr.Singleton.Local.data.Bind.UnListen(OnData);
        }

        public void Add(IConditionVisual visual)
        {
            _conditionVisuals.Add(visual);
            SetVisual(GameMgr.Singleton.Local.data.conditionalVisual, visual);
        }


        public void Remove(IConditionVisual visual)
        {
            _conditionVisuals.Remove(visual);
        }
    }
}