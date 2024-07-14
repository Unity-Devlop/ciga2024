using System;
using Cinemachine;
using DG.Tweening;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Game
{
    public class ChangeVirtualCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
            {
                DOTween.To<float, float, FloatOptions>(new FloatPlugin(), () => virtualCamera.m_Lens.OrthographicSize,
                    (f) => virtualCamera.m_Lens.OrthographicSize = f, 10, 1f);
            }
        }
    }
}