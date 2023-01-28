using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog : Interactable
{
    public List<ConvoText> StartText;

    public List<ConvoText> nextText;
    public List<ConvoText> lastText;
    public List<ConvoText> endText;

    [TextArea]
    public List<string> option1;

    [TextArea]
    public List<string> option2;

    public InputAction press1;
    public InputAction press2;
    private bool choice = false;
    private int index;
    public List<ConvoText> currentConvo;
    public ItemSo flut;
    public GameObject DeadFrog;
    private bool end;
    public AudioSource flutmusic;

    public enum person
    {
        frog, rose
    }

    private void Start()
    {
        PlayerAIMovement.instance.skip.started += NextText;

        press1.Enable();
        press2.Enable();
        press1.performed += Press1;
        press2.performed += Press2;
    }

    [System.Serializable]
    public class ConvoText
    {
        [TextArea]
        public string text;

        public person person;
    }

    public override void OnInteract()
    {
        if (!interacted)
        {
            interacted = true;
            MovementController.instance.freez = true;
            base.OnInteract();
            InteractionController.instance.DisableButton();
            index = 0;
            if (ConvoManager.instance.convostate == ConvoManager.FrogState.nottalked)
            {
                currentConvo = StartText;
            }
            else if (ConvoManager.instance.convostate == ConvoManager.FrogState.talked)

            {
                currentConvo = nextText;
            }
            else if (ConvoManager.instance.convostate == ConvoManager.FrogState.finshedtalking)

            {
                if (!inventorySystem.instance.HasItem(flut))
                {
                    currentConvo = lastText;
                }
                else
                {
                    currentConvo = endText;
                    end = true;
                    choice = true;
                    ConvoManager.instance.ShowOptions("1 play the flut (press left arrow <-)", "2 kill it !(press right arrow ->)");
                    return;
                }
            }
            PlayerAIMovement.instance.spaceBar.SetActive(true);
            ConvoManager.instance.Frog = this;
            if (currentConvo[index].person == person.frog)
            {
                ConvoManager.instance.ShowFrogText(currentConvo[index].text);
            }
            else
            {
                ConvoManager.instance.ShowRoseText(currentConvo[index].text);
            }
        }
    }

    public void EndConvo()
    {
        InteractionController.instance.EnableButton();
        MovementController.instance.freez = false;
        PlayerAIMovement.instance.spaceBar.SetActive(false);
        interacted = false;
        choice = false;
        ConvoManager.instance.EndConvo();
    }

    public void Press1(InputAction.CallbackContext i)
    {
        if (choice)
        {
            if (ConvoManager.instance.convostate == ConvoManager.FrogState.nottalked)
            {
                EndConvo();
            }
            else if (ConvoManager.instance.convostate == ConvoManager.FrogState.finshedtalking)
            {
                StartCoroutine(PlayFult());
            }
        }
    }

    public IEnumerator PlayFult()
    {
        flutmusic.Play(0);
        ConvoManager.instance.EndConvo();
        yield return new WaitForSeconds(flutmusic.clip.length);
        end = false;
        PlayerAIMovement.instance.spaceBar.SetActive(true);
        ConvoManager.instance.Frog = this;
        if (currentConvo[index].person == person.frog)
        {
            ConvoManager.instance.ShowFrogText(currentConvo[index].text);
        }
        else
        {
            ConvoManager.instance.ShowRoseText(currentConvo[index].text);
        }
    }

    public void killFrog()
    {
        Instantiate(DeadFrog, transform.position, Quaternion.identity);
        Destroy(gameObject);
        EndConvo();
    }

    public void Press2(InputAction.CallbackContext i)
    {
        if (choice)
        {
            if (ConvoManager.instance.convostate == ConvoManager.FrogState.nottalked)
            {
                choice = false;
                ConvoManager.instance.convostate = ConvoManager.FrogState.talked;
                index = 0;
                currentConvo = nextText;
                ConvoManager.instance.ShowFrogText(currentConvo[0].text);
            }
            else if (ConvoManager.instance.convostate == ConvoManager.FrogState.finshedtalking)
            {
                killFrog();
            }
        }
    }

    private void NextText(InputAction.CallbackContext i)
    {
        if (interacted && !end)
        {
            index++;

            if (index < currentConvo.Count)
            {
                if (currentConvo[index].person == person.frog)
                {
                    ConvoManager.instance.ShowFrogText(currentConvo[index].text);
                }
                else
                {
                    ConvoManager.instance.ShowRoseText(currentConvo[index].text);
                }
            }
            else
            {
                if (ConvoManager.instance.convostate == ConvoManager.FrogState.nottalked)
                {
                    choice = true;
                    ConvoManager.instance.ShowOptions("1 “Frogs don’t talk”(press left arrow <-)", "2 “It has.”(press right arrow ->)");
                }
                else if (ConvoManager.instance.convostate == ConvoManager.FrogState.talked)
                {
                    index = 0;
                    currentConvo = lastText;
                    ConvoManager.instance.convostate = ConvoManager.FrogState.finshedtalking;
                }
                else
                {
                    EndConvo();
                }
            }
        }
    }
}