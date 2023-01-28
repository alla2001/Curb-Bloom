using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LayeredProp : MonoBehaviour
{
    public bool invert;
    public Transform switchPoint;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (switchPoint == null) switchPoint = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerManager.instance.transform.position.y > switchPoint.position.y)
        {
            sr.sortingOrder = (PlayerManager.instance).orderLayer + (1 - 2 * Convert.ToInt32(invert));
        }
        else
        {
            sr.sortingOrder = (PlayerManager.instance).orderLayer + (-1 + 2 * Convert.ToInt32(invert));
        }
    }
}