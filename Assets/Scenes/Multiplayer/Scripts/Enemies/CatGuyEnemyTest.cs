using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatGuyEnemyTest : MonoBehaviour
{
    public Animator CatGuyEnemy_Animator;

    public Transform targetTransform, healthBarTransform;

    private Transform enemyTransform;

    public float enemySpeed, damageAmount, distanceDamage;

    private void Start()
    {
        enemyTransform = this.gameObject.transform;
    }

    private void Update()
    {
        Vector2 distanceBetween_Enemy_Target = targetTransform.position - enemyTransform.position;
        enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, new Vector2(targetTransform.position.x, enemyTransform.position.y), enemySpeed * Time.deltaTime);
       
         if(distanceBetween_Enemy_Target.magnitude <= distanceDamage)
        {
            CatGuyEnemy_Animator.SetBool("isHitting", true);             
        }

        if(healthBarTransform.localScale.x <= 0)
        {
            healthBarTransform.localScale = new Vector2(0, 1);
            StartCoroutine(OnPlayerDeath());
        }
    }

    IEnumerator DealDamage()
    {
       // healthBarTransform.localScale = new Vector2(healthBarTransform.localScale.x - damageAmount, 1);
        GetComponentInChildren<FireBallBatGuyTest>().ShootFireBall();
        yield return null;
    }

    IEnumerator OnPlayerDeath()
    {
        SceneManager.LoadScene(2);
        yield return null;
    }

    private void StopHittingAnimation()
    {
        CatGuyEnemy_Animator.SetBool("isHitting", false);
    }
}

