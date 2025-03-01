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

    private PlayerTransformationController transformController;
    private TransformationData transformationData;
    private PlayerAttackController attackController;

    private int abilityUsageCount; // Ư�� �ɷ� ��� Ƚ�� ����

    


    public TransformationState(PlayerTransformationController transformController,
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.abilityUsageCount = transformationData.specialAbility.maxUsageCount;

        Initialize();
    }

    private void Initialize()
    {
        attackController = transformController.GetComponent<PlayerAttackController>();
    }


    public void Activate()
    {
        transformController.Transform(transformationData);
        Debug.Log($"���� ���� ����. {transformationData.transformationType}");

    }



    public void UseSpecialAbility()
    {
        Debug.Log($"Ư�� �ɷ� ��� ��. ���� ���� Ƚ�� {abilityUsageCount} ");


        if (abilityUsageCount <= 0)
        {
            Debug.Log($"Ư�� �ɷ� ��� Ƚ�� 0. ���� ���� Ƚ�� {abilityUsageCount}");
            return;
        }

        transformationData.specialAbility.ActivateAbility(transformController.transform, transformationData);
        abilityUsageCount--;

        Debug.Log($"Ư�� �ɷ� ��� �Ϸ�. ���� ���� Ƚ�� {abilityUsageCount}");


        if (abilityUsageCount <= 0)
        {
            transformController.ResetTransformationTimer();
            transformController.DeTransform();

        }

        if (attackController != null)
        {
            attackController.ResetAttackState();
        }
    }




    public void Deactivate()
    {
        Debug.Log("���� ���� ����");
        transformController.DeTransform();


    }



    public int GetRemainingAbilityUses()
    {
        return abilityUsageCount;
    }

}

