using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hilighted : Interactable
{
    public string HilightSpeech;
    public float HilightDuration = 3f;

    public override void OnHighLight(InteractionController player)
    {
        if (Showbubbles)
        {
            SpeechBubbleAndShouting.instance.Speak(HilightSpeech, HilightDuration);
            base.OnHighLight(player);
        }
    }

    public override void OnUnHighLight(InteractionController player)
    {
        if (Showbubbles)
        {
            SpeechBubbleAndShouting.instance.ShutUp();
            base.OnUnHighLight(player);
        }
    }
}