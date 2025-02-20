using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation; // 현재 변신 타입

    //public PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;

    private Coroutine transformationTimer; // 변신 타이머 관리



    void Start()
    {
        //gameManager = GameManager.Instance; // Start()에서 한 번만 할당
        playerAnimationController = GetComponent<PlayerAnimationController>();
        //playerInputController = GetComponent<PlayerInputController>();
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

        // 변신 해제 및 애니메이션 리셋
        ResetCurrentTransformation();

        Debug.Log($"변신 시작: {transformationData.transformationType}"); // 어떤 변신인지 확인
        playerAnimationController.PlayerTransformationAnimation(transformationData.transformationType);

        currentTransformation = transformationData.transformationType;


        // 기존 변신 타이머 종료 후 새로운 타이머 시작
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer(transformationData.duration));
    }




    private void ResetCurrentTransformation()
    {
        playerAnimationController.ResetAllTransformation();

    }

    public void StartRevertProcess()
    {
        StopTransformationTimer();
        playerAnimationController.StartRevertAnimation();
        StartCoroutine(RevertToNormalAfterDelay());

    }

    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        RevertToOriginalCharacter();
    }


    public void RevertToOriginalCharacter()
    {
        ResetCurrentTransformation();
        currentTransformation = TransformationType.NormalFrog;
        playerAnimationController.PlayerTransformationAnimation(TransformationType.NormalFrog);
    }

    private void StopTransformationTimer()
    {
        if (transformationTimer != null)
        {
            StopCoroutine(transformationTimer);
            transformationTimer = null;
        }
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
        RevertToOriginalCharacter();
    }

    public void ChangeState(ITransformation newState)
    {
        currentState = newState;
        currentState.Activate();
    }
}
