using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Reborn : MonoBehaviour,IReborn
    {
        private void Start()
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
            // gameObject.SetActive(true);
        }

        public void UnActive()
        {
            // gameObject.SetActive(false);
        }
    }
}