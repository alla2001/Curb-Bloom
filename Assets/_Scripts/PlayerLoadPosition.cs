using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoadPosition : MonoBehaviour
{
    public GameObject player;
    public Object ifScene;
    [SerializeField] private bool inside;
    public string speechOnSpawn;

    // Start is called before the first frame update
    private void Start()
    {
        if (ifScene != null && SceneTransitionManager.Instance != null && SceneTransitionManager.Instance.LastScene == ifScene.name)
        {
            if (MovementController.instance != null)
            {
                MovementController.instance.gameObject.transform.position = transform.position;
            }
        }
        else if (ifScene == null && MovementController.instance != null)
        {
            MovementController.instance.gameObject.transform.position = transform.position;
        }
        else
        {
            if (MovementController.instance == null)
            {
                Instantiate(player, transform.position, Quaternion.identity);
                MovementController.instance.gameObject.transform.position = transform.position;
            }
        }
        if (inside)
            PlayerStateManager.instance.outside = false;
        else
            PlayerStateManager.instance.outside = true;
        if (PlayerStateManager.instance.TimeleftOutSide < 300 - 200)
            SpeechBubbleAndShouting.instance.Speak(speechOnSpawn, 4);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}