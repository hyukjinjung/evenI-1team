using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    void Start()
    {
       
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

    // NinjaFrog �ִϸ��̼�
    public void PlayDisappearAnimation()
    {
        ResetAllTriggers();
        animator.SetTrigger("Disappear");
        //StartCoroutine(TransitionToAssassination());
    }

    // NinjaFrog �ִϸ��̼�
    public void PlayAssassinationAnimation()
    {
        ResetAllTriggers();
        animator.SetTrigger("Assassination");
    }


    // ���� ���� �ִϸ��̼� ��� ����
    public void PlayRevertToNormalAnimation()
    {
        ResetAllTriggers();
        animator.SetTrigger("ToJumpWait");
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

    public float GetDisappearAnimationLength() => 0.5f;
    public float GetAssassinationAnimationLength() => 0.5f;


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

        // ���� Ʈ���� �ʱ�ȭ
        ResetAllTriggers();

        // ���� ���� �ִϸ��̼� ��� ����
        animator.Play("RevertToNormal");

        StartCoroutine(RevertToNormalAfterDelay());
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
