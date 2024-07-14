using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
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
        [SerializeField] private Button people;
        public RectTransform buttons;
        public Image level1Fade;
        [SerializeField] private VideoPlayer _videoPlayer;

        private CancellationTokenSource _videoCts;

        [SerializeField] private Button startButton;
        [SerializeField] private Button tutorialButton;

        private void Awake()
        {
            level01.onClick.AddListener(ToLevel1);
            startButton.onClick.AddListener(ToLevel1);
            tutorialButton.onClick.AddListener(OnTutorialButtonClick);
            level02.onClick.AddListener(() =>
            {
                CloseSelf();
                SceneManager.LoadScene("Level02");
            });
            level03.onClick.AddListener(() =>
            {
                CloseSelf();
                SceneManager.LoadScene("Level03");
            });
            level04.onClick.AddListener(() =>
            {
                CloseSelf();
                SceneManager.LoadScene("Level04");
            });
            level05.onClick.AddListener(() =>
            {
                CloseSelf();
                SceneManager.LoadScene("Level05");
            });
            people.onClick.AddListener(() =>
            {
                CloseSelf();
                SceneManager.LoadScene("End");
            });
        }

        private void OnTutorialButtonClick()
        {
            UIRoot.Singleton.OpenPanel<TutorialPanel>();
        }

        public override void OnOpened()
        {
            base.OnOpened();
            StartCheck();
        }

        public override void OnClosed()
        {
            base.OnClosed();
            startButton.gameObject.SetActive(true);
            tutorialButton.gameObject.SetActive(false);
        }

        private async void StartCheck()
        {
            _videoPlayer.Play();
            // 检查视频是否播放完毕
            _videoCts = new CancellationTokenSource();
            try
            {
                await UniTask.WaitUntil(() => _videoPlayer.frame >= (long)_videoPlayer.frameCount - 1,
                    cancellationToken: _videoCts.Token).ContinueWith(() =>
                {
                    if (_videoPlayer == null) return;
                    CompleteVideoDirect();
                    Debug.Log("视频播放完毕");
                    _videoPlayer.Stop();
                    _videoPlayer.gameObject.SetActive(false);
                    startButton.gameObject.SetActive(true);
                    tutorialButton.gameObject.SetActive(true);
                });
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _videoCts.Cancel();
                buttons.gameObject.SetActive(true);
                CompleteVideoDirect();
                return;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                CompleteVideoDirect();
                return;
            }
        }

        private void CompleteVideoDirect()
        {
            _videoPlayer.frame = (long)_videoPlayer.frameCount - 1;
        }


        private async void ToLevel1()
        {
            buttons.gameObject.SetActive(false);
            level1Fade.gameObject.SetActive(true);
            level1Fade.DOColor(Color.black, 1).OnComplete(async () =>
            {
                await SceneManager.LoadSceneAsync("Level01");
                level1Fade.color = Color.clear;
                level1Fade.gameObject.SetActive(false);
                CloseSelf();
                buttons.gameObject.SetActive(true);
            });
        }
    }
}