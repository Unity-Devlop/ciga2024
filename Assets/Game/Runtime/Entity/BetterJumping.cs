using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class BetterJumping : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private Player _player;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _rb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_rb2D.velocity.y < 0)
            {
                _rb2D.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
            }
            else if (_rb2D.velocity.y > 0 && !_player.Input.Jump.WasPerformedThisFrame())
            {
                _rb2D.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }
    }
}