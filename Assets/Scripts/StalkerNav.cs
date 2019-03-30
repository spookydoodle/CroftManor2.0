using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StalkerNav : MonoBehaviour
{
    public GameObject player;               // Reference to the player's position.
    public NavMeshAgent agent;               // Reference to the nav mesh agent.
    public Animator anim;
    public float stalkerRadius = 1.5f;
    private bool isStalking = true;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
    }



    void Update()
    {
        isStalking = IsStalking(agent, player, stalkerRadius);
        agent.enabled = isStalking;

        if (agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
        
        Animating(isStalking);

    }


    // returns false if distance is in radius = 1
    bool IsStalking(NavMeshAgent agent, GameObject player, float stalkerRadius)
    {
        return Vector3.Distance(agent.transform.position, player.transform.position) > stalkerRadius;
    }


    // Set parameters used in conditions of transitions in Animator component based on state id
    void Animating(bool isStalking)
    {
        anim.SetBool("IsWalking", isStalking);
    }
}
