using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

/*
 
Ŭ���� ����:
���� �ý����� ���� �������̽�

*/

public interface ITransformation
{
    void Activate();    // ���� ����
    void UseSpecialAbility();   // Ư�� �ɷ� ���
    void Deactivate();  // ���� ����
}


public class NormalState : ITransformation
{
    private PlayerTransformationController transformationController;
    
    private GameManager gameManager; // GameManager ����



    // ������ �����ų� ������ ������ �� ���¸� ���� (PlayerTransformationController)
    public NormalState(PlayerTransformationController transformationController)
    {
        // this.transformationController�� Ŭ���� ������ �ʵ�(��� ����)
        // transformationController�� �������� �Ű�����(�Ķ����)
        // this�� ������� ������ �Ű������� Ŭ������ �ʵ�(��� ����)�� �򰥸� �� �ֱ� ������,
        // ��Ȯ�ϰ� �����ϱ� ���� ���
        this.transformationController = transformationController;

        // GameManager �ʱ�ȭ
        gameManager = GameManager.Instance;
    }



    public void Activate()
    {
        if (gameManager == null) return;
    }


    public void UseSpecialAbility() 
    {
        if (gameManager == null) return;
    }


    public void Deactivate() 
    {
        if (gameManager == null) return;
    }
}
