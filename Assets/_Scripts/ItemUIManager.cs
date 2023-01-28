using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public float margine = 10;
    private List<GameObject> tempUi = new List<GameObject>();

    private void Start()
    {
        int i = 0;
        foreach (ItemSo item in inventorySystem.instance.items)
        {
            GameObject tmp = Instantiate(itemPrefab, transform);
            tmp.GetComponent<RectTransform>().localPosition = Vector3.zero;
            tmp.GetComponentInChildren<ItemUI>().SetItem(item);

            float xPos = margine * i + tmp.GetComponent<RectTransform>().rect.width * i +
                tmp.GetComponent<RectTransform>().rect.width / 2 +
                margine - tmp.GetComponent<RectTransform>().rect.width * 4 * (int)(i / 4) - margine * 4 * (int)(i / 4);
            xPos -= GetComponent<RectTransform>().rect.width / 2;
            print(xPos);
            float yPos = -margine * (int)(i / 4) - tmp.GetComponent<RectTransform>().rect.height * (int)(i / 4) - margine - tmp.GetComponent<RectTransform>().rect.height / 2;
            yPos += GetComponent<RectTransform>().rect.height / 2;
            tmp.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            tempUi.Add(tmp);
            i++;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject gm in tempUi)
        {
            Destroy(gm);
        }
    }

    public void OnEnable()
    {
        int i = 0;
        foreach (ItemSo item in inventorySystem.instance.items)
        {
            GameObject tmp = Instantiate(itemPrefab, transform);
            tmp.GetComponent<RectTransform>().localPosition = Vector3.zero;
            tmp.GetComponentInChildren<ItemUI>().SetItem(item);

            float xPos = margine * i + tmp.GetComponent<RectTransform>().rect.width * i +
                tmp.GetComponent<RectTransform>().rect.width / 2 +
                margine - tmp.GetComponent<RectTransform>().rect.width * 4 * (int)(i / 4) - margine * 4 * (int)(i / 4);
            xPos -= GetComponent<RectTransform>().rect.width / 2;
            print(xPos);
            float yPos = -margine * (int)(i / 4) - tmp.GetComponent<RectTransform>().rect.height * (int)(i / 4) - margine - tmp.GetComponent<RectTransform>().rect.height / 2;
            yPos += GetComponent<RectTransform>().rect.height / 2;
            tmp.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            tempUi.Add(tmp);
            i++;
        }
    }
}