using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }




    public void SetJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void SetAttacking(bool isLeft)
    {
        animator.SetTrigger(isLeft ? "AttackLeft" : "AttackRight"); // ���� ������ ���� �ִϸ��̼� ���� ����
    }

    public void PlayGameOverAnimation()
    {
        animator.SetTrigger("GameOver");
    }
}
