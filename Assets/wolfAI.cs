using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class wolfAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    public Vector3 dir;
    public bool moving;
    private bool attacked;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //
        moving = true;
        //SpeechBubbleAndShouting.instance.Speak("Oh NO ! , i need to run !!");
    }

    private void Attack()
    {
        attacked = true;
        agent.SetDestination((transform.position + new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), 0)) * 30);
        PlayerStateManager.instance.Degrade(0.4f);
        SpeechBubbleAndShouting.instance.Speak("AAGHH !!");
    }

    private void Update()

    {
        if (!attacked)
        {
            agent.SetDestination(PlayerAIMovement.instance.transform.position);
        }

        dir = agent.destination - transform.position;
        if (Vector3.Distance(transform.position, PlayerAIMovement.instance.transform.position) > 30)
        {
            Destroy(gameObject);
        }
        else if (!attacked && Vector3.Distance((Vector2)transform.position, (Vector2)PlayerAIMovement.instance.transform.position) < 0.24f)
        {
            Attack();
        }
        //if (dir.magnitude < 0.1f)
        //{
        //    dir = Vector3.zero;
        //}
    }
}