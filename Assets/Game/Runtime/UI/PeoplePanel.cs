using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityToolkit;

namespace Game
{
    public class PeoplePanel : UIPanel
    {
        public RectTransform peopleContainer;
        public Vector2 anchorPos;
        [SerializeField] private RawImage imgVideo;

        private VideoPlayer _videoPlayer;

        public override void OnOpened()
        {
            base.OnOpened();
            _videoPlayer = GetComponent<VideoPlayer>();
            
            _videoPlayer.Play();
            _videoPlayer.loopPointReached += (vp) =>
            {
                anchorPos = peopleContainer.anchoredPosition;
                imgVideo.gameObject.SetActive(false);
                peopleContainer.DOAnchorPosY(-1000, 18f).SetEase(Ease.Linear).onComplete += CloseSelf;    
            };
        }
    }
}