using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/*

클래스 설명:
플레이어가 변신했을 때의 상태를 관리
 
*/

public class TransformationState : ITransformation
{
    private PlayerAnimationController PlayerAnimationController;
    private GameManager gameManager;

    private PlayerTransformationController transformController;
    private TransformationData transformationData;

    private int abilityUsageCount; // 특수 능력 사용 횟수 관리


    public TransformationState(PlayerTransformationController transformController,
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.abilityUsageCount = transformationData.specialAbility.maxUsageCount;

    }


    // 변신 활성화
    public void Activate()
    {
        Debug.Log("변신 진입 성공");

    }


    // 특수 능력 사용(횟수)
    public void UseSpecialAbility()
    {
        if (abilityUsageCount <= 0)
            return;

        transformationData.specialAbility.ActivateAbility(transformController.transform);
        abilityUsageCount--;


        // 특수 능력 사용 횟수를 모두 소진했을 경우 변신 해제 애니메이션 실행
        if (abilityUsageCount <= 0)
        {

            transformController.StartRevertProcess();
        }
    }




    public void Deactivate()
    {
        Debug.Log("변신 해제 성공");
        transformController.StartRevertProcess();

    }


    // 능력 사용 횟수 소진 여부 확인
    public bool IsAbilityUsageDepleted()
    {
        return abilityUsageCount <= 0;
    }

}

