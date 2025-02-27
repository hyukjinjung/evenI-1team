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

    private Coroutine transformationTimer; // ���� Ÿ�̸� ����

    // ���� ������ ����Ʈ (ScriptableObject)
    public List<TransformationData> transformationDataList;

    private float remainingTime = 0f; // ���� ���� �ð� ���� ����
    private int specialAbilityUsesRemaning = 0; // ���� Ư�� �ɷ� ��� Ƚ��


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


        // Ư�� �ɷ� �ִ� ��� Ƚ�� ����
        specialAbilityUsesRemaning = transformationData.specialAbility.maxUsageCount;
        remainingTime = transformationData.duration;


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
            GetComponent<PlayerCollisionController>().EnableMonsterIgnore(transformationData.duration);
        }


        // ���� ���� �ð� Ÿ�̸� ����
        ResetTransformationTimer();

        transformationTimer = StartCoroutine(TransformationTimer());

    }


    public void UseSpecialAbility()
    {
        if (specialAbilityUsesRemaning > 0)
        {
            specialAbilityUsesRemaning--;
            Debug.Log($"Ư�� �ɷ� ���. ���� Ƚ��: {specialAbilityUsesRemaning}");

            if (specialAbilityUsesRemaning == 0)
            {
                Debug.Log("Ư�� �ɷ� Ƚ�� ����.��� ���� ����");
                ResetTransformationTimer();
                StartRevertProcess();
            }
        }
    }


    // ���� ���� ����
    public void StartRevertProcess()
    {
        // �̹� NormalFrog ���¸�� ������ �ʿ� ����
        if (currentTransformation == TransformationType.NormalFrog)
            return;

        ResetTransformationTimer();
        Debug.Log("���� ���� �ִϸ��̼� ����"); // �ִϸ��̼� ȣ�� Ȯ��

        playerAnimationController.StartRevertAnimation(); // ���� ���� �ִϸ��̼� ����

        // ���� ���� ���� Ÿ�� ���� ����
        FindObjectOfType<PlayerMovement>().UpdateTileInfo();

        // ���� ���� �� Ư�� �ɷ� ��Ȱ��ȭ
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
        transformationTimer = StartCoroutine(TransformationTimer());
    }


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
