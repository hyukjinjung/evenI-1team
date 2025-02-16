using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public interface ITransformation    // 변신 시스템을 위한 인터페이스
{
    void Activate();    // 변신 시작
    void UseSpecialAbility();   // 특수 능력 사용
    void Deactivate();  // 변신 종료
}


public class NormalState : ITransformation
{
    private PlayerTransformationController transformationController;



    // 변신이 끝나거나 변신을 시작할 때 상태를 변경 (PlayerTransformationController)
    public NormalState(PlayerTransformationController transformationController)
    {
        // this.transformationController는 클래스 내부의 필드(멤버 변수)
        // transformationController는 생성자의 매개변수(파라미터)
        // this를 사용하지 않으면 매개변수와 클래스의 필드(멤버 변수)가 헷갈릴 수 있기 때문에,
        // 명확하게 구분하기 위해 사용
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
