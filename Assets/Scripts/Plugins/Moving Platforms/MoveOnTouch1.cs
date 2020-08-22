using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


    
public class MoveOnTouch : MonoBehaviour
{
    float moveSpeed = 3f;
    bool moveRight;
   
    void Update()
    {
        
        if (transform.position.x > 5)
            moveRight = false;
        if (transform.position.x < -5)
            moveRight = true;
        

        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}
