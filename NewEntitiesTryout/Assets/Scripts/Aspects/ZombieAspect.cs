using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

readonly partial struct ZombieAspect : IAspect
{
    public readonly Entity Self;

    readonly TransformAspect Transform;
    readonly RefRW<EZombie> Zombie;



    public Vector3 Position
    {
        get => Transform.Position;
        set => Transform.Position = value;
    }

    public Vector3 Velocity
    {
        get => Zombie.ValueRO.velocity;
        set => Zombie.ValueRW.velocity = value;
    }

    public float3 Speed
    {
        get => Zombie.ValueRO.Speed;
        set => Zombie.ValueRW.Speed = value;
    }

    public DynamicBuffer<SurroundingsBufferElement> closeZombies
    {
        get => Zombie.ValueRO.surroundingsData;
    }

    public DynamicBuffer<CloseWallsBufferElement> closeWalls
    {
        get => Zombie.ValueRO.closeWallsData;
    }
}