using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TransformationState : ITransformation
{
    private PlayerTransformationController transformController;
    private TransformationData transformationData;

    //private int specialAbilityUses;

    private Coroutine transformationCoroutine;

    private GameManager gameManager; // GameManager ����

    

    public TransformationState(PlayerTransformationController transformController, 
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;

        // GameManager �ʱ�ȭ
        gameManager = GameManager.Instance;
    }



    public void Activate()
    {
        if (GameManager.Instance == null) // GameManager�� NULL�̸� ����
        {
            Debug.Log("[TransformationState] GameManager �缳��");
            gameManager = GameManager.Instance;

            if (gameManager == null)
                return; 
        }

        // ���� ���� �ð� Ÿ�̸� ����
        transformController.StartTransformationTimer(this);

        // ���� �ִϸ��̼� ����
    }



    public void UseSpecialAbility()
    {
        if (gameManager == null) // GameManager�� NULL�̸� ����
            return;

        // Ƚ�� ���� �� ���� ����
    }




    public void Deactivate()
    {
        if (gameManager == null) // GameManager�� NULL�̸� ����
            return;

        // ���� ���� �ִϸ��̼� ����

        // �⺻ ���·� ����
        transformController.ChangeState(new NormalState(transformController));
    }


    // ���� ����� ���� ã��, ���������� ������ �ٰ��� ��� ����
    private void PerformNinjaAbility()
    {
        if (gameManager == null) // GameManager�� NULL�̸� ����
            return;

        // �÷��̾ ���� ��ġ�� �����̵�

        // ���� �ִϸ��̼� ����

        // ������ ������ ����
    }

}

