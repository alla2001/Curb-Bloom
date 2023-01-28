using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public InputAction space;

    // Start is called before the first frame update
    private void Start()
    {
        space.Enable();
        space.performed += LoadScene;
    }

    public void LoadScene(InputAction.CallbackContext i)
    {
        space.Disable();
        SceneManager.LoadScene(firstLevel);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}