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

    private Coroutine transformationTimer; // 변신 타이머 관리

    // 변신 데이터 리스트 (ScriptableObject)
    public List<TransformationData> transformationDataList;

    private float remainingTime = 0f; // 변신 남은 시간 추적 변수
    private int specialAbilityUsesRemaning = 0; // 남은 특수 능력 사용 횟수


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


        // 특수 능력 최대 사용 횟수 설정
        specialAbilityUsesRemaning = transformationData.specialAbility.maxUsageCount;
        remainingTime = transformationData.duration;


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
            GetComponent<PlayerCollisionController>().EnableMonsterIgnore(transformationData.duration);
        }


        // 변신 지속 시간 타이머 시작
        ResetTransformationTimer();

        transformationTimer = StartCoroutine(TransformationTimer());

    }


    public void UseSpecialAbility()
    {
        if (specialAbilityUsesRemaning > 0)
        {
            specialAbilityUsesRemaning--;
            Debug.Log($"특수 능력 사용. 남은 횟수: {specialAbilityUsesRemaning}");

            if (specialAbilityUsesRemaning == 0)
            {
                Debug.Log("특수 능력 횟수 소진.즉시 변신 해제");
                ResetTransformationTimer();
                StartRevertProcess();
            }
        }
    }


    // 변신 해제 시작
    public void StartRevertProcess()
    {
        // 이미 NormalFrog 상태면실 실행할 필요 없음
        if (currentTransformation == TransformationType.NormalFrog)
            return;

        ResetTransformationTimer();
        Debug.Log("변신 해제 애니메이션 실행"); // 애니메이션 호출 확인

        playerAnimationController.StartRevertAnimation(); // 변신 해제 애니메이션 실행

        // 변신 해제 직후 타일 정보 갱신
        FindObjectOfType<PlayerMovement>().UpdateTileInfo();

        // 변신 해제 시 특수 능력 비활성화
        PlayerAttackController playerAttackController = GetComponent<PlayerAttackController>();
        if (playerAttackController != null)
        {
            playerAttackController.SetTransformedState(false, null);
        }

        StartCoroutine(RevertToNormalAfterDelay());

    }


    private void ResetTransformationTimer()
    {
        if (transformationTimer != null)
        {
            StopCoroutine(transformationTimer);
            transformationTimer = null;
        }
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

}
