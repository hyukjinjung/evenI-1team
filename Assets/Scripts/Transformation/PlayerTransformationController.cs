using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation; // ���� ���� Ÿ��

    //public PlayerInputController playerInputController;
    private PlayerAnimationController playerAnimationController;

    private Coroutine transformationTimer; // ���� Ÿ�̸� ����



    void Start()
    {
        //gameManager = GameManager.Instance; // Start()���� �� ���� �Ҵ�
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

        // ���� ���� �� �ִϸ��̼� ����
        ResetCurrentTransformation();

        Debug.Log($"���� ����: {transformationData.transformationType}"); // � �������� Ȯ��
        playerAnimationController.PlayerTransformationAnimation(transformationData.transformationType);

        currentTransformation = transformationData.transformationType;


        // ���� ���� Ÿ�̸� ���� �� ���ο� Ÿ�̸� ����
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


    //���� Ÿ�̸� �߰�
    public void StartTransformationTimer(ITransformation transformation, float duration)
    {
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer(duration));
    }

    // ���� ���� �ð� �� ����
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
