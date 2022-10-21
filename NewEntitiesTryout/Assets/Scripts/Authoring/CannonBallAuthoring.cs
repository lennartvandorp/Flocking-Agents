using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

class CannonBallAuthoring: MonoBehaviour
{

}

class CannonBallBaker : Baker<CannonBallAuthoring>
{
    public override void Bake(CannonBallAuthoring authoring)
    {
        AddComponent<CannonBall>();
    }
}