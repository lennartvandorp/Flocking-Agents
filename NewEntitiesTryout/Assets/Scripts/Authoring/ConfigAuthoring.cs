using Unity.Entities;
using UnityEngine;

class ConfigAuthoring : MonoBehaviour
{
    public GameObject TankPrefab;
    public int TankCount;
    public float SafeZoneRadius;

}

class ConfigBaker : Baker<ConfigAuthoring>
{
    public override void Bake(ConfigAuthoring authoring)
    {
        AddComponent(new Config
        {
            TankPrefab = GetEntity(authoring.TankPrefab),
            tankCount = authoring.TankCount,
            safeZoneRadius = authoring.SafeZoneRadius
        });
    }
}