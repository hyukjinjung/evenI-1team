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
        //this.gameManager = GameManager.Instance; // Start()���� �� ���� �Ҵ�
        this.PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();

        // Ư�� �ɷ� ��� Ƚ�� ����
        abilityUsageCount = transformationData.specialAbility.maxUsageCount;

    }


    // ���� Ȱ��ȭ
    public void Activate()
    {
        //if (gameManager == null)
        //    return;

        // ���� ���� �ð��� ������ �ڵ����� ������ �����ǵ���
        // ���� ���� �ð� Ÿ�̸� ����
        //transformController.StartTransformationTimer(this, transformationData.duration);

    }


    // Ư�� �ɷ� ���(Ƚ��)
    public void UseSpecialAbility()
    {
        if (gameManager == null && abilityUsageCount <= 0)
            return;

        transformationData.specialAbility.ActivateAbility(transformController.transform);
        abilityUsageCount--;

        // Ư�� �ɷ� ��� Ƚ���� ��� �������� ��� ���� ���� �ִϸ��̼� ����
        if (abilityUsageCount <= 0)
        {
            Debug.Log("Ư�� �ɷ� ��� �Ϸ�. ���� ���� ����");
            Deactivate();
        }
    }




    public void Deactivate()
    {

        if (transformController != null)
        {
            transformController.StartRevertProcess();
        }
        

        // ���� ���� ��, ���� �ð� ���� ���� �浹 ����
        // ���� ���� �� ���� ���� �Ǵ� ���� �߻�
        PlayerCollisionController collisionController = transformController.GetComponent< PlayerCollisionController>();
        if (collisionController != null)
        {
            collisionController.EnableMonsterIgnore(0.2f);
        }


        // NormalFrog ���·� ����
        transformController.ChangeState(new NormalState(transformController));
    }


    // �ɷ� ��� Ƚ�� ���� ���� Ȯ��
    public bool IsAbilityUsageDepleted()
    {
        return abilityUsageCount <= 0;
    }

}

