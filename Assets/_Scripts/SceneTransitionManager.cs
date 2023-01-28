using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }
    public GameObject Ui;
    public string LastScene;

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(Ui);
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if (Instance == null)

            Instance = this;
        else Destroy(this);
    }

    public void LoadScene(string Name)
    {
        LastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Name);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}