using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected float health = 100; // %

    protected PlayerController player;
    protected PlayerStats playerStats;

    protected Transform playerTransform;
    protected Transform playerTargetTransform;

    public float Health { get => health; }

    protected void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerStats = player.GetComponent<PlayerStats>();

        playerTransform = player.transform;
        playerTargetTransform = playerTransform.Find("Enemy Target");
    }

    public void TakeDamages(float damages)
    {
        health -= damages;

        if (health <= 0)
        {
            Debug.Log("Enemy dead");

            // Todo: handle death
        }
    }
}
