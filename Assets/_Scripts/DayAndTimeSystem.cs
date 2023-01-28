using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DayAndTimeSystem : SingletonMonoBehaviour<DayAndTimeSystem>
{
    public int currentDay = 0;
    public float TimeOutSide = 1;
    public GameObject ScreenCover;
    [SerializeField] private bool closed;
    private Vector3 closedPos;
    public float closeSpeed = 4f;
    public TextMeshProUGUI dayText;
    public UnityAction DayPassed;

    public void AdvanceDay()
    {
        currentDay += 1;
        MovementController.instance.GetComponent<PlayerStateManager>().Degrade(0.1f);
        dayText.text = "Day " + currentDay;

        StartCoroutine(ChangeDay());
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        closedPos = ScreenCover.transform.position;
    }

    private void Update()
    {
        if (closed)
        {
            ScreenCover.transform.position = Vector3.MoveTowards(ScreenCover.transform.position,
                closedPos,
                closeSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 tergetpos = closedPos - new Vector3(ScreenCover.GetComponent<RectTransform>().rect.width * 2, 0, 0);
            ScreenCover.transform.position = Vector3.MoveTowards(ScreenCover.transform.position, tergetpos,
                closeSpeed * Time.deltaTime
                );
        }
    }

    private IEnumerator ChangeDay()
    {
        closed = true;
        yield return new WaitForSeconds(3);
        DayPassed.Invoke();
        closed = false;
    }
}