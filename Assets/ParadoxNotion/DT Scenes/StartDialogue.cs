using UnityEngine;
using NodeCanvas.DialogueTrees;

public class StartDialogue : MonoBehaviour
{
    public DialogueTreeController dialogueController;

    private void Start()
    {
        dialogueController.StartDialogue(OnDialogueEnd);
    }

    void OnDialogueEnd(bool success) 
    {
        gameObject.SetActive(true);
    }
}