using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class AiFishSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float interval = 10f;
        private CancellationTokenSource _spawnCts;
        private Player _player => GameMgr.Singleton.Local;

        private void OnEnable()
        {
            GameObjectPoolManager.Create(nameof(AiFish), prefab);
            _spawnCts = new CancellationTokenSource();
            SpawnTask().Forget();
        }

        private void OnDisable()
        {
            _spawnCts.Cancel();
        }

        private async UniTask SpawnTask()
        {
            while (!_spawnCts.Token.IsCancellationRequested)
            {
                SpawnFish();
                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: _spawnCts.Token);
            }
        }

        private void SpawnFish()
        {
            GameObject go = GameObjectPoolManager.Get(nameof(AiFish));
            Vector3 playerPos = _player.transform.position;
            go.transform.position = new Vector3(playerPos.x + UnityEngine.Random.Range(-5, 5),
                playerPos.y + UnityEngine.Random.Range(-5, 5), 0);
        }

        private void Update()
        {
        }
    }
}