using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class JournalManager : MonoBehaviour
{
    public InputAction Read;
    public ItemSo jurnalSo;
    public GameObject juranlObject;
    public List<Page> pages;
    public TextMeshProUGUI text;
    private int currentPage;

    private void Start()
    {
        Read.Enable();
        Read.performed += ReadJournal;
    }

    public void ReadJournal(InputAction.CallbackContext i)
    {
        if (inventorySystem.instance.HasItem(jurnalSo))
        {
            text.text = pages[currentPage].text;
            juranlObject.SetActive(!juranlObject.activeSelf);
        }
    }

    public void NextPage()
    {
        print("next");
        if (currentPage < pages.Count - 1)
        {
            currentPage++;
            text.text = pages[currentPage].text;
        }
    }

    public void PrevPage()
    {
        if (currentPage >= 1)
        {
            currentPage--;
            text.text = pages[currentPage].text;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}