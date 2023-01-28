using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueTextSO", menuName = "Dialogue")]
public class DialogueTextSO : ScriptableObject
{
    public string Name = "NAME";
    public List<string> Text = new List<string>();
}