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
    private PlayerInputController inputController;
    private TestTileManager testTileManager;
    private PlayerMovement playerMovement;

    public List<TransformationData> transformationDataList;

    private float remainingTime;
    private bool isTransformed;
    


    void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        attackController = GetComponent<PlayerAttackController>();
        playerCollisionController = GetComponent<PlayerCollisionController>();
        inputController = GetComponent<PlayerInputController>();
        testTileManager = GetComponent<TestTileManager>();
        playerMovement = GetComponent<PlayerMovement>();

        currentTransformation = TransformationType.NormalFrog;
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
        Debug.Log($"���� ���� Ȯ�� {isTransformed}");
        return isTransformed;
    }
    
    public TransformationType GetCurrentTransformation()
    {
        return currentTransformation;
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
            Debug.Log($"���� Ư�� �ɷ� Ƚ�� {transformationData.specialAbility.maxUsageCount}");
        }
    }



    public void DeTransform()
    {
        if (!isTransformed) return;

        Debug.Log("���� ���� ���� ����");

        ResetTransformationTimer();
        
        currentTransformation = TransformationType.NormalFrog;
        currentState = null;
        //isTransformed = false;

        Debug.Log("���� ���� �Ϸ�. NormalFrog ����");

        playerAnimationController.StartRevertAnimation();

        attackController.SetTransformedState(false, null);

        EnablePlayerInput(false);

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

        Debug.Log("���� ���� �ִϸ��̼� ����");


        playerAnimationController.ResetAllAnimation();
        currentTransformation = TransformationType.NormalFrog;

        yield return new WaitForSeconds(1.5f);

        EnablePlayerInput(true);

    }



    public ITransformation GetCurrentState()
    {
        return currentState;
    }



    public TransformationData GetCurrentTransformationData()
    {

        return transformationDataList.Find(data => data.transformationType == currentTransformation);
    }


    private void EnablePlayerInput(bool enable)
    {
        if (inputController != null)
        {
            inputController.SetInputActive(enable);
        }
    }

}
