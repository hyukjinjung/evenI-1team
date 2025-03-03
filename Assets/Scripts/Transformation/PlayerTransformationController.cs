using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerTransformationController : MonoBehaviour
{
    private TransformationType currentTransformationType;

    private PlayerAnimationController playerAnimationController;
    private PlayerAttackController attackController;
    private PlayerCollisionController playerCollisionController;
    private PlayerInputController inputController;
    private TestTileManager testTileManager;
    private PlayerMovement playerMovement;

    public List<TransformationData> transformationDataList;

    public TransformationData currentTransformationData;
    
    private bool isTransformed;
    
    private float remainingTime;
    private int abilityUsageCount;
    


    void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        attackController = GetComponent<PlayerAttackController>();
        playerCollisionController = GetComponent<PlayerCollisionController>();
        inputController = GetComponent<PlayerInputController>();
        testTileManager = GetComponent<TestTileManager>();
        playerMovement = GetComponent<PlayerMovement>();

        currentTransformationType = TransformationType.NormalFrog;
        isTransformed = false;
        remainingTime = 0f;

        Debug.Log("�⺻ ����: NormalFrog");
    }


    void Update()
    {
        if (!isTransformed) return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                Debug.Log("���� ���� �ð� ����. NormalFrog�� ��ȯ");

                DeTransform();
            }
        }
    }
    
    public bool IsTransformed()
    {
        return isTransformed;
    }
    
    public TransformationType GetCurrentTransformation()
    {
        return currentTransformationType;
    }

    public void Transform(TransformationType transformationType)
    {
        if (currentTransformationType == transformationType) return;

        TransformationData transformationData = GetCurrentTransformationData(transformationType);
        
        if (transformationData == null) return;
        
        currentTransformationType = transformationType;
        currentTransformationData = transformationData;
        
        isTransformed = true;
        
        remainingTime = transformationData.duration;
        abilityUsageCount = transformationData.specialAbility.maxUsageCount;
        
        playerAnimationController.PlayerTransformationAnimation(transformationType);
    }



    public void DeTransform()
    {
        if (!isTransformed) return;

        Debug.Log("���� ���� ���� ����");

        
        currentTransformationType = TransformationType.NormalFrog;

        Debug.Log("���� ���� �Ϸ�. NormalFrog ����");

        playerAnimationController.StartRevertAnimation();
        
        currentTransformationData = null;
        
        EnablePlayerInput(false);

        playerCollisionController.EnableMonsterIgnore(0f);
      
        ResetTransformation();
        StartCoroutine(RevertToNormalAfterDelay());

    }


    public void ResetTransformation()
    {
        remainingTime = 0f;
        isTransformed = false;
        currentTransformationData = null;
        abilityUsageCount = 0;
    }


    private IEnumerator RevertToNormalAfterDelay()
    {
        yield return new WaitUntil(() => playerAnimationController.IsAnimationPlaying("RevertToNormal"));

        Debug.Log("���� ���� �ִϸ��̼� ����");
        playerAnimationController.ResetAllAnimation();

        yield return new WaitForSeconds(1.5f);

        EnablePlayerInput(true);

    }

    public TransformationData GetCurrentTransformationData(TransformationType transformationType)
    {
        return transformationDataList.Find(data => data.transformationType ==transformationType);
    }


    private void EnablePlayerInput(bool enable)
    {
        if (inputController != null)
        {
            inputController.SetInputActive(enable);
        }
    }
    
    public void UseSpecialAbility()
    {
        if (abilityUsageCount <= 0)
        {
            return;
        }

        currentTransformationData.specialAbility.ActivateAbility(transform, currentTransformationData);
        abilityUsageCount--;

        Debug.Log($"Ư�� �ɷ� ��� �Ϸ�. ���� ���� Ƚ�� {abilityUsageCount}");
        
        if (abilityUsageCount <= 0)
        {
            DeTransform();
        }
        
        attackController.ResetAttackState();
    }
    
}
