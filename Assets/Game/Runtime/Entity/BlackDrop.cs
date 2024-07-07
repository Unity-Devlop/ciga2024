using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BlackDrop : MonoBehaviour, IBackDrop
    {
        [SerializeField] private SpriteRenderer render;

        private void Awake()
        {
            render = GetComponent<SpriteRenderer>();
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                render.color = Color.clear;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                render.color = Color.white;
            }
        }
    }
}