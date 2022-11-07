using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [HideInInspector] protected NavMeshAgent agent;
    [HideInInspector] public Rigidbody rb;

    public float moveSpeed = 5f;
    public float acc = 2f;
    protected float timeSinceUpdate;


    [HideInInspector] public List<Rigidbody> closeZombies;
    [HideInInspector] public List<Collider> closeObstacles;
    [HideInInspector] public Transform scent;

    [HideInInspector] public float conhesionStrength { get { return GameManager.Instance.CohesionStrength; } }
    [HideInInspector] public float obstacleAvoidanceStrength { get { return GameManager.Instance.ObstacleAvoidanceStrength; } }
    [HideInInspector] public float separationStrength { get { return GameManager.Instance.SeparationStrength; } }
    [HideInInspector] public float alignmentStrength { get { return GameManager.Instance.AlignmentStrength; } }
    [HideInInspector] public float followLeaderStrength { get { return GameManager.Instance.FollowLeaderStrength; } }
    [HideInInspector] public float obstacleAvoidanceDist { get { return GameManager.Instance.ObstacleAvoidanceDist; } }
    [HideInInspector] public float scentStrength { get { return GameManager.Instance.ObstacleAvoidanceDist; } }



    [HideInInspector] public PlayerSenses senses;

    [HideInInspector] public NavMeshAgent leader;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        closeZombies = new List<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * moveSpeed;
        closeObstacles = new List<Collider>();
        senses = GetComponent<PlayerSenses>();
    }

    private void Update()
    {
        timeSinceUpdate += Time.deltaTime;
    }

    public virtual void UpdateZombie()
    {
        timeSinceUpdate = 0f;
    }

}
