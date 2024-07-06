using System;
using UnityEngine;

namespace Game
{
    public class Spring : MonoBehaviour, ISpring
    {
        public Vector2 force = new Vector2(0, 10);
        private void OnCollisionEnter2D(Collision2D other)
        {
            // 给一个弹力
            if (other.collider.TryGetComponent(out Player player))
            {
                player.rb2D.velocity = new Vector2(player.rb2D.velocity.x, 0);
                player.rb2D.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }
}