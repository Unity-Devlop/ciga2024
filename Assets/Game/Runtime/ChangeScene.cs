using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ChangeScene : MonoBehaviour
    {
        public void ChangeSceneTo(string s)
        {
            SceneManager.LoadScene(s);
        }
    }
}
