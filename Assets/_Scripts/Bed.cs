using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public override void OnInteract()
    {
        if (PlayerStateManager.instance.TimeleftOutSide < 300 - 200)
        {
            base.OnInteract();
            DayAndTimeSystem.instance.AdvanceDay();
        }
        else
        {
            SpeechBubbleAndShouting.instance.Speak("am not tired enough ,i need to go outside");
        }
    }
}