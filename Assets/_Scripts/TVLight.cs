using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVLight : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f));
        sprite.color = new Color(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), 1);

        StartCoroutine(ChangeColor());
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeColor());
    }
}