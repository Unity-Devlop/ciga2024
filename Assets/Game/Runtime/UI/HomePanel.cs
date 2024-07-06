using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class HomePanel : UIPanel
    {
        [SerializeField] private Button level01;
        [SerializeField] private Button level02;
        [SerializeField] private Button level03;
        [SerializeField] private Button level04;
        private void Awake()
        {
            level01.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level01");
            });
            level02.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level02");
            });
            level03.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level03");
            });
            level04.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Level04");
            });
        }
    }
}