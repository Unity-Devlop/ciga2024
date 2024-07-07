using System;
using NodeCanvas.DialogueTrees;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Dialog : MonoBehaviour
    {
        public DialogueTreeController dialogueTreeController;

        private Collider2D _collider;
        
        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            GameMgr.Singleton.StartDialog(dialogueTreeController);
            _collider.enabled = false;
        }
    }
}