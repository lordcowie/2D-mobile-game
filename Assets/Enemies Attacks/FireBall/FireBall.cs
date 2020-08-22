using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Vector3 PlayerPos;
    public GameObject Player;
    private float FireBallSpeed = 0.1f;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPos = Player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,PlayerPos, FireBallSpeed);
        if(transform.position == PlayerPos)
        {
            Destroy(gameObject);
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
      
            Destroy(gameObject);
        
    }
    
}
