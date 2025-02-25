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


    // ������� �ִϸ��̼� ���� �� �ϻ� �ִϸ��̼����� ��ȯ
    public void PlayDisappearAnimation()
    {
        ResetAllTriggers();
        animator.SetTrigger("Disappear");
        //StartCoroutine(TransitionToAssassination());
    }

    public void PlayAssassinationAnimation()
    {
        ResetAllTriggers();
        animator.SetTrigger("Assassination");
    }

    // ������� �ִϸ��̼� ���� ��ȯ
    public float GetDisappearAnimationLength()
    {
        return 0.5f; // ���� �ִϸ��̼� ���̿� �°� ����
    }


    // �ϻ� �ִϸ��̼� ���� ��ȯ
    public float GetAssassinationAnimationLength()
    {
        return 0.7f; // ���� �ִϸ��̼� ���̿� �°� ����
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
    }

    // ��� Ʈ���� �ʱ�ȭ
    private void ResetAllTriggers()
    {
        animator.ResetTrigger("Disappear");
        animator.ResetTrigger("Assassination");
        animator.ResetTrigger("ToNinja");
        animator.ResetTrigger("ToBull");
        animator.ResetTrigger("Revert");
        animator.ResetTrigger("ToJumpWait");
    }

    public void ResetTrigger(string triggerName)
    {
        animator.ResetTrigger(triggerName);
    }

    public bool IsAnimationPlaying(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }



    public void StartRevertAnimation()
    {
        Debug.Log("Revert Ʈ���� ����");

        animator.ResetTrigger("Disappear");
        animator.ResetTrigger("Assassination");
        animator.ResetTrigger("ToNinja");
        animator.ResetTrigger("ToBull");
        //animator.ResetTrigger("ToJumpWait");

        // RevertToNormal(���� ����) �ִϸ��̼� ����
        animator.SetTrigger("Revert");
        
        //StopAllCoroutines(); // ���� ���� ���� ���� �ڷ�ƾ �ߺ� ���� ����
        //StartCoroutine(SetRevertToNormal());
        StartCoroutine(RevertToNormalAfterDelay());
    }

    private IEnumerator TransitionToAssassination()
    {
        // ������� �ִϸ��̼� ���� ������ ���
        yield return new WaitForSeconds(GetDisappearAnimationLength());
        PlayAssassinationAnimation();
    }





    // ���� ���� �ִϸ��̼� ���� �� NormalFrog�� ����
    private IEnumerator RevertToNormalAfterDelay()
    {
        // RevertToNormal �ִϸ��̼� ����� ������ ���
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("RevertToNormal"));


        // ���� ���� �ִϸ��̼� ���̿� ���� ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("���� ���� �ִϸ��̼� ����");

        // ���� ���� �ʱ�ȭ
        ResetAllTransformation();

        // WaitJump ���·� ��ȯ
        animator.SetTrigger("ToJumpWait");
    }


}
