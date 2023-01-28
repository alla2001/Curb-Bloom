using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ConvoManager : SingletonMonoBehaviour<ConvoManager>
{
    public GameObject convoUi;
    public TextMeshProUGUI RoseText;
    public TextMeshProUGUI FrogText;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;

    public Frog Frog;
    public FrogState convostate = ConvoManager.FrogState.nottalked;

    public enum FrogState
    {
        nottalked, talked, finshedtalking
    }

    public void EndConvo()
    {
        convoUi.SetActive(false);
        FrogText.text = "";
        RoseText.text = "";
        option1Text.text = "";
        option2Text.text = "";
    }

    public void ShowRoseText(string text)
    {
        convoUi.SetActive(true);
        RoseText.text = text;
        option1Text.text = "";

        FrogText.text = "";
        option2Text.text = "";
    }

    public void ShowFrogText(string text)
    {
        convoUi.SetActive(true);
        FrogText.text = text;
        RoseText.text = "";
        option1Text.text = "";
        option2Text.text = "";
    }

    public void ShowOptions(string option1, string option2)
    {
        RoseText.text = "";
        FrogText.text = "";
        convoUi.SetActive(true);
        option1Text.text = option1;
        option2Text.text = option2;
    }
}