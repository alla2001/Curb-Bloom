using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySystem : MonoBehaviour
{
    [field: SerializeField] public List<ItemSo> items { get; private set; }

    [SerializeField] private int MaxItems;
    public static inventorySystem instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    public bool HasItem(ItemSo item)
    {
        foreach (ItemSo _item in items)
        {
            if (item == _item)
                return true;
        }
        return false;
    }

    public bool AddItem(ItemSo addedItem)
    {
        if (items.Count < MaxItems && !HasItem(addedItem))
        {
            items.Add(addedItem);
            return true;

            //itemUIs[items.Count - 1].SetItem(addedItem);
        }
        return false;
    }

    public bool removeItem(ItemSo addedItem)
    {
        if (items.Count > 0 && HasItem(addedItem))
        {
            items.RemoveAt(items.Count - 1);
            return true;
            //itemUIs[items.Count - 1].ClearItem();
        }
        return false;
    }
}