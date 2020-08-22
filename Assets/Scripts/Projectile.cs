using System;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float maxLifeTime = 10f;

    [NonSerialized] public Action<GameObject> collisionHandler;
    [NonSerialized] public Vector2 direction;

    private new Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;

        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionHandler?.Invoke(collision.gameObject);

        // todo: add destroy effects (particle, sound)
        Destroy(gameObject);
    }
}
