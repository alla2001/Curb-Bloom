using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerStateManager : SingletonMonoBehaviour<PlayerStateManager>
{
    [field: SerializeField, Range(0, 1)]
    public float Degradation = 0;

    [field: SerializeField, Range(0, 1)]
    public float Food = 1;

    [field: SerializeField, Range(0, 1)]
    public float water = 1;

    public State degradationState;

    public float dayDegradation = 0;

    [field: SerializeField, Range(0, 1)]
    public float playerTirdness = 1;

    public float TimeleftOutSide = 300;
    public bool outside;
    public InputAction openStateMenu;
    public GameObject stateMenu;

    public enum State
    {
        Perfect, Fine, ok, notok, bruh
    }

    private void Start()
    {
        TimeleftOutSide = 300;
        DayAndTimeSystem.instance.DayPassed += DayPassed;
        openStateMenu.Enable();
        openStateMenu.performed += OpenStateMenu;
        AnimationManager.instance.animator.SetInteger("DEG", (int)(Degradation / 0.2f));
    }

    private void OpenStateMenu(InputAction.CallbackContext i)
    {
        stateMenu.SetActive(!stateMenu.activeSelf);
    }

    public void AddHunger(float value)
    {
        Food = Mathf.Clamp(Food - value, 0, 1);
    }

    public void AddThirst(float value)
    {
        water = Mathf.Clamp(water - value, 0, 1);
    }

    public void AddTirdness(float value)
    {
        playerTirdness = Mathf.Clamp(playerTirdness - value, 0, 1);
    }

    private void SetDegradationState(State state)
    {
        degradationState = state;
    }

    public void Degrade(float value)
    {
        float ammount = Mathf.Clamp(value - dayDegradation, 0, 0.4f);
        dayDegradation += ammount;
        Degradation += ammount;
        Degradation = Mathf.Clamp(Degradation, 0, 1);
        MovementController.instance.speedMutlplier = Mathf.Clamp((1 - Degradation) * 1.75f, 0.35f, 1f);
        SetDegradationState((State)(int)(Degradation / 0.2f));
        AnimationManager.instance.animator.SetInteger("DEG", (int)(Degradation / 0.2f));
    }

    public void DayPassed()
    {
        TimeleftOutSide = 300;
        dayDegradation = 0;
        Degrade(0.1f);
        AddHunger(0.2f);
        AddThirst(0.3f);
        dayDegradation = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (outside)
        {
            TimeleftOutSide -= Time.deltaTime;
            if (TimeleftOutSide < -1)
            {
                SceneTransitionManager.Instance.LoadScene("basement");
                TimeleftOutSide = 0;
            }
        }
    }
}