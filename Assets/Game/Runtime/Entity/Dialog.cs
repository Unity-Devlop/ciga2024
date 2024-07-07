using System;
using System.Collections;
using NodeCanvas.DialogueTrees;
using UnityEngine;
using UnityEngine.Serialization;
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
            DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
            Global.Event.Listen<DialogNodePlayOver>(CheckIsContinue);
        }

        void UnSubscribe()
        {
            DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
        }
        
        void OnSubtitlesRequest(SubtitlesRequestInfo info)
        {
            if (string.IsNullOrEmpty(info.statement.meta))
                return;
            var eventName = info.statement.meta;
            switch (eventName)
            {
                case nameof(UnlockHeadSet):
                    info.Continue += () =>
                    {
                        
                    };
                    break;
                
                default:
                    Debug.LogError($"没有 <color=red>{eventName}</color> 对应的事件");
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
                        UIRoot.Singleton.GetOpenedPanel<DialoguePanel>(out var panel);
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