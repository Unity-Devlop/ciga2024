using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class RecoverDashObject : MonoBehaviour
    {
        public float coolDown=3f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnRecoverDash(player, this);
            }
        }

        public void CollDown()
        {
            gameObject.SetActive(false);
            UniTask.Delay(TimeSpan.FromSeconds(coolDown)).ContinueWith(() =>
            {
                gameObject.SetActive(true);
            });
        }
    }
}