using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerAudio : MonoBehaviour
    {
        public AudioSource rain;

        private void OnEnable()
        {
            rain.Play();
        }

        private void OnDisable()
        {
            rain.Stop();
        }
    }
}