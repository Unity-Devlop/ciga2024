using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class FloatTextPanel : GamePanel
    {
        [SerializeField] private TextMeshProUGUI _content;
        private CancellationTokenSource _cancellation;

        public async UniTask Set()
        {
            _cancellation?.Cancel();
            _cancellation = new CancellationTokenSource();
            // TODO 打字机效果
        }

        public void Cancel()
        {
            _cancellation?.Cancel();
        }
    }
}