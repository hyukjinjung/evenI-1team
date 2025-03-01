using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation; // 현재 변신 타입

    private PlayerAnimationController playerAnimationController;
    private PlayerAttackController attackController;
    //private PlayerCollisionController playerCollisionController;

    //PlayerMovement playerMovement;
    
    private Coroutine transformationTimer; // 변신 타이머 관리

    // 변신 데이터 리스트 (ScriptableObject)
    public List<TransformationData> transformationDataList;

    private float remainingTime; // 변신 남은 시간 추적 변수
    private int specialAbilityUsesRemaning; // 남은 특수 능력 사용 횟수


    void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();

        attackController = GetComponent<PlayerAttackController>();
        //playerCollisionController = GetComponent<PlayerCollisionController>();

        currentState = new NormalState(this);
        currentTransformation = TransformationType.NormalFrog;
        //playerMovement = GetComponent<PlayerMovement>();
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


        // 특수 능력 최대 사용 횟수 설정
        specialAbilityUsesRemaning = transformationData.specialAbility.maxUsageCount;
        remainingTime = transformationData.duration;


        // 특수 능력 설정
        attackController.SetTransformedState(true, transformationData.specialAbility);


        //ApplyEffect(transformationData);


        // 변신 지속 시간 타이머 시작
        ResetTransformationTimer();

        transformationTimer = StartCoroutine(TransformationTimer());

    }

    //private void ApplyEffect(TransformationData transformationData)
    //{
    //    // NinjaFrog 패시브 효과 적용 (몬스터 충돌 무시)
    //    if (currentTransformation == TransformationType.NinjaFrog)
    //    {
    //        playerCollisionController.EnableMonsterIgnore(transformationData.duration);
    //    }
    //}


    public void UseSpecialAbilityAndRevert()
    {
        if (currentState is TransformationState transformationState)
        {
            transformationState.UseSpecialAbility();

            if (transformationState.IsAbilityUsageDepleted())
            {
                Debug.Log("변신 해제 실행");
                StartRevertProcess();
            }
        }
    }


    // 변신 해제 시작
    public void StartRevertProcess()
    {
        if (currentTransformation == TransformationType.NormalFrog) return;

        Debug.Log("변신 해제 실행");

        ResetTransformationTimer();

        playerAnimationController.StartRevertAnimation();

        attackController.SetTransformedState(false, null);
        
        StartCoroutine(RevertToNormalAfterDelay());

    }


    private void ResetTransformationTimer()
    {
        if (transformationTimer != null)
        {
            Debug.Log("변신 타이머 중단");
            StopCoroutine(transformationTimer);
            transformationTimer = null;
        }
    }


    //private void StopTransformationTimer()
    //{
    //    if (transformationTimer != null)
    //    {
    //        StopCoroutine(transformationTimer);
    //        transformationTimer = null;
    //    }
    //}


    //// 변신 타입에 따라 TransformationData 찾기
    //public TransformationData GetTransformationData(TransformationType type)
    //{
    //    return transformationDataList.Find(data => data.transformationType == type);
    //}

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
    //public void StartTransformationTimer(ITransformation transformation, float duration)
    //{
    //    if (transformationTimer != null)
    //        StopCoroutine(transformationTimer);
    //    transformationTimer = StartCoroutine(TransformationTimer());
    //}


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

    //public void ChangeState(ITransformation newState)
    //{
    //    currentState = newState;
    //    currentState.Activate();
    //}

    public ITransformation GetCurrentState()
    {
        return currentState;
    }

}
