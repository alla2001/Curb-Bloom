using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeInteractable : Interactable
{
    public float degrade = 0.2f;
    private bool drankeToday = false;

    public override void OnInteract()
    {
        if (!drankeToday)
        {
            PlayerStateManager.instance.AddThirst(-0.2f);
            PlayerStateManager.instance.Degrade(degrade);
            base.OnInteract();
            drankeToday = true;
            SpeechBubbleAndShouting.instance.Speak("i Drank Enough for today", 5f);
        }
    }

    public override void OnHighLight(InteractionController player)
    {
        if (!drankeToday)
        {
            SpeechBubbleAndShouting.instance.Speak("the water does look so good , but i can still drink it", 5f);
            base.OnHighLight(player);
        }
    }

    public override void OnUnHighLight(InteractionController player)
    {
        if (!drankeToday)
        {
            SpeechBubbleAndShouting.instance.ShutUp();
            base.OnUnHighLight(player);
        }
    }
}