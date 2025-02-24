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

    //IEnumerator PlayTurnAround()
    //{
    //    playerAnimator.SetTrigger("GameStart");

    //    // �ִϸ��̼� ���̸�ŭ ���
    //    AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
    //    float animationDuration = stateInfo.length;
    //    yield return new WaitForSeconds(animationDuration);
    //}



    // ���� ���� �� ĳ���� TrunAround �ִϸ��̼� ����
    public void PlayGameStartAnimation()
    {
        animator.SetTrigger("GameStart");
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


    // �ִϸ��̼� ���̸�ŭ ��ٷȴٰ� ������ ����
    public float GetCurrentAnimationLength()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
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
            //case TransformationType.LotusFrog:
            //    animator.SetTrigger("ToLotus");
            //    break;
            //case TransformationType.MusicianFrog:
            //    animator.SetTrigger("ToMusician");
            //    break;
            default:
                animator.SetTrigger("Reset"); // ���� ���� �� �⺻ ���·� ����
                break;
        }

        animator.SetTrigger("ToJumpWait"); // ���� ���� �� WaitJump��ȯ ��ȯ
    }



    public void ResetAllTransformation()
    {
        animator.ResetTrigger("ToNinja");
        animator.ResetTrigger("ToBull");
        //animator.ResetTrigger("ToLotus");
        //animator.ResetTrigger("ToMusician");

        animator.SetTrigger("Reset"); // ��� ���� ���ӽð� ���� �⺻ ���·� ��ȯ�ϴ� �ڵ�
    }



    public void StartRevertAnimation()
    {
        Debug.Log("Revert Ʈ���� ����");
        animator.SetTrigger("Revert"); // RevertToNormal(���� ����) �ִϸ��̼� ����

        StartCoroutine(SetRevertToNormal());
        StartCoroutine(RevertToNormalAfterDelay());
    }


    private IEnumerator SetRevertToNormal()
    {
        yield return new WaitForSeconds(0.1f); // ��� �� ���� ��ȯ

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RevertToNormal"))
        {
            animator.Play("RevertToNormal"); // �ִϸ��̼� ���� ����
        }
    }


    // ���� ���� �ִϸ��̼� ���� �� NormalFrog�� ����
    private IEnumerator RevertToNormalAfterDelay()
    {
        // RevertToNormal �ִϸ��̼� ����� ������ ���
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("RevertToNormal"));


        //// ���� ���� �ִϸ��̼� ���̿� ���� ���
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        Debug.Log("���� ���� �ִϸ��̼� ����");

        // ���� ���� �ʱ�ȭ
        ResetAllTransformation();

        // WaitJump ���·� ��ȯ
        animator.SetTrigger("ToJumpWait");
    }

}
