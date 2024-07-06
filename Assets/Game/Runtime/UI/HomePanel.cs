using System;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class HomePanel : GamePanel
    {
        [SerializeField] private Button enterGameButton;

        private void Awake()
        {
            enterGameButton.onClick.AddListener(OnEnterGameButtonClick);
        }

        private void OnEnterGameButtonClick()
        {
            Global.Systems.Get<GameFlow>().EnterGame();
        }
    }
}