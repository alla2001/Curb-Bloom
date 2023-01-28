using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool interacted;
    public bool showButton = true;
    public static bool Showbubbles = true;

    public virtual void OnHighLight(InteractionController player)
    { if (showButton) player.EnableButton(); }

    public virtual void OnUnHighLight(InteractionController player)
    { if (showButton) player.DisableButton(); }

    public virtual void OnInteract()
    { }

    public virtual void OnQuit()
    { }
}