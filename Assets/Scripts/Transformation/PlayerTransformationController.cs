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

    private float lastTransformationEndTime = -1f; // ���� �ð��� ���� �ð��� ����

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


    // ���� ���� �޼���
    public void StartTransformation(TransformationData transformationData)
    {
        if (currentTransformation == transformationData.transformationType) return;
        

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


        // NinjaFrog �нú� ȿ�� ���� (���� �浹 ����)
        if (currentTransformation == TransformationType.NinjaFrog)
        {
            PlayerCollisionController collisionController = GetComponent<PlayerCollisionController>();
            collisionController.EnableMonsterIgnore(transformationData.duration);
        }



        // ���� ���� �ð� Ÿ�̸� ����
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer(transformationData.duration));

    }


    // ���� ���� ����
    public void StartRevertProcess()
    {
        // �̹� NormalFrog ���¸�� ������ �ʿ� ����
        if (currentTransformation == TransformationType.NormalFrog)
            return; 

        StopTransformationTimer();
        Debug.Log("���� ���� �ִϸ��̼� ����"); // �ִϸ��̼� ȣ�� Ȯ��

        playerAnimationController.StartRevertAnimation(); // ���� ���� �ִϸ��̼� ����

        lastTransformationEndTime = Time.time; // ���� ���� �ð� ���
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


    // Ư�� �ɷ� ��� �� ��� ���� ���� Ȯ��
    public void UseSpecialAbilityAndCheckRevert()
    {
        if (currentState is TransformationState transformationState)
        {
            transformationState.UseSpecialAbility();

            // �ɷ� ��� �� ��� ���� ���� ����
            if (transformationState.IsAbilityUsageDepleted())
            {
                Debug.Log("�ϻ� ����. ��� ���� ���� ����");
                StartRevertProcess();
            }
        }
    }


    // ���� Ÿ�Կ� ���� TransformationData ã��
    public TransformationData GetTransformationData(TransformationType type)
    {
        return transformationDataList.Find(data => data.transformationType == type);
    }

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
    public void StartTransformationTimer(ITransformation transformation, float duration)
    {
        if (transformationTimer != null)
            StopCoroutine(transformationTimer);
        transformationTimer = StartCoroutine(TransformationTimer(duration));
    }


    // ���� ���� �ð��� ������ �ڵ� ����
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

    public ITransformation GetCurrentState()
    {
        return currentState;
    }


    // ���� ���� ���� ���� �ð� ���� �浹 ����
    public bool IsRecentlyTransformed()
    {
        float timeSinceRevert = Time.time - lastTransformationEndTime;

        // 0.2�� ���� �浹 ����
        // ���� ���� ���� ���� ���� ó���Ǵ� ���� ����
        return (timeSinceRevert >= 0 && timeSinceRevert <= 0.2f); 
    }
}
