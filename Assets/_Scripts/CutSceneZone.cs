using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneZone : MonoBehaviour
{
    public List<PlayerAIMovement.Point> points = new List<PlayerAIMovement.Point>();
    private bool triggred;
    public CutScene cutScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PlayerAIMovement.instance.playedCutScenes.Contains(cutScene))
        {
            PlayerAIMovement.instance.points = points;
            PlayerAIMovement.instance.StartCutScene(cutScene, this);
        }
    }
}