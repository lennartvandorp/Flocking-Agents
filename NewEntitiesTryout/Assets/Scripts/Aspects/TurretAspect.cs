using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

readonly partial struct TurretAspect : IAspect
{
    readonly RefRO<Turret> m_turret;

    public Entity CannonBallSpawn => m_turret.ValueRO.CannonBallSpawn;
    public Entity CannonBallPrefab => m_turret.ValueRO.CannonBallPrefab;
}
