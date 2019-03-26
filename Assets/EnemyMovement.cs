using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;               // Reference to the player's position.
    //PlayerHealth playerHealth;      // Reference to the player's health.
    //EnemyHealth enemyHealth;        // Reference to this enemy's health.

    public NavMeshAgent agent;               // Reference to the nav mesh agent.
    public Animator anim;

    void Start()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        //NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
    }

    void Awake()
    {
        //Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();


    }


    void Update()
    {
        //// If the enemy and the player have health left...
        //if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
        //    // ... set the destination of the nav mesh agent to the player.
        agent.SetDestination(player.position);
        Animating();
        //}
        //// Otherwise...
        //else
        //{
        //    // ... disable the nav mesh agent.
        //    nav.enabled = false;
        //}
    }

    // Set parameters used in conditions of transitions in Animator component
    void Animating()
    {
        anim.SetBool("IsRunning", true);
    }
}
