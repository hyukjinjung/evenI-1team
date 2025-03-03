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



    public void SetJumping(bool isJumping)
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void SetAttacking(bool isLeft)
    {
        animator.SetTrigger(isLeft ? "LeftAttack" : "RightAttack");
    }



    public void PlayGameStartAnimation()
    {
        animator.SetTrigger("GameStart");
    }


    public void PlayGameOverAnimation()
    {
        animator.SetTrigger("GameOver");
    }



    public void PlayDisappearAnimation()
    {
        ResetAllAnimation();
        animator.SetTrigger("Disappear");
    }



    public void PlayAssassinationAnimation()
    {
        ResetAllAnimation();
        animator.SetTrigger("Assassination");
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



    public void PlayerTransformationAnimation(TransformationType newTransformation)
    {
        ResetAllAnimation();

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
                animator.SetTrigger("Reset");
                break;
        }

    }



    public void ResetAllAnimation()
    {
        animator.ResetTrigger("Disappear");
        animator.ResetTrigger("Assassination");


        animator.ResetTrigger("ToNinja");
        animator.ResetTrigger("ToBull");
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
        Debug.Log("Revert 트리거 실행");

        ResetAllAnimation();

        animator.Play("RevertToNormal");

        StartCoroutine(RevertToNormalAfterDelay());
    }



    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("RevertToNormal"));


        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("변신 해제 애니메이션 종료");

        ResetAllAnimation();

        animator.SetTrigger("ToJumpWait");

    }

}
