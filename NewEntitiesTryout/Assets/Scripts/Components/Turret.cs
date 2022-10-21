using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

struct Turret : IComponentData
{
    public Entity CannonBallSpawn;

    public Entity CannonBallPrefab;
}
