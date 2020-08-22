using Pathfinding;

using UnityEngine;

public class EnemyGraphics : MonoBehaviour
{
    private AIPath aiPath;

    private Vector3 scale;
    private Vector3 iscale;

    private void Start()
    {
        aiPath = GetComponentInParent<AIPath>();

        scale = transform.localScale;
        iscale = scale;
        iscale.x *= -1;
    }

    private void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = scale;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = iscale;
        }
    }
}
