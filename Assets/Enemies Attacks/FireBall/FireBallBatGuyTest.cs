using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class FireBallBatGuyTest : MonoBehaviour
{

    public GameObject FireBallPref;
   public void ShootFireBall()
    {
        GameObject fireball = Instantiate(FireBallPref);
        fireball.transform.position = transform.position;      

    }


}
