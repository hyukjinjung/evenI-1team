using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation; // ���� ���� Ÿ��

    private PlayerAnimationController playerAnimationController;

    private Coroutine transformationTimer; // ���� Ÿ�̸� ����

    // ���� ������ ����Ʈ (ScriptableObject)
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


        
        //playerAnimationController.ResetAllTransformation();
        //Debug.Log($"���� ����: {transformationData.transformationType}"); // � �������� Ȯ��
        

        // ���� �ִϸ��̼� ����
        playerAnimationController.PlayerTransformationAnimation(transformationData.transformationType);
        currentTransformation = transformationData.transformationType;

        // Ư�� �ɷ� ����
        PlayerAttackController attackController = GetComponent<PlayerAttackController>();
        if (attackController != null)
        {
            Debug.Log("Ư�� �ɷ� �Ҵ��");
            attackController.SetTransformedState(true, transformationData.specialAbility);
        }

        // ���� ���� �ð� Ÿ�̸� ����
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer(transformationData.duration));

    }



    public void StartRevertProcess()
    {     

        StopTransformationTimer();

        Debug.Log("���� ���� �ִϸ��̼� ����"); // �ִϸ��̼� ȣ�� Ȯ��
        playerAnimationController.StartRevertAnimation(); // ���� ���� �ִϸ��̼� ����

    }


    private void StopTransformationTimer()
    {
        if (transformationTimer != null)
        {
            StopCoroutine(transformationTimer);
            transformationTimer = null;
        }
    }


    public void RevertToOriginalCharacter()
    {
        playerAnimationController.ResetAllTransformation();
        currentTransformation = TransformationType.NormalFrog;
    }

    
    // ���� Ÿ�Կ� ���� TransformationData ã��
    public TransformationData GetTransformationData(TransformationType type)
    {
        return transformationDataList.Find(data => data.transformationType == type);
    }


    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitForSeconds(playerAnimationController.GetCurrentAnimationLength());

        RevertToOriginalCharacter(); // NormalFrog�� ����
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
        StartRevertProcess(); // ���� ���� ����
    }

    public void ChangeState(ITransformation newState)
    {
        currentState = newState;
        currentState.Activate();
    }
}
