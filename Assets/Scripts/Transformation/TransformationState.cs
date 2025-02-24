using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*

Ŭ���� ����:
�÷��̾ �������� ���� ���¸� ����
 
*/

public class TransformationState : ITransformation
{
    private PlayerAnimationController PlayerAnimationController;
    private GameManager gameManager;

    private PlayerTransformationController transformController;
    private TransformationData transformationData;

    private int abilityUsageCount; // Ư�� �ɷ� ��� Ƚ�� ����


    public TransformationState(PlayerTransformationController transformController, 
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.gameManager = GameManager.Instance; // Start()���� �� ���� �Ҵ�
        this.PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();

        // Ư�� �ɷ� ��� Ƚ�� ����
        abilityUsageCount = transformationData.specialAbility.maxUsageCount; 

    }


    // ���� Ȱ��ȭ
    public void Activate()
    {
        if (gameManager == null)
            return;

        // ���� ���� �ð��� ������ �ڵ����� ������ �����ǵ���
        // ���� ���� �ð� Ÿ�̸� ����
        transformController.StartTransformationTimer(this, transformationData.duration);

    }


    // Ư�� �ɷ� ���(Ƚ��)
    public void UseSpecialAbility()
    {
        if (gameManager == null && abilityUsageCount <= 0)
            return;

        transformationData.specialAbility.ActivateAbility(transformController.transform);
        abilityUsageCount--;

        if (abilityUsageCount <= 0)
        {
            Deactivate();
        }
    }




    public void Deactivate()
    {
        if (gameManager == null) // GameManager�� NULL�̸� ����
            return;

        // ���� ���� �ִϸ��̼� ����
        if (transformController != null)
        {
            PlayerAnimationController.PlayerTransformationAnimation(TransformationType.NormalFrog);
        }

        // �⺻ ���·� ����
        transformController.ChangeState(new NormalState(transformController));
    }


}

