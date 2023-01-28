using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public int textSpeed = 10;
    public TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //DisplayText("chicken is my fav food i love itsoo much hahaha what ? , i was joking X)");
    }

    public void DisplayText(string text)
    {
        textMeshProUGUI.text = null;
        StartCoroutine(Wait(text));
    }

    private IEnumerator Wait(string text)
    {
        foreach (char c in text)
        {
            textMeshProUGUI.text += c;
            yield return new WaitForSeconds(10 / textSpeed);
        }
    }
}