using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayOnWall : MonoBehaviour
{
    public List<GameObject> lines;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            if (i < DayAndTimeSystem.instance.currentDay)
                lines[i].SetActive(true);
            else
                lines[i].SetActive(false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}