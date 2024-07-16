using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityToolkit;

namespace Game
{
    public struct BossCome : IEvent {}
    
    public struct ChangeBody : IEvent {}
    
    public class Home : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites = new Sprite[3];
        [SerializeField] private string _sceneName;
        
        private SpriteRenderer _spriteRenderer;
        private VideoPlayer _videoPlayer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            _videoPlayer.Play();
            GameMgr.Singleton.Local.rb2D.simulated = false;
            Global.Event.Send(new PlayVideoBegin());
            var ao = SceneManager.LoadSceneAsync(_sceneName);
            ao.allowSceneActivation = false;
            _videoPlayer.loopPointReached += (vp) =>
            {
                GameMgr.Singleton.Local.data.canMove = false;
                GameMgr.Singleton.Local.rb2D.simulated = true;
                Global.Event.Send(new PlayVideoOver());
                ao.allowSceneActivation = true;
            };
        }

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _videoPlayer = GetComponent<VideoPlayer>();

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

        private void ChangeBody(ChangeBody obj) => _spriteRenderer.sprite = _sprites[2];

        private void BossCome(BossCome obj) => _spriteRenderer.sprite = _sprites[1];
    }
}
