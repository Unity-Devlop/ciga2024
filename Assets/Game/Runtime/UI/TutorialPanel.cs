using System;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class TutorialPanel : UIPanel
    {
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(CloseSelf);
        }
    }
}