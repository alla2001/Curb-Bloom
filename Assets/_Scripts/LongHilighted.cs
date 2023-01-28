using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongHilighted : Interactable
{
    public List<string> text = new List<string>();
    public bool hilighOnce = true;

    public override void OnInteract()
    {
        if (!interacted && Showbubbles)
        {
            SpeechBubbleAndShouting.instance.SpeakList(text, this);
            interacted = true;
            base.OnInteract();
        }
    }

    public override void OnHighLight(InteractionController player)
    {
        base.OnHighLight(player);
    }

    public override void OnUnHighLight(InteractionController player)
    {
        if (Showbubbles)
        {
            SpeechBubbleAndShouting.instance.ShutUp();
            if (hilighOnce && interacted) Destroy(this);
            interacted = false;
            base.OnUnHighLight(player);
        }
    }
}