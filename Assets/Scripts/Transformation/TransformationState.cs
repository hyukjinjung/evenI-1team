using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TransformationState : ITransformation
{
    private PlayerTransformationController transformController;
    private PlayerAnimationController PlayerAnimationController;

    private TransformationData transformationData;

    private GameManager gameManager;

    

    public TransformationState(PlayerTransformationController transformController, 
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.gameManager = GameManager.Instance; // Start()���� �� ���� �Ҵ�
        this.PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();

        // GameManager �ʱ�ȭ
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();
    }



    public void Activate()
    {
        if (gameManager == null)
            return;


        // ���� ���� �ð� Ÿ�̸� ����
        transformController.StartTransformationTimer(this, transformationData.duration);

    }



    public void UseSpecialAbility()
    {
        if (gameManager == null) // GameManager�� NULL�̸� ����
            return;

        // Ƚ�� ���� �� ���� ����
        transformController.ChangeState(new NormalState(transformController));
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

