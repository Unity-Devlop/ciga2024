using System;
using UnityEngine;

namespace Game
{
    public class EnergyObject : MonoBehaviour
    {
        public float energyPoint;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnEnergyObjectEnter(player, this);
            }
        }
    }
}