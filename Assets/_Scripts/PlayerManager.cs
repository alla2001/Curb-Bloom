using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    public int orderLayer;

    // Start is called before the first frame update
    private void Start()
    {
        orderLayer = GetComponent<SpriteRenderer>().sortingOrder;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}