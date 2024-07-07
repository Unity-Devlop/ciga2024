using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolkit;

namespace Game
{
    public struct BossCome : IEvent
    {
        
    }
    
    public struct ChangeBody : IEvent
    {
        
    }
    
    public class Home : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites = new Sprite[3];
        [SerializeField] private string _sceneName;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameMgr.Singleton.Local.data.canMove = false;
            SceneManager.LoadScene(_sceneName);
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            Global.Event.Listen<BossCome>(BossCome);
            Global.Event.Listen<ChangeBody>(ChangeBody);
        }

        private void OnDestroy()
        {
            if (Global.SingletonNullable==null)
                return;
            
            Global.Event.UnListen<BossCome>(BossCome);
            Global.Event.UnListen<ChangeBody>(ChangeBody);
        }

        private void ChangeBody(ChangeBody obj)
        {
            _spriteRenderer.sprite = _sprites[2];
        }

        private void BossCome(BossCome obj)
        {
            _spriteRenderer.sprite = _sprites[1];
        }
    }
}
