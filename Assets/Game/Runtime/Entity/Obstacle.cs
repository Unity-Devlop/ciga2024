using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Obstacle : MonoBehaviour, IObstacle
    {
        [SerializeField] private SpriteRenderer tip;
        [SerializeField] private float trickTime;

        private void Start()
        {
            if (tip)
            {
                DOVirtual.Float(0f, 1f, trickTime, (val) =>
                {
                    tip.color = new Color(tip.color.r, tip.color.g, tip.color.b, val);
                }).SetLoops(-1, LoopType.Yoyo);
            }
        }

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