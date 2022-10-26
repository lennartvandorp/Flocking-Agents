using Unity.Entities;
using UnityEditor;
using UnityEngine;
using Unity.Transforms;
using Unity;
using Unity.VisualScripting;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Physics;


partial struct ZombieJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;
    public float DeltaTime;

    void Execute([ChunkIndexInQuery] int chunkIndex, ref ZombieAspect zombie, ref PhysicsVelocity vel)
    {
        Vector3 dir = Vector3.zero;
        dir +=  GetAvgSurroundingVel(ref zombie);

        zombie.Velocity = new float3(0f, 1f, 0f);

        zombie.Position += zombie.Velocity * DeltaTime;
    }

    Vector3 GetAvgSurroundingVel(ref ZombieAspect zombie)
    {
        Vector3 toReturn = Vector3.zero;
        if (!zombie.closeZombies.IsEmpty)
        {
            foreach (SurroundingsBufferElement zomb in zombie.closeZombies)
            {
                toReturn += zomb.velocity.normalized;
            }
        }
        return toReturn;
    }
}


[BurstCompile]
partial struct ZombieMovementSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state) { }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        var zombieJob = new ZombieJob
        {
            ECB = ecb.AsParallelWriter(),
            DeltaTime = SystemAPI.Time.DeltaTime
        };
        zombieJob.ScheduleParallel();
    }
}