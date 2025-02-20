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
        animator.SetTrigger(isLeft ? "LeftAttack" : "RightAttack"); // 왼쪽 오른쪽 공격 애니메이션 각각 실행
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
        ResetAllTransformation(); // 기존 변신 리셋

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
                animator.SetTrigger("Reset"); // 변신 해제 시 기본 상태로 변경
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

        animator.SetTrigger("Reset"); // 모든 변신 지속시간 이후 기본 상태로 전환하는 코드
    }



    public void StartRevertAnimation()
    {
        animator.SetTrigger("Revert"); // 변신 해제 애니메이션 실행
        StartCoroutine(RevertToNormalAfterDelay());
    }

    // 변신 해제 애니메이션 끝난 후 NormalFrog로 복귀
    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // 변신 해제 애니메이션 길이에 맞춰 대기
        ResetAllTransformation(); // 변신 상태 초기화
        animator.SetTrigger("Jumpwait");
    }

}
