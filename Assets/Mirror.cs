using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Interactable
{
    private bool usedToday;

    private void Start()
    {
        DayAndTimeSystem.instance.DayPassed += DayPassed;
    }

    private void DayPassed()
    {
        usedToday = false;
    }

    public override void OnInteract()
    {
        if (
        !usedToday)
        {
            usedToday = true;
            ReflectionManager.instance.ShowReflection();
            base.OnInteract();
        }
    }
}