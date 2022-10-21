using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

struct CannonBall : IComponentData
{
    public float3 Speed;
}
