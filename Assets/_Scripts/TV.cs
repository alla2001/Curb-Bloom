using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Hilighted
{
    public GameObject Light;

    public override void OnInteract()
    {
        if (!interacted)
        {
            interacted = true;
            Light.SetActive(true);
        }
        else
        {
            interacted = false;
            Light.SetActive(false);
        }
    }
}