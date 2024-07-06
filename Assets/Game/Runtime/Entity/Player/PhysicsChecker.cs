using System;
using UnityEngine;

namespace Game
{
    public class PhysicsChecker : MonoBehaviour
    {
        [field: SerializeField] public bool isGrounded { get; private set; }
        public Vector2 offset;
        public float checkRadius = 0.2f;
        public Transform checkPosition;
        public LayerMask groundLayer;

        private void FixedUpdate()
        {
            Collider2D col =
                Physics2D.OverlapCircle(offset + (Vector2)checkPosition.position, checkRadius, groundLayer);
            isGrounded = col != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)checkPosition.position + offset, checkRadius);
        }
    }
}