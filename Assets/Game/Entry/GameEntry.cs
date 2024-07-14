using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Entry
{
    public class GameEntry : MonoBehaviour
    {
        private void Awake()
        {
            
            Camera camera = Camera.main;
            Camera.main.enabled = false;
            SceneManager.LoadScene("Main");
            camera.enabled = true;
        }
    }
}