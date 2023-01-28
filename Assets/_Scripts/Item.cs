using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Item : Hilighted
{
    public ItemSo itemSO;
    private SpriteRenderer renderer;
    public bool Unique;
    public Material outLine;
    private Material startmat;

    private void Awake()
    {
        startmat = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        //renderer.sprite = itemSO.image;
        if (Unique && inventorySystem.instance.HasItem(itemSO))
        {
            Destroy(gameObject);
        }
    }

    public override void OnHighLight(InteractionController player)
    {
        player.EnableButton("Pick UP");
        base.OnHighLight(player);
        GetComponent<SpriteRenderer>().material = outLine;
    }

    public override void OnUnHighLight(InteractionController player)
    {
        GetComponent<SpriteRenderer>().material = startmat;
        base.OnUnHighLight(player);
    }

    public override void OnInteract()
    {
        if (!inventorySystem.instance.AddItem(itemSO))
        {
            SpeechBubbleAndShouting.instance.Speak("i cant take it, no space");
        }

        base.OnInteract();
        Destroy(gameObject);
    }

    public virtual void Use()
    {
    }
}