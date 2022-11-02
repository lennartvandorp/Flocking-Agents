using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class IdleZombieState : ZombieState
{

    public IdleZombieState(Zombie _parent)
    {
        parent = _parent;
    }

    Vector3 moveDir = Vector3.zero;

    public override void UpdateZombie()
    {
        moveDir = (
            GetAverageSurroundingVel() * parent.conhesionStrength
            + GoAwayFromObstacles() * parent.obstacleAvoidanceStrength
            + GoAwayFromZombies() * parent.separationStrength
            + GoToZombies() * parent.alignmentStrength
            + FollowLeader() * parent.followLeaderStrength
            + FollowScent() * parent.scentStrength
            ) * parent.acc
            + GoUpHill()
            ;

        GoInDirection(moveDir);
        base.UpdateZombie();
    }

    /// <summary>
    /// Accelerates the agent towards a direction, but makes sure the agent doesnt go faster than a certain speed. 
    /// </summary>
    /// <param name="dir"></param>
    void GoInDirection(Vector3 dir)
    {
        if (new Vector2(parent.rb.velocity.x, parent.rb.velocity.z).magnitude > parent.moveSpeed &&// to make sure the agent doesn't go faster than max speed
        Vector3.Dot(parent.rb.velocity.normalized, dir.normalized) > 0)
        {
            Vector3 adjustedMoveDir = dir.normalized - Vector3.Dot(parent.rb.velocity.normalized, dir.normalized) * parent.rb.velocity;
            parent.rb.AddForce(new Vector3(adjustedMoveDir.x, parent.rb.velocity.y, adjustedMoveDir.z));
        }
        else
        {
            parent.rb.AddForce(new Vector3(moveDir.x, parent.rb.velocity.y, moveDir.z) * Time.deltaTime * 150f);
        }
    }

    Vector3 GoAwayFromObstacles()
    {
        Vector3 averageDir = Vector3.zero;
        foreach (Collider o in parent.closeObstacles)
        {
            Vector3 closestPointDir = o.ClosestPointOnBounds(parent.transform.position) - parent.transform.position;
            if (closestPointDir.magnitude < parent.obstacleAvoidanceDist)
            {
                averageDir -= (closestPointDir).normalized / closestPointDir.magnitude;

            }
        }
        return averageDir.normalized;
    }

    Vector3 GoAwayFromZombies()
    {
        Vector3 averageDir = Vector3.zero;
        foreach (Rigidbody z in parent.closeZombies)
        {
            Vector3 closestPointDir = z.transform.position - parent.transform.position;
            averageDir -= (closestPointDir).normalized / closestPointDir.magnitude;
        }
        return averageDir.normalized;
    }

    Vector3 GoToZombies()
    {
        Vector3 averageDir = Vector3.zero;
        foreach (Rigidbody z in parent.closeZombies)
        {
            Vector3 closestPointDir = z.transform.position - parent.transform.position;
            averageDir += (closestPointDir).normalized;
        }
        return averageDir.normalized;
    }

    Vector3 FollowLeader()
    {
        if (parent.leader)
        {
            Vector3 toReturn = Vector3.zero;

            toReturn = parent.leader.velocity;

            return toReturn;
        }
        return Vector3.zero;
    }

    Vector3 FollowScent()
    {
        if (parent.scent)
        {
            if (!ScentObstructedByWall())
            {
                if (Vector3.Dot(parent.scent.forward, parent.scent.forward - parent.transform.position) < 0)//If haven't passed the scent point yet. 
                {
                    return (parent.scent.forward + (parent.scent.position - parent.transform.position).normalized).normalized;
                }

                return parent.scent.forward;
            }
        }
        return Vector3.zero;
    }

    bool ScentObstructedByWall()
    {
        RaycastHit[] hits;
        Vector3 scentDir = parent.scent.position - parent.transform.position;
        hits = Physics.RaycastAll(parent.transform.position, scentDir, scentDir.magnitude);

        bool toReturn = false;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Obstacle"))
            {
                toReturn = true;
            }
        }

        if (toReturn)
        {
            Debug.DrawRay(parent.transform.position, scentDir, Color.red);
        }
        else { Debug.DrawRay(parent.transform.position, scentDir, Color.green); }
        return toReturn;
    }


    /// <summary>
    /// Negates the force pushing the character downhill
    /// </summary>
    /// <returns></returns>
    Vector3 GoUpHill()
    {
        Vector3 gravityF = Physics.gravity * parent.rb.mass;
        Vector3 groundNormal = parent.senses.GroundNormal();
        return new Vector3(-groundNormal.x, 0f, -groundNormal.z) * gravityF.magnitude;
    }

    Vector3 GetAverageSurroundingVel()
    {
        Vector3 avgVel = new Vector3(0f, 0f, 0f);
        foreach (Rigidbody r in parent.closeZombies)
        {
            avgVel += r.velocity;
        }
        if (parent.closeZombies.Count == 0)
        {
            return Vector3.zero;
        }
        return avgVel.normalized;
    }
}
