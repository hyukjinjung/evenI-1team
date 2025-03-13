using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonsterAnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }

    public void PlayMove()
    {
        animator.SetBool("isMoving", true);
    }


    public void PlayAttack()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            StartCoroutine(ResetAttackState());
        }
    }


    private IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false ;
    }






}
