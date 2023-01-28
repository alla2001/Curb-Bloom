using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenetrigger : MonoBehaviour
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneTransitionManager.Instance.LoadScene(scene);
    }
}