using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public InputAction skipDialogue;
    public DialogueBox DialogueBox;

    [HideInInspector]
    public NPCDialogue talkingNPC;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        skipDialogue.Enable();
        skipDialogue.performed += Skip;
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void Skip(InputAction.CallbackContext i)
    {
        if (talkingNPC != null)
            talkingNPC.Skip();
    }

    public void DisableDialogueBox()
    {
        DialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
        talkingNPC = null;
    }

    public void EnableDialogueBox(NPCDialogue talkingNPC)
    {
        this.talkingNPC = talkingNPC;
        DialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void SkipDialogue()
    {
        if (talkingNPC != null)
        {
            talkingNPC.Skip();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}