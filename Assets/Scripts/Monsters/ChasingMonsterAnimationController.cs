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
            StartCoroutine(PlayAttackSequence());
        }
    }


    
    private IEnumerator PlayAttackSequence()
    {
        GameManager.Instance.PlayerAnimationController.PlaySurprised();

        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.GameOver();
    }
}
