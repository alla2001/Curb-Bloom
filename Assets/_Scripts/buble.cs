using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buble : Interactable
{
    public GameObject bubble;

    public override void OnHighLight(InteractionController player)
    {
        bubble.SetActive(true);
    }

    public override void OnUnHighLight(InteractionController player)
    {
        bubble.SetActive(false);
    }
}