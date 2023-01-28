using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class SpeechBubbleAndShouting : SingletonMonoBehaviour<SpeechBubbleAndShouting>
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private TextMeshProUGUI speechText;
    public InputAction shout;
    public GameObject letter;

    [Tooltip("the default time in seconds for the bubble to show the text , example : if value is 3 , then the bubble will show for 3 seconds")]
    public float defaultTime = 3.5f;

    public GameObject shoutUIObject;
    private int size = 0;
    private List<string> currentTextList;
    private bool readingText;
    private bool loaded = false;

    private LongHilighted Owner;

    // Start is called before the first frame update
    private void Start()
    {
        shout.Enable();
        shout.performed += Shout;
        InteractionController.instance.interact.performed += NextText;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ShowLetter()
    {
        letter.SetActive(true);
    }

    public void HideLetter()
    {
        letter.SetActive(false);
    }

    private void Shout(InputAction.CallbackContext i)
    {
        ShutUp();
        StopAllCoroutines();
        StartCoroutine(WaitShout());
    }

    private IEnumerator WaitShout()
    {
        shoutUIObject.SetActive(true);
        yield return new WaitForSeconds(3);
        shoutUIObject.SetActive(false);
    }

    public void Speak(string text)
    {
        this.speechText.text = text;
        StartCoroutine(ShowSpeech(defaultTime));
    }

    public void SpeakList(List<string> text, LongHilighted owner)
    {
        if (text.Count > 0)
        {
            Owner = owner;
            bubble.SetActive(true);
            readingText = true;
            MovementController.instance.freez = true;
            size = 0;
            currentTextList = new List<string>(text);
            this.speechText.text = text[0];
        }
    }

    private void NextText(InputAction.CallbackContext i)
    {
        if (readingText && loaded)
        {
            size++;
            if (size <= currentTextList.Count - 1)
            {
                this.speechText.text = currentTextList[size];
            }
            else
            {
                loaded = false;
                bubble.SetActive(false);

                size = 0;
                currentTextList.Clear();
                readingText = false;
                MovementController.instance.freez = false;
                if (Owner.hilighOnce) Destroy(Owner);
                Owner.interacted = false;
                Owner = null;

                return;
            }
        }
        loaded = true;
    }

    public void Speak(string text, float waitTime)
    {
        this.speechText.text = text;
        StartCoroutine(ShowSpeech(waitTime));
    }

    public void ShutUp()
    {
        StopAllCoroutines();
        bubble.SetActive(false);
    }

    public IEnumerator ShowSpeech(float time)
    {
        bubble.SetActive(true);
        yield return new WaitForSeconds(time);
        bubble.SetActive(false);
    }
}