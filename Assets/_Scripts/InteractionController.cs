using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InteractionController : SingletonMonoBehaviour<InteractionController>
{
    public float InteractionDistance;
    public Interactable currentInteractable;
    public InputAction interact;
    [SerializeField] private GameObject button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Start()
    {
        interact.Enable();
        interact.started += Interact;
    }

    public void EnableButton(string text)
    {
        buttonText.text = text;
        button.SetActive(true);
    }

    public void EnableButton()
    {
        buttonText.text = "Interact";
        button.SetActive(true);
    }

    public void DisableButton()
    {
        button.SetActive(false);
    }

    private void Interact(InputAction.CallbackContext i)
    {
        if (currentInteractable != null)
            currentInteractable.OnInteract();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable" && collision.GetComponent<Interactable>() != null)
        {
            if (currentInteractable != null)
            {
                if (Vector2.Distance(currentInteractable.transform.position, transform.position)
                    > Vector2.Distance(collision.transform.position, transform.position))
                {
                    currentInteractable.OnUnHighLight(this);
                    currentInteractable = collision.GetComponent<Interactable>();
                    currentInteractable.OnHighLight(this);
                }
            }
            else
            {
                currentInteractable = collision.GetComponent<Interactable>();
                currentInteractable.OnHighLight(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable" && collision.GetComponent<Interactable>() == currentInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnUnHighLight(this);
                currentInteractable.OnQuit();
            }

            currentInteractable = null;
        }
    }
}