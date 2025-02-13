using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public interface ITransformation
{
    void Activate(); // 변신 시작
    void UseSpecialAbility(); // 특수 능력 사용
    void Deactivate(); // 변신 종료
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
