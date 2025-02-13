using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public interface ITransformation
{
    void Activate(); // ���� ����
    void UseSpecialAbility(); // Ư�� �ɷ� ���
    void Deactivate(); // ���� ����
}


public class NormalState : ITransformation
{
    private PlayerTransformationController transformationController;

    public NormalState(PlayerTransformationController transformationController)
    {
        this.transformationController = transformationController;
    }

    public void Activate()
    {

    }

    public void UseSpecialAbility() 
    {
    
    }


    public void Deactivate() { }
}
