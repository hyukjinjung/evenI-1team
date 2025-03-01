using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation;

    private PlayerAnimationController playerAnimationController;
    private PlayerAttackController attackController;
    private PlayerCollisionController playerCollisionController;

    public List<TransformationData> transformationDataList;

    private float remainingTime;
    private bool isTransformed;
    


    void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        attackController = GetComponent<PlayerAttackController>();
        playerCollisionController = GetComponent<PlayerCollisionController>();

        currentTransformation = TransformationType.NormalFrog;
        isTransformed = false;
        remainingTime = 0f;

        Debug.Log("기본 상태: NormalFrog");
    }


    void Update()
    {
        if (!isTransformed) return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                Debug.Log("변신 지속 시간 종료. NormalFrog로 전환");

                DeTransform();
            }
        }
    }


    public bool IsTransformed()
    {
        return isTransformed;
    }



    public void Transform(TransformationData transformationData)
    {
        if (currentTransformation == transformationData.transformationType) return;

        playerAnimationController.PlayerTransformationAnimation(transformationData.transformationType);
        currentTransformation = transformationData.transformationType;

        remainingTime = transformationData.duration;
        isTransformed = true;

        attackController.SetTransformedState(true, transformationData.specialAbility);

        currentState = new TransformationState(this, transformationData);

        if (currentState != null)
        {
            currentState.Activate();
            Debug.Log($"현재 특수 능력 횟수 {transformationData.specialAbility.maxUsageCount}");
        }
    }



    public void DeTransform()
    {
        if (!isTransformed) return;

        Debug.Log("변신 강제 해제 실행");

        ResetTransformationTimer();
        
        currentTransformation = TransformationType.NormalFrog;
        currentState = null;
        isTransformed = false;

        Debug.Log("변신 해제 완료. NormalFrog 상태");

        playerAnimationController.StartRevertAnimation();

        attackController.SetTransformedState(false, null);

        playerCollisionController.EnableMonsterIgnore(0f);

        StartCoroutine(RevertToNormalAfterDelay());

    }


    public void ResetTransformationTimer()
    {
        remainingTime = 0f;
        isTransformed = false;
    }


    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitUntil(() => playerAnimationController.IsAnimationPlaying("RevertToNormal"));

        Debug.Log("변신 해제 애니메이션 종료");

        playerAnimationController.ResetAllAnimation();
        currentTransformation = TransformationType.NormalFrog;

        // 변신 해제 후 기본 공격 가능하도록 isAttacking 초기화
        // attackController.ResetAttackState();
    }


    public ITransformation GetCurrentState()
    {
        return currentState;
    }



    public TransformationData GetCurrentTransformationData()
    {

        return transformationDataList.Find(data => data.transformationType == currentTransformation);
    }

}
