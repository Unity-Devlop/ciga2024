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

        private void Awake()
        {
            StartCheck();

            level01.onClick.AddListener(ToLevel1);
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

        private CancellationTokenSource _videoCts;

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
                    ToLevel1();
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