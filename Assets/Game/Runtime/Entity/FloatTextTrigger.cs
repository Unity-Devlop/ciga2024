using System;
using UnityEngine;

namespace Game
{
    public class FloatTextTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnFloatText(player, this);
            }
        }
    }
}