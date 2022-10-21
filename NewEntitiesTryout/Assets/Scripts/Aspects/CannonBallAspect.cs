using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

readonly partial struct CannonBallAspect : IAspect
{
    public readonly Entity Self;

    readonly TransformAspect Transform;
    readonly RefRW<CannonBall> CannonBall;

    public float3 Position
    {
        get => Transform.Position;
        set => Transform.Position = value;
    }

    public float3 Speed
    {
        get => CannonBall.ValueRO.Speed;
        set => CannonBall.ValueRW.Speed = value;
    }
}