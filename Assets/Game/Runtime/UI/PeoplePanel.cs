using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolkit;

namespace Game
{
    public class PeoplePanel : UIPanel
    {
        public RectTransform peopleContainer;
        public Vector2 anchorPos;

        public override void OnOpened()
        {
            base.OnOpened();
            anchorPos = peopleContainer.anchoredPosition;

            peopleContainer.DOAnchorPosY(-1000, 18f).SetEase(Ease.Linear).onComplete += CloseSelf;
        }
    }
}