using Unity.Entities;

struct Config: IComponentData
{
    public Entity TankPrefab;
    public int tankCount;
    public float safeZoneRadius;
}