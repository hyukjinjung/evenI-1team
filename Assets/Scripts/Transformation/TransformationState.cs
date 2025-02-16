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
        // 변신 지속 시간 타이머 시작
        transformController.StartTransformationTimer(this);

        // 변신 애니메이션 실행
    }



    public void UseSpecialAbility()
    {
        // 횟수 차감 후 변신 해제
    }




    public void Deactivate()
    {
        // 변신 해제 애니메이션 실행

        // 기본 상태로 복귀
        transformController.ChangeState(new NormalState(transformController));
    }


    // 가장 가까운 적을 찾아, 순간적으로 적에게 다가가 즉시 공격
    private void PerformNinjaAbility()
    {

        // 플레이어를 적의 위치로 순간이동

        // 공격 애니메이션 실행

        // 적에게 데미지 적용
    }

}

