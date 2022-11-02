using Unity.Entities;
using UnityEditor;
using UnityEngine;
using Unity.Transforms;
using Unity;
using Unity.VisualScripting;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Random = Unity.Mathematics.Random;

partial struct ZombieJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;
    public float DeltaTime;

    float timeSinceLastTurn;
    void Execute([ChunkIndexInQuery] int chunkIndex, ref ZombieAspect zombie, ref PhysicsVelocity vel)
    {
        zombie.timeSinceTurn += DeltaTime;
        Debug.Log(zombie.timeSinceTurn);


        if (zombie.timeSinceTurn > 2f)
        {
            //vel.Linear = random.NextFloat3(-1f, 1f) * 1000f;
            zombie.timeSinceTurn -= 2f;
        }


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
struct Trigger : ITriggerEventsJob
{
    public void Execute(TriggerEvent evt)
    {
        Entity A = evt.EntityB;
        Debug.Log("Hey!");
    }
}

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(ExportPhysicsWorld))]
[UpdateBefore(typeof(PhysicsSystemGroup))]

[BurstCompile]
partial struct ZombieMovementSystem : ISystem
{
    BuildPhysicsWorld m_BuildPhysicsWorldSystem;
    PhysicsWorld m_PhysicsWorld;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // m_PhysicsWorld = World.GetOrCreateSystem<PhysicsWorld>();
    }

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

        var triggerJob = new Trigger();
        //Dependency = triggerJob.Schedule(m_PhysicsWorld, Dependency);
    }
}