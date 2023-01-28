using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public ItemSo item;
    public Image UIImage;
    public TextMeshProUGUI UIText;

    public void SetItem(ItemSo item)
    {
        this.item = item;
        UIImage.sprite = item.image;
        UIText.text = item.name;
    }

    public void ClearItem()
    {
        this.item = null;
        UIImage.sprite = null;
    }
}