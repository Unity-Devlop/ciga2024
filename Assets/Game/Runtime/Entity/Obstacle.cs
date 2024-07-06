using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Obstacle : MonoBehaviour, IObstacle
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnObstacleEnter(player, this);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                GameMgr.Singleton.OnObstacleExit(player, this);
            }
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        public void Recover()
        {
            gameObject.SetActive(true);
        }
    }
}