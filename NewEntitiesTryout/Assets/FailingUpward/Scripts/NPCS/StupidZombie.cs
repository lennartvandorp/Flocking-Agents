using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StupidZombie : Zombie
{

    ZombieState idleState;

    ZombieState currentState;


    //private static Dictionary<int, StupidZombie> ColMap = new();

    // Start is called before the first frame update
    protected override void Start()
    {
        idleState = new IdleZombieState(this);
        currentState = idleState;

        base.Start();
    }

    private void Update()
    {
        currentState.UpdateZombie();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie")) { closeZombies.Add(other.attachedRigidbody); return; }
        if (other.CompareTag("Obstacle")) { closeObstacles.Add(other); return; }
        if (other.CompareTag("Leader")) { if (!leader) { leader = other.GetComponent<NavMeshAgent>(); } }
        if (other.CompareTag("Scent")) { scent = other.transform; }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie")) { closeZombies.Remove(other.attachedRigidbody); return; }
        if (other.CompareTag("Obstacle")) { closeObstacles.Remove(other); return; }
        if (other.CompareTag("Leader")) { if (leader == other.GetComponent<NavMeshAgent>()) { leader = null; } }
        if (other.CompareTag("Scent")) { if (other.transform == scent) { scent = null; } }
    }


}