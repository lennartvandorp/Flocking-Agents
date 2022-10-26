using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

struct EZombie : IComponentData
{
    public Vector3 position;
    public Vector3 velocity;

    public DynamicBuffer<SurroundingsBufferElement> surroundingsData;
    public DynamicBuffer<CloseWallsBufferElement> closeWallsData;
}

public struct SurroundingsBufferElement : IBufferElementData
{
    public Vector3 velocity;
    public Vector3 position;
}

public struct CloseWallsBufferElement : IBufferElementData
{
    public Vector3 closestPos;
}