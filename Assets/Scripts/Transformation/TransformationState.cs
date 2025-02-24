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
        this.gameManager = GameManager.Instance; // Start()에서 한 번만 할당
        this.PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();

        // 특수 능력 사용 횟수 설정
        abilityUsageCount = transformationData.specialAbility.maxUsageCount; 

    }


    // 변신 활성화
    public void Activate()
    {
        if (gameManager == null)
            return;

        // 변신 지속 시간이 지나면 자동으로 변신이 해제되도록
        // 변신 지속 시간 타이머 시작
        transformController.StartTransformationTimer(this, transformationData.duration);

    }


    // 특수 능력 사용(횟수)
    public void UseSpecialAbility()
    {
        if (gameManager == null && abilityUsageCount <= 0)
            return;

        transformationData.specialAbility.ActivateAbility(transformController.transform);
        abilityUsageCount--;

        if (abilityUsageCount <= 0)
        {
            Deactivate();
        }
    }




    public void Deactivate()
    {
        if (gameManager == null) // GameManager가 NULL이면 종료
            return;

        // 변신 해제 애니메이션 실행
        if (transformController != null)
        {
            PlayerAnimationController.PlayerTransformationAnimation(TransformationType.NormalFrog);
        }

        // 기본 상태로 복귀
        transformController.ChangeState(new NormalState(transformController));
    }


}

