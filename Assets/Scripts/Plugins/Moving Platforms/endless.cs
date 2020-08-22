using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endless : MonoBehaviour
{
    float moveSpeed = 3f;
    bool moveRight;

    void Update()
    {

        if (transform.position.x > 12)
            moveRight = false;
        if (transform.position.x < 7)
            moveRight = true;


        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}


