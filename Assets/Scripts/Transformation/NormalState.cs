using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

/*
 
클래스 설명:
변신 시스템을 위한 인터페이스

*/

public interface ITransformation
{
    void Activate();    // 변신 시작
    void UseSpecialAbility();   // 특수 능력 사용
    void Deactivate();  // 변신 종료
}


public class NormalState : ITransformation
{
    private PlayerTransformationController transformationController;
    
    private GameManager gameManager; // GameManager 참조



    // 변신이 끝나거나 변신을 시작할 때 상태를 변경 (PlayerTransformationController)
    public NormalState(PlayerTransformationController transformationController)
    {
        // this.transformationController는 클래스 내부의 필드(멤버 변수)
        // transformationController는 생성자의 매개변수(파라미터)
        // this를 사용하지 않으면 매개변수와 클래스의 필드(멤버 변수)가 헷갈릴 수 있기 때문에,
        // 명확하게 구분하기 위해 사용
        this.transformationController = transformationController;

        // GameManager 초기화
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
