using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Reborn : MonoBehaviour,IReborn
    {
        private void Awake()
        {
            UnActive();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnRebornHit(player, this);
            }
        }

        public void Active()
        {
            // GetComponent<SpriteRenderer>().enabled = true;
        }

        public void UnActive()
        {
            // GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}