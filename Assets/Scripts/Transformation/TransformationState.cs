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
        this.abilityUsageCount = transformationData.specialAbility.maxUsageCount;

    }


    // ���� Ȱ��ȭ
    public void Activate()
    {
        Debug.Log("���� ���� ����");

    }


    // Ư�� �ɷ� ���(Ƚ��)
    public void UseSpecialAbility()
    {
        if (abilityUsageCount <= 0)
            return;

        transformationData.specialAbility.ActivateAbility(transformController.transform);
        abilityUsageCount--;


        // Ư�� �ɷ� ��� Ƚ���� ��� �������� ��� ���� ���� �ִϸ��̼� ����
        if (abilityUsageCount <= 0)
        {

            transformController.StartRevertProcess();
        }
    }




    public void Deactivate()
    {
        Debug.Log("���� ���� ����");
        transformController.StartRevertProcess();

    }


    // �ɷ� ��� Ƚ�� ���� ���� Ȯ��
    public bool IsAbilityUsageDepleted()
    {
        return abilityUsageCount <= 0;
    }

}

