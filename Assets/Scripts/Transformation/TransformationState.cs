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

    private GameManager gameManager; // GameManager 참조

    

    public TransformationState(PlayerTransformationController transformController, 
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;

        // GameManager 초기화
        gameManager = GameManager.Instance;
    }



    public void Activate()
    {
        if (GameManager.Instance == null) // GameManager가 NULL이면 종료
        {
            Debug.Log("[TransformationState] GameManager 재설정");
            gameManager = GameManager.Instance;

            if (gameManager == null)
                return; 
        }

        // 변신 지속 시간 타이머 시작
        transformController.StartTransformationTimer(this);

        // 변신 애니메이션 실행
    }



    public void UseSpecialAbility()
    {
        if (gameManager == null) // GameManager가 NULL이면 종료
            return;

        // 횟수 차감 후 변신 해제
    }




    public void Deactivate()
    {
        if (gameManager == null) // GameManager가 NULL이면 종료
            return;

        // 변신 해제 애니메이션 실행

        // 기본 상태로 복귀
        transformController.ChangeState(new NormalState(transformController));
    }


    // 가장 가까운 적을 찾아, 순간적으로 적에게 다가가 즉시 공격
    private void PerformNinjaAbility()
    {
        if (gameManager == null) // GameManager가 NULL이면 종료
            return;

        // 플레이어를 적의 위치로 순간이동

        // 공격 애니메이션 실행

        // 적에게 데미지 적용
    }

}

