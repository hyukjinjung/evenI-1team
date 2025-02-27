using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation; // 현재 변신 타입

    private PlayerAnimationController playerAnimationController;

    private Coroutine transformationTimer; // 변신 타이머 관리

    // 변신 데이터 리스트 (ScriptableObject)
    public List<TransformationData> transformationDataList;

    private float remainingTime = 0f; // 변신 남은 시간 추적 변수


    void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        currentState = new NormalState(this);
        currentTransformation = TransformationType.NormalFrog;

    }

    public bool IsTransformed()
    {
        return currentTransformation != TransformationType.NormalFrog;
    }


    // 변신 시작 메서드
    public void StartTransformation(TransformationData transformationData)
    {
        if (currentTransformation == transformationData.transformationType) return;
        

        // 변신 애니메이션 실행
        playerAnimationController.PlayerTransformationAnimation(transformationData.transformationType);
        currentTransformation = transformationData.transformationType;


        // 특수 능력 설정
        PlayerAttackController attackController = GetComponent<PlayerAttackController>();
        if (attackController != null)
        {
            Debug.Log("특수 능력 할당됨");
            attackController.SetTransformedState(true, transformationData.specialAbility);
        }


        // NinjaFrog 패시브 효과 적용 (몬스터 충돌 무시)
        if (currentTransformation == TransformationType.NinjaFrog)
        {
            PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();
            collisionController.EnableMonsterIgnore(transformationData.duration);
        }



        // 변신 지속 시간 타이머 시작
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);

        remainingTime = transformationData.duration; // 남은 시간 초기화
        transformationTimer = StartCoroutine(TransformationTimer());

    }


    // 변신 해제 시작
    public void StartRevertProcess()
    {
        // 이미 NormalFrog 상태면실 실행할 필요 없음
        if (currentTransformation == TransformationType.NormalFrog)
            return; 

        StopTransformationTimer();
        Debug.Log("변신 해제 애니메이션 실행"); // 애니메이션 호출 확인

        playerAnimationController.StartRevertAnimation(); // 변신 해제 애니메이션 실행

        //lastTransformationEndTime = Time.time; // 변신 해제 시간 기록
        StartCoroutine(RevertToNormalAfterDelay());

    }


    private void StopTransformationTimer()
    {
        if (transformationTimer != null)
        {
            StopCoroutine(transformationTimer);
            transformationTimer = null;
        }
    }


    // 특수 능력 사용 후 즉시 변신 해제 확인
    public void UseSpecialAbilityAndCheckRevert()
    {
        if (currentState is TransformationState transformationState)
        {
            transformationState.UseSpecialAbility();

            // 능력 사용 후 즉시 변신 해제 실행
            if (transformationState.IsAbilityUsageDepleted())
            {
                Debug.Log("암살 성공. 즉시 변신 해제 실행");
                StartRevertProcess();
            }
        }
    }


    // 변신 타입에 따라 TransformationData 찾기
    public TransformationData GetTransformationData(TransformationType type)
    {
        return transformationDataList.Find(data => data.transformationType == type);
    }

    //변신 해제 후 NormalFrog 상태로 복귀
    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitUntil(() => playerAnimationController.IsAnimationPlaying("RevertToNormal"));

        Debug.Log("변신 해제 애니메이션 종료");

        // 변신 상태 초기화
        playerAnimationController.ResetAllTransformation();
        currentTransformation = TransformationType.NormalFrog;
    }


    //변신 타이머 관리
    public void StartTransformationTimer(ITransformation transformation, float duration)
    {
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer());
    }


    // 변신 지속 시간이 끝나면 자동 해제
    public IEnumerator TransformationTimer()
    {
        while (remainingTime > 0)
        {
            Debug.Log($"변신 남은 시간: {remainingTime}초");

            yield return new WaitForSeconds(1);
            remainingTime -= 1f;
        }

        StartRevertProcess(); // 변신 지속시간이 끝나면 해제
    }

    public void ChangeState(ITransformation newState)
    {
        currentState = newState;
        currentState.Activate();
    }

    public ITransformation GetCurrentState()
    {
        return currentState;
    }


    //// 변신 해제 직후 일정 시간 동안 충돌 무시
    //public bool IsRecentlyTransformed()
    //{
    //    float timeSinceRevert = Time.time - lastTransformationEndTime;

    //    // 0.2초 동안 충돌 무시
    //    // 변신 해제 직후 게임 오버 처리되는 오류 방지
    //    return (timeSinceRevert >= 0 && timeSinceRevert <= 0.2f); 
    //}
}
