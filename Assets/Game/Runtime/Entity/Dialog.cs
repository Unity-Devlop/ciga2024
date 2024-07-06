using System;
using NodeCanvas.DialogueTrees;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Dialog : MonoBehaviour
    {
        public DialogueTreeController dialogueTreeController;

        private void OnTriggerEnter2D(Collider2D other)
        {
            GameMgr.Singleton.StartDialog(dialogueTreeController);
        }
    }
}