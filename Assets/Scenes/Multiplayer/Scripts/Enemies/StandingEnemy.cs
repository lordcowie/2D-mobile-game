#pragma warning disable 0649

using UnityEngine;

using Random = UnityEngine.Random;

public class StandingEnemy : EnemyBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float shootingRate = 1f;
    [SerializeField] private float damages = 20f;
    [SerializeField] private float randomDamages = 0f;
    [SerializeField] private float randomStartTime = 2f;
    [SerializeField] private float minRange = 5f;

    private const float TARGET_RADIUS = 0.75f;

    private new void Start()
    {
        base.Start();

        InvokeRepeating("Shoot", Random.Range(0, randomStartTime), shootingRate);
    }

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) { return; }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, playerTargetTransform.position);
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.DrawWireSphere(playerTargetTransform.position, TARGET_RADIUS);
    }

    private void Shoot()
    {
        if(playerStats.Dead)
        {
            enabled = false;
            return;
        }

        if(Vector2.Distance(transform.position, playerTargetTransform.position) > minRange + TARGET_RADIUS) { return; }

        Projectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity, null);

        projectile.collisionHandler = HandleCollision;
        projectile.direction = playerTargetTransform.position - firePoint.position;
        projectile.direction.Normalize();
    }

    private void HandleCollision(GameObject collision)
    {
        if (collision.GetComponentInParent<PlayerStats>() != null)
        {
            playerStats.ApplyDamages(damages + Random.Range(-randomDamages, randomDamages));
        }
    }
}
