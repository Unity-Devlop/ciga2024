using System;
using UnityEngine;

namespace Game
{
    public class Reborn : MonoBehaviour,IReborn
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnRebornHit(player, this);
            }
        }
    }
}