using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NomeRunAround : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    public NavMeshAgent agent;               // Reference to the nav mesh agent.
    public Animator anim;
    public float stalkerRadius = 1.5f;
    private bool isWalking = false;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private float timer;

    private Vector3 prevPos;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }


    //void Start()
    //{
    //    Animator anim = gameObject.GetComponent<Animator>();
    //    //NavMeshAgent agent = GetComponent<NavMeshAgent>();

    //    // Disabling auto-braking allows for continuous movement
    //    // between points (ie, the agent doesn't slow down as it
    //    // approaches a destination point).
    //    agent.autoBraking = false;

    //    GotoNextPoint();
    //}



    void Update()
    {
        //isStalking = IsStalking(agent, player, stalkerRadius);
        //agent.enabled = isStalking;

        //if (agent.enabled)
        //{
        //    agent.SetDestination(player.transform.position);
        //}

        //Animating(isStalking);


        // Choose the next destination point when the agent gets
        // close to the current one.
        //if (!agent.pathPending && agent.remainingDistance < 0.5f)
        //    GotoNextPoint();

        if(prevPos != agent.transform.position)
        {
            isWalking = true;
        } 
        else
        {
            isWalking = false;
        }

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        prevPos = agent.transform.position;

        Animating(isWalking);

    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    //void GotoNextPoint()
    //{
    //    // Returns if no points have been set up
    //    if (points.Length == 0)
    //        return;

    //    // Set the agent to go to the currently selected destination.
    //    agent.destination = points[destPoint].position;

    //    // Choose the next point in the array as the destination,
    //    // cycling to the start if necessary.
    //    destPoint = (destPoint + 1) % points.Length;
    //}


    //// returns false if distance is in radius = 1
    //bool IsStalking(NavMeshAgent agent, GameObject player, float stalkerRadius)
    //{
    //    return Vector3.Distance(agent.transform.position, player.transform.position) > stalkerRadius;
    //}


    // Set parameters used in conditions of transitions in Animator component based on state id
    void Animating(bool isWalking)
    {
        anim.SetBool("IsWalking", isWalking);
    }
}
