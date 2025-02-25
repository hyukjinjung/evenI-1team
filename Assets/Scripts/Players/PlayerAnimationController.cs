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


    // 애니메이션 길이만큼 기다렸다가 변신을 해제
    public float GetCurrentAnimationLength()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
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

        animator.SetTrigger("ToJumpWait"); // 변신 해제 후 WaitJump로환 전환
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
        Debug.Log("Revert 트리거 실행");
        animator.SetTrigger("Revert"); // RevertToNormal(변신 해제) 애니메이션 실행

        StartCoroutine(SetRevertToNormal());
        StartCoroutine(RevertToNormalAfterDelay());
    }


    private IEnumerator SetRevertToNormal()
    {
        yield return new WaitForSeconds(0.1f); // 대기 후 강제 전환

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("RevertToNormal"))
        {
            animator.Play("RevertToNormal"); // 애니메이션 강제 변경
        }
    }


    // 변신 해제 애니메이션 끝난 후 NormalFrog로 복귀
    private IEnumerator RevertToNormalAfterDelay()
    {
        // RevertToNormal 애니메이션 실행될 때까지 대기
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("RevertToNormal"));

        Debug.Log("ReverToNormal 감지");


        // 변신 해제 애니메이션 길이에 맞춰 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("변신 해제 애니메이션 종료");
        

        // 변신 상태 해제(이 시점에서 실행)
        Debug.Log("변신 해제 완료. JumpWait 상태로 전환");


        // 변신 상태 초기화
        ResetAllTransformation();


        // WaitJump 상태로 전환
        animator.SetTrigger("ToJumpWait");
    }

}
