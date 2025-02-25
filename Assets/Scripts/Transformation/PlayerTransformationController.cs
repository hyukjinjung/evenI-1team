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
        transformationTimer = StartCoroutine(TransformationTimer(transformationData.duration));

    }



    public void StartRevertProcess()
    {
        if (currentTransformation == TransformationType.NormalFrog)
            return; // 이미 NormalFrog 상태면실 실행할 필요 없음

        StopTransformationTimer();

        Debug.Log("변신 해제 애니메이션 실행"); // 애니메이션 호출 확인

        playerAnimationController.StartRevertAnimation(); // 변신 해제 애니메이션 실행
        StartCoroutine(RevertToNormalAfterDelay());

        // 변신 해제 후 NormalFrog로 복귀
        //currentTransformation = TransformationType.NormalFrog;
        //ChangeState(new NormalState(this));

    }


    private void StopTransformationTimer()
    {
        if (transformationTimer != null)
        {
            StopCoroutine(transformationTimer);
            transformationTimer = null;
        }
    }



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


    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitUntil(() => playerAnimationController.IsAnimationPlaying("RevertToNormal"));

        Debug.Log("변신 해제 애니메이션 종료");

        // 변신 상태 초기화
        playerAnimationController.ResetAllTransformation();
        currentTransformation = TransformationType.NormalFrog;
    }


    //변신 타이머 추가
    public void StartTransformationTimer(ITransformation transformation, float duration)
    {
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer(duration));
    }


    // 변신 지속 시간 후 해제
    public IEnumerator TransformationTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartRevertProcess(); // 변신 해제 실행
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
}
