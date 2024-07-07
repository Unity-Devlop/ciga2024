using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class HitPanel : UIPanel
    {
        public Image image;

        public override void OnOpened()
        {
            image.DOComplete();
            base.OnOpened();
            image.DOColor(new Color(1, 0, 0, 0.5f), 0.5f).OnComplete(() =>
            {
                image.DOColor(Color.clear, 0.5f).OnComplete(CloseSelf);
            });
        }
    }
}