using System;
using UnityEngine;

namespace Game
{
    public class Faster : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        public float mul = 1;

        private void Update()
        {
            mul *= (1 + 0.01f * Time.deltaTime);
        }
    }
}