using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Page", fileName = "paeg", order = 0)]
public class Page : ScriptableObject
{
    [TextArea]
    public string text;
}