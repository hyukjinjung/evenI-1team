using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerTransformationController : MonoBehaviour
{
    private ITransformation currentState;
    private TransformationType currentTransformation; // ���� ���� Ÿ��

    private PlayerAnimationController playerAnimationController;
    private PlayerAttackController attackController;
    //private PlayerCollisionController playerCollisionController;

    //PlayerMovement playerMovement;
    
    private Coroutine transformationTimer; // ���� Ÿ�̸� ����

    // ���� ������ ����Ʈ (ScriptableObject)
    public List<TransformationData> transformationDataList;

    private float remainingTime; // ���� ���� �ð� ���� ����
    private int specialAbilityUsesRemaning; // ���� Ư�� �ɷ� ��� Ƚ��


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
    

    // ���� ���� �޼���
    public void StartTransformation(TransformationData transformationData)
    {
        if (currentTransformation == transformationData.transformationType) return;
        

        // ���� �ִϸ��̼� ����
        playerAnimationController.PlayerTransformationAnimation(transformationData.transformationType);
        currentTransformation = transformationData.transformationType;


        // Ư�� �ɷ� �ִ� ��� Ƚ�� ����
        specialAbilityUsesRemaning = transformationData.specialAbility.maxUsageCount;
        remainingTime = transformationData.duration;


        // Ư�� �ɷ� ����
        attackController.SetTransformedState(true, transformationData.specialAbility);


        //ApplyEffect(transformationData);


        // ���� ���� �ð� Ÿ�̸� ����
        ResetTransformationTimer();

        transformationTimer = StartCoroutine(TransformationTimer());

    }

    //private void ApplyEffect(TransformationData transformationData)
    //{
    //    // NinjaFrog �нú� ȿ�� ���� (���� �浹 ����)
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
                Debug.Log("���� ���� ����");
                StartRevertProcess();
            }
        }
    }


    // ���� ���� ����
    public void StartRevertProcess()
    {
        if (currentTransformation == TransformationType.NormalFrog) return;

        Debug.Log("���� ���� ����");

        ResetTransformationTimer();

        playerAnimationController.StartRevertAnimation();

        attackController.SetTransformedState(false, null);
        
        StartCoroutine(RevertToNormalAfterDelay());

    }


    private void ResetTransformationTimer()
    {
        if (transformationTimer != null)
        {
            Debug.Log("���� Ÿ�̸� �ߴ�");
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


    //// ���� Ÿ�Կ� ���� TransformationData ã��
    //public TransformationData GetTransformationData(TransformationType type)
    //{
    //    return transformationDataList.Find(data => data.transformationType == type);
    //}

    //���� ���� �� NormalFrog ���·� ����
    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitUntil(() => playerAnimationController.IsAnimationPlaying("RevertToNormal"));

        Debug.Log("���� ���� �ִϸ��̼� ����");

        // ���� ���� �ʱ�ȭ
        playerAnimationController.ResetAllTransformation();
        currentTransformation = TransformationType.NormalFrog;
    }


    //���� Ÿ�̸� ����
    //public void StartTransformationTimer(ITransformation transformation, float duration)
    //{
    //    if (transformationTimer != null)
    //        StopCoroutine(transformationTimer);
    //    transformationTimer = StartCoroutine(TransformationTimer());
    //}


    // ���� ���� �ð��� ������ �ڵ� ����
    public IEnumerator TransformationTimer()
    {
        while (remainingTime > 0)
        {
            Debug.Log($"���� ���� �ð�: {remainingTime}��");

            yield return new WaitForSeconds(1);
            remainingTime -= 1f;
        }

        StartRevertProcess(); // ���� ���ӽð��� ������ ����
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
