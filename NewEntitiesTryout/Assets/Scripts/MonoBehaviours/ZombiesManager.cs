using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public class ZombiesManager : MonoBehaviour
{
    public static DynamicBuffer<TransformAspect> zombiesList;
    public static Entity entity;
    EntityManager entityManager;
    // Start is called before the first frame update
    void Start()
    { 
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //zombiesList = entityManager.GetBuffer<TransformAspect>(entity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
