using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : SingletonMonoBehaviour<DontDestroyOnLoad>
{
    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}