using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public interface ITransformation    // ���� �ý����� ���� �������̽�
{
    void Activate();    // ���� ����
    void UseSpecialAbility();   // Ư�� �ɷ� ���
    void Deactivate();  // ���� ����
}


public class NormalState : ITransformation
{
    private PlayerTransformationController transformationController;



    // ������ �����ų� ������ ������ �� ���¸� ���� (PlayerTransformationController)
    public NormalState(PlayerTransformationController transformationController)
    {
        // this.transformationController�� Ŭ���� ������ �ʵ�(��� ����)
        // transformationController�� �������� �Ű�����(�Ķ����)
        // this�� ������� ������ �Ű������� Ŭ������ �ʵ�(��� ����)�� �򰥸� �� �ֱ� ������,
        // ��Ȯ�ϰ� �����ϱ� ���� ���
        this.transformationController = transformationController;
    }



    public void Activate()
    {

    }


    public void UseSpecialAbility() 
    {
        
    }


    public void Deactivate() 
    { 
    
    }
}
