using System.Collections;
using NodeCanvas.DialogueTrees;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Dialog : MonoBehaviour
    {
        public DialogueTreeController dialogueTreeController;

        private Collider2D _collider;
        private SubtitlesRequestInfo _waitInfo;
        
        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }

        void OnEnable()
        {
            UnSubscribe();
            Subscribe();
        }

        void OnDisable()
        {
            UnSubscribe();
        }

        void Subscribe()
        {
            Global.Event.Listen<DialogNodePlayOver>(CheckIsContinue);
            DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
        }

        void UnSubscribe()
        {
            Global.Event.UnListen<DialogNodePlayOver>(CheckIsContinue);
            DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
        }

        void OnSubtitlesRequest(SubtitlesRequestInfo info)
        {
            if (string.IsNullOrEmpty(info.statement.meta))
                return;
            
            var eventName = info.statement.meta;
            switch (eventName)
            {
                case nameof(BossCome):
                    Global.Event.Send(new BossCome());
                    Debug.Log("send -- > BossCome");
                    break;
                case nameof(ChangeBody):
                    Global.Event.Send(new ChangeBody());
                    Debug.Log("send -- > ChangeBody");
                    break;
                case nameof(RingUp):
                    Global.Event.Send(new RingUp());
                    Debug.Log("send -- > RingUp");
                    break;
                case nameof(RingOver):
                    Global.Event.Send(new RingOver());
                    Debug.Log("send -- > RingOver");
                    break;
            }
        }

        private void CheckIsContinue(DialogNodePlayOver obj)
        {
            var info = obj.requestInfo;
            if (string.IsNullOrEmpty(info.statement.meta))
            {
                info.Continue();
            }
            else
            {
                var eventName = info.statement.meta;
                switch (eventName)
                {
                    case nameof(UnlockHeadSet):
                        Global.Event.Send(new UnlockHeadSet());
                        Debug.Log("send -- > UnlockHeadSet");
                        Invoke("Connect", 2f);
                        _waitInfo = info;
                        StartCoroutine(WaitE());
                        break;
                    default:
                        info.Continue();
                        break;
                }
            }
        }
        
        IEnumerator WaitE()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UIRoot.Singleton.GetOpenedPanel<DialoguePanel>(out var panel);
                    _waitInfo.Continue();
                    yield break;
                }

                yield return null;
            }
        }

        private void Connect() => Global.Event.Send(new ConnectHeadSet());
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameMgr.Singleton.StartDialog(dialogueTreeController);
            _collider.enabled = false;
        }
    }
}