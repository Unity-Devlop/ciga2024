using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] private Button level05;
        public RectTransform buttons;
        public Image level1Fade;

        private void Awake()
        {
            level01.onClick.AddListener(ToLevel1);
            level02.onClick.AddListener(() => { SceneManager.LoadScene("Level02"); });
            level03.onClick.AddListener(() => { SceneManager.LoadScene("Level03"); });
            level04.onClick.AddListener(() => { SceneManager.LoadScene("Level04"); });
            level05.onClick.AddListener(() => { SceneManager.LoadScene("Level05"); });
        }

        private async void ToLevel1()
        {
            buttons.gameObject.SetActive(false);
            level1Fade.gameObject.SetActive(true);
            level1Fade.DOColor(Color.clear, 1).OnComplete(() =>
            {
                level1Fade.color = Color.black;
                level1Fade.gameObject.SetActive(false);
                CloseSelf();
                buttons.gameObject.SetActive(true);
                SceneManager.LoadScene("Level01");
            });
        }
    }
}