using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerAIMovement : SingletonMonoBehaviour<PlayerAIMovement>
{
    public Transform target;
    public NavMeshAgent agent;
    [HideInInspector] public List<CutScene> playedCutScenes = new List<CutScene>();
    public GameObject spaceBar;
    public InputAction skip;

    // Start is called before the first frame update
    public bool moving;

    public Vector2 dir;
    public CutSceneZone CutSceneOwner;
    private int index = 0;
    [HideInInspector] public List<Point> points = new List<Point>();
    private bool waiting;
    private Point currentPoint;

    [System.Serializable]
    public class Point
    {
        public Transform target;
        public float WaitTime;
        public string SpeechOnReach;
    }

    private bool skipped;

    private void Start()
    {
        skip.Enable();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = false;
        skip.performed += SkipCutScene;
        // StartCutScene();
    }

    public void StartCutScene(CutScene cutScene, CutSceneZone zone)
    {
        index = 0;
        CutSceneOwner = zone;
        if (points.Count > 0 && moving == false)
        {
            spaceBar.SetActive(true);
            playedCutScenes.Add(cutScene);
            Interactable.Showbubbles = false;
            agent.enabled = true;
            currentPoint = points[index];
            agent.SetDestination(points[index].target.position);
            MovementController.instance.freez = true;
            moving = true;
        }
    }

    private void SkipCutScene(InputAction.CallbackContext i)
    {
        PidgeonCutScene pc = CutSceneOwner as PidgeonCutScene;
        if (pc != null && pc.ShowedLetter && SpeechBubbleAndShouting.instance.letter.active)
        {
            SpeechBubbleAndShouting.instance.HideLetter();
        }
        else
        {
            if (agent.enabled)
            {
                DebugStuff.ConsoleToGUI.instance.Log("SPACE", "", LogType.Log);
                skipped = true;
                agent.SetDestination(points[points.Count - 1].target.position);
            }
        }
    }

    public void NextPosScene()
    {
        index++;
        PidgeonCutScene pc = CutSceneOwner as PidgeonCutScene;
        if (pc != null && pc.letterIndex == index && !pc.ShowedLetter)
        {
            SpeechBubbleAndShouting.instance.ShowLetter();
            pc.ShowedLetter = true;
            index--;
            waiting = false;
            return;
        }
        if (pc != null)
        {
            if (pc.letterIndex == index)
            {
                if (pc.ShowedLetter && !SpeechBubbleAndShouting.instance.letter.active)
                {
                    if (index < points.Count)
                    {
                        agent.SetDestination(points[index].target.position);
                        currentPoint = points[index];

                        moving = true;
                    }
                    else
                    {
                        spaceBar.SetActive(false);
                        Interactable.Showbubbles = true;
                        agent.isStopped = true;
                        agent.enabled = false;
                        points.Clear();
                        moving = false;
                        MovementController.instance.freez = false;
                    }
                }
                else
                {
                    waiting = false;
                }
            }
            else
            {
                if (index < points.Count)
                {
                    agent.SetDestination(points[index].target.position);
                    currentPoint = points[index];

                    moving = true;
                }
                else
                {
                    spaceBar.SetActive(false);
                    Interactable.Showbubbles = true;
                    agent.isStopped = true;
                    agent.enabled = false;
                    points.Clear();
                    moving = false;
                    MovementController.instance.freez = false;
                }
            }
        }
        else
        {
            if (index < points.Count)
            {
                agent.SetDestination(points[index].target.position);
                currentPoint = points[index];

                moving = true;
            }
            else
            {
                spaceBar.SetActive(false);
                Interactable.Showbubbles = true;
                agent.isStopped = true;
                agent.enabled = false;
                points.Clear();
                moving = false;
                MovementController.instance.freez = false;
            }
        }
    }

    private IEnumerator Wait(float time)
    {
        waiting = true;
        yield return new WaitForSeconds(time);
        NextPosScene();
        waiting = false;
        SpeechBubbleAndShouting.instance.ShutUp();
    }

    // Update is called once per frame
    private void Update()
    {
        if (agent.enabled)
        {
            if (skipped && !agent.pathPending)
            {
                transform.position = agent.pathEndPosition;
                agent.isStopped = true;
                spaceBar.SetActive(false);
                Interactable.Showbubbles = true;
                agent.isStopped = true;

                points.Clear();
                moving = false;
                MovementController.instance.freez = false;
                agent.enabled = false;
                skipped = false;
            }
            dir = agent.destination - transform.position;
            if (dir.magnitude < 0.1f)
            {
                dir = Vector3.zero;
            }
            if (agent.remainingDistance < 0.1f && !waiting)
            {
                if (currentPoint.SpeechOnReach != null && currentPoint.SpeechOnReach != "")
                {
                    SpeechBubbleAndShouting.instance.Speak(currentPoint.SpeechOnReach);
                }
                StartCoroutine(Wait(currentPoint.WaitTime));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, dir);
    }
}