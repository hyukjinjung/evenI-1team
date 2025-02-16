using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TransformationState : ITransformation
{
    private PlayerTransformationController transformController;
    private TransformationData transformationData;

    private int specialAbilityUses;

    private Coroutine transformationCoroutine;

    

    public TransformationState(PlayerTransformationController transformController, 
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.specialAbilityUses = transformationData.abilityUsageLimit;
    }



    public void Activate()
    {
        // ���� ���� �ð� Ÿ�̸� ����
        transformController.StartTransformationTimer(this);

        // ���� �ִϸ��̼� ����
    }



    public void UseSpecialAbility()
    {
        // Ƚ�� ���� �� ���� ����
    }




    public void Deactivate()
    {
        // ���� ���� �ִϸ��̼� ����

        // �⺻ ���·� ����
        transformController.ChangeState(new NormalState(transformController));
    }


    // ���� ����� ���� ã��, ���������� ������ �ٰ��� ��� ����
    private void PerformNinjaAbility()
    {

        // �÷��̾ ���� ��ġ�� �����̵�

        // ���� �ִϸ��̼� ����

        // ������ ������ ����
    }

}

