using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class TurretAuthoring : MonoBehaviour
{
    public GameObject CannonBallPrefab;
    public Transform CannonBallSpawn;
}

class TurretBaker : Baker<TurretAuthoring>
{
    public override void Bake(TurretAuthoring authoring)
    {
        AddComponent(new Turret
        {
            CannonBallPrefab = GetEntity(authoring.CannonBallPrefab),
            CannonBallSpawn = GetEntity(authoring.CannonBallSpawn)
        });
    }
}