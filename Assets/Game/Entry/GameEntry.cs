using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Entry
{
    public class GameEntry : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.LoadScene("Main");
        }
    }
}