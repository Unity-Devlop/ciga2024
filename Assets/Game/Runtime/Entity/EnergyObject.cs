using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class EnergyObject : MonoBehaviour
    {
        public float energyPoint;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnEnergyObjectEnter(player, this);
            }
        }

        public async void BeEat()
        {
            gameObject.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(5));
            gameObject.SetActive(true);
        }
    }
}