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
        animator.SetTrigger(isLeft ? "LeftAttack" : "RightAttack"); // ���� ������ ���� �ִϸ��̼� ���� ����
    }


    public void PlayGameOverAnimation()
    {
        animator.SetTrigger("GameOver");
    }


    public float GetAttackAniamtionLength()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(stateInfo.IsName("AttackLeft") || stateInfo.IsName("AttackRight"))
        {
            return stateInfo.length;
        }

        return 0.5f;
    }



    public void PlayerTransformationAnimation(TransformationType newTransformation)
    {
        ResetAllTransformation(); // ���� ���� ����

        switch (newTransformation)
        {
            case TransformationType.NinjaFrog:
                animator.SetTrigger("ToNinja");
                break;
            case TransformationType.BullFrog:
                animator.SetTrigger("ToBull");
                break;
            case TransformationType.LotusFrog:
                animator.SetTrigger("ToLotus");
                break;
            case TransformationType.MusicianFrog:
                animator.SetTrigger("ToMusician");
                break;
            default:
                animator.SetTrigger("Reset"); // ���� ���� �� �⺻ ���·� ����
                break;
        }

        animator.SetTrigger("ToWaitJump");
    }



    public void ResetAllTransformation()
    {
        animator.ResetTrigger("ToNinja");
        animator.ResetTrigger("ToBull");
        animator.ResetTrigger("ToLotus");
        animator.ResetTrigger("ToMusician");

        animator.SetTrigger("Reset"); // ��� ���� ���ӽð� ���� �⺻ ���·� ��ȯ�ϴ� �ڵ�
    }



    public void StartRevertAnimation()
    {
        animator.SetTrigger("Revert"); // ���� ���� �ִϸ��̼� ����
        StartCoroutine(RevertToNormalAfterDelay());
    }

    // ���� ���� �ִϸ��̼� ���� �� NormalFrog�� ����
    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // ���� ���� �ִϸ��̼� ���̿� ���� ���
        ResetAllTransformation(); // ���� ���� �ʱ�ȭ
        animator.SetTrigger("Jumpwait");
    }

}
