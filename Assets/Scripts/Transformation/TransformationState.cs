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

    private PlayerTransformationController transformController;
    private TransformationData transformationData;
    private PlayerAttackController attackController;

    private int abilityUsageCount; // 특수 능력 사용 횟수 관리

    


    public TransformationState(PlayerTransformationController transformController,
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.abilityUsageCount = transformationData.specialAbility.maxUsageCount;

        Initialize();
    }

    private void Initialize()
    {
        attackController = transformController.GetComponent<PlayerAttackController>();
    }


    public void Activate()
    {
        transformController.Transform(transformationData);
        Debug.Log($"변신 진입 성공. {transformationData.transformationType}");

    }



    public void UseSpecialAbility()
    {
        Debug.Log($"특수 능력 사용 전. 현재 남은 횟수 {abilityUsageCount} ");


        if (abilityUsageCount <= 0)
        {
            Debug.Log($"특수 능력 사용 횟수 0. 현재 남은 횟수 {abilityUsageCount}");
            return;
        }

        transformationData.specialAbility.ActivateAbility(transformController.transform, transformationData);
        abilityUsageCount--;

        Debug.Log($"특수 능력 사용 완료. 현재 남은 횟수 {abilityUsageCount}");


        if (abilityUsageCount <= 0)
        {
            transformController.ResetTransformationTimer();
            transformController.DeTransform();

        }

        if (attackController != null)
        {
            attackController.ResetAttackState();
        }
    }




    public void Deactivate()
    {
        Debug.Log("변신 해제 성공");
        transformController.DeTransform();


    }



    public int GetRemainingAbilityUses()
    {
        return abilityUsageCount;
    }

}

