using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LevelEnd : MonoBehaviour
    {
        public string level;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                SceneManager.LoadScene(level);
            }
        }
    }
}