using Pathfinding;

using UnityEngine;

public class GhostEnemy : EnemyBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float nextNodeDistance = 3f;
    [SerializeField] private float refreshRate = 0.5f;

    private Vector3 scale;
    private Vector3 iscale;

    private Path path;
    private int currentNode = 0;
    private bool reachedEnd = false;

    private Seeker seeker;
    private Transform graphics;
    private new Rigidbody2D rigidbody;

    private new void Start()
    {
        base.Start();

        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();

        graphics = transform.GetChild(0);

        scale = graphics.localScale;
        iscale = scale;
        iscale.x *= -1;

        InvokeRepeating("UpdatePath", 0, refreshRate);
    }

    private void UpdatePath()
    {
        if(!seeker.IsDone()) { return; }

        seeker.StartPath(rigidbody.position, playerTargetTransform.position, OnPathComplete);
    }

    private void FixedUpdate()
    {
        if(path == null) { return; }

        if(currentNode >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentNode] - rigidbody.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime * PlayerMotor.PHYSIC_MULTIPLIER;

        rigidbody.AddForce(force);

        float dist = Vector2.Distance(rigidbody.position, path.vectorPath[currentNode]);
        if(dist < nextNodeDistance)
        {
            currentNode++;
        }

        if (rigidbody.velocity.x >= 0.01f)
        {
            graphics.localScale = scale;
        }
        else if (rigidbody.velocity.x <= -0.01f)
        {
            graphics.localScale = iscale;
        }
    }

    private void OnPathComplete(Path path)
    {
        if(!path.error)
        {
            this.path = path;
            currentNode = 0;
        }
    }
}
