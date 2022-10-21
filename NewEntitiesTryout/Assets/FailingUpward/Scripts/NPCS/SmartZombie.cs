using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartZombie : Zombie
{

    [SerializeField] float lookDist;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if ((transform.position - GameManager.Instance.followPosition.position).magnitude < lookDist)
        {
            agent.SetDestination(GameManager.Instance.followPosition.position);
        }
    }
}
