using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;               // Reference to the player's position.
    public NavMeshAgent agent;               // Reference to the nav mesh agent.
    public Animator anim;

    void Start()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
    }



    void Update()
    {
        agent.enabled = true;
        agent.SetDestination(player.transform.position);
        Debug.Log(agent.destination);
        Animating();

    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating()
    {
        anim.SetBool("IsRunning", true);
    }
}
