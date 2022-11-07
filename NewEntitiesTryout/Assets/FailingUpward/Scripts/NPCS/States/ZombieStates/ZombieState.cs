using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState
{
    protected Zombie parent;
    public virtual void UpdateZombie(float timeSinceUpdate) { }
    public virtual void PhysicsUpdate() { }
    public virtual void DrawGizmos() { }
}
