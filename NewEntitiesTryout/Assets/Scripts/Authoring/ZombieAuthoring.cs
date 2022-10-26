using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ZombieAuthoring : MonoBehaviour
{
    [HideInInspector] public EZombie zombieData;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = zombieData.velocity;
        zombieData.position = transform.position;
    }
}

class ZombieBaker : Baker<ZombieAuthoring>
{
    public override void Bake(ZombieAuthoring authoring)
    {
        authoring.zombieData = new EZombie();  
        AddComponent(authoring.zombieData);
    }
}