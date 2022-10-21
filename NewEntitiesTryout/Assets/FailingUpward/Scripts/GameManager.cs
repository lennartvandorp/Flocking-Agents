using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform followPosition;

    [Header("Zombie settings")]
    [SerializeField] float cohesionStrength;
    [HideInInspector] public float CohesionStrength { get { return cohesionStrength; } }

    [SerializeField] float obstacleAvoidanceStrength;
    [HideInInspector] public float ObstacleAvoidanceStrength { get { return obstacleAvoidanceStrength; } }

    [SerializeField] float obstacleAvoidanceDist;
    [HideInInspector] public float ObstacleAvoidanceDist { get { return obstacleAvoidanceDist; } }

    [SerializeField] float separationStrength;
    [HideInInspector] public float SeparationStrength { get { return separationStrength; } }


    [SerializeField] float alignmentStrength;
    [HideInInspector] public float AlignmentStrength { get { return alignmentStrength; } }

    [SerializeField] float followLeaderStrength;
    [HideInInspector] public float FollowLeaderStrength { get { return followLeaderStrength; } }


    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                Debug.LogError("There is no GameManager in the scene instantiated");
                return null;
                instance = new GameManager();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] public InputSetup setup;
}
