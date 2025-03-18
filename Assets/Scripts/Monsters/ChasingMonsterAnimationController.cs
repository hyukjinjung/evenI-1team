using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonsterAnimationController : MonoBehaviour
{
    public Animator animator;
    private bool isAttacking = false;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
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
            //animator.SetTrigger("Attack");
            isAttacking = true;
            StartCoroutine(PlayAttackSequence());
        }
    }


    public void PlayMonsterRoar()
    {
        if (animator == null)
        {
            Debug.Log("Animator is null");
        }

        animator.SetTrigger("MonsterRoar");
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
