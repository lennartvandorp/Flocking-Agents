using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity;

[BurstCompile]
partial struct CannonBallJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ECB;
    public float DeltaTime;

    void Execute([ChunkIndexInQuery] int chunkIndex, ref CannonBallAspect cannonBall)
    {
        var gravity = new float3(0f, -9.81f, 0f);
        var invertY = new float3(1f, -1f, 1f);

        cannonBall.Position += cannonBall.Speed * DeltaTime;

        if (cannonBall.Position.y < 0f)
        {
            cannonBall.Position *= invertY;
            cannonBall.Speed *= invertY * .8f;

        }
        cannonBall.Speed += gravity * DeltaTime;

        var speed = math.lengthsq(cannonBall.Speed);
        if (speed < .1f)
        {
            ECB.DestroyEntity(chunkIndex, cannonBall.Self);
        }

    }
}

[BurstCompile]
partial struct CannonBallSystem : ISystem
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
        var cannonBallJob = new CannonBallJob
        {
            ECB = ecb.AsParallelWriter(),
            DeltaTime = SystemAPI.Time.DeltaTime
        };
        cannonBallJob.ScheduleParallel();
    }
}