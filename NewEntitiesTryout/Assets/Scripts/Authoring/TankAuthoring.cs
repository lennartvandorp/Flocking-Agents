using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

class TankAuthoring : MonoBehaviour { }


class TankBaker : Baker<TankAuthoring>
{
    public override void Bake(TankAuthoring authoring)
    {
        AddComponent<Tank>();
    }
}