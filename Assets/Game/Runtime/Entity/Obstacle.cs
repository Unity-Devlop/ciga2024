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
                GameMgr.Singleton.OnObstacleHit(player, this);
            }
        }
    }
}