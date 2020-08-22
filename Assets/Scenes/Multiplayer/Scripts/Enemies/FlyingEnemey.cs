using UnityEngine;

using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
public class FlyingEnemey : EnemyBehaviour
{
    private new void Start()
    {
        base.Start();

        GetComponent< AIDestinationSetter>().target = playerTargetTransform;
    }
}
