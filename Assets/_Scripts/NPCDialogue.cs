using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : Interactable
{
    private DialogueBox ActiveBox;
    public DialogueTextSO dialogueScript;
    private int index = 0;

    public override void OnInteract()
    {
        if (!interacted)
        {
            if (dialogueScript.Text.Count > 0)
                DialogueManager.instance.EnableDialogueBox(this);
            ActiveBox = DialogueManager.instance.DialogueBox;
            ActiveBox.DisplayText(dialogueScript.Text[0]);
            interacted = true;
        }
    }

    public void Skip()
    {
        if (dialogueScript.Text.Count >= index + 2)
        {
            index++;
            ActiveBox.DisplayText(dialogueScript.Text[index]);
        }
        else
        {
            OnQuit();
        }
    }

    public override void OnQuit()
    {
        index = 0;
        interacted = false;
        DialogueManager.instance.DisableDialogueBox();
    }

    public override void OnHighLight(InteractionController player)
    {
    }

    public override void OnUnHighLight(InteractionController player)
    {
    }
}