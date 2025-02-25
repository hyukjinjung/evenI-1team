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
        //this.gameManager = GameManager.Instance; // Start()에서 한 번만 할당
        this.PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();

        // 특수 능력 사용 횟수 설정
        abilityUsageCount = transformationData.specialAbility.maxUsageCount;

    }


    // 변신 활성화
    public void Activate()
    {
        //if (gameManager == null)
        //    return;

        // 변신 지속 시간이 지나면 자동으로 변신이 해제되도록
        // 변신 지속 시간 타이머 시작
        //transformController.StartTransformationTimer(this, transformationData.duration);

    }


    // 특수 능력 사용(횟수)
    public void UseSpecialAbility()
    {
        if (gameManager == null && abilityUsageCount <= 0)
            return;

        transformationData.specialAbility.ActivateAbility(transformController.transform);
        abilityUsageCount--;

        // 특수 능력 사용 횟수를 모두 소진했을 경우 변신 해제 애니메이션 실행
        if (abilityUsageCount <= 0)
        {
            Debug.Log("특수 능력 사용 완료. 변신 해제 시작");
            Deactivate();
        }
    }




    public void Deactivate()
    {

        if (transformController != null)
        {
            transformController.StartRevertProcess();
        }
        

        // 변신 해제 후, 일정 시간 동안 몬스터 충돌 무시
        // 변신 해제 후 게임 오버 되는 오류 발생
        PlayerCollisionController collisionController = transformController.GetComponent< PlayerCollisionController>();
        if (collisionController != null)
        {
            collisionController.EnableMonsterIgnore(0.2f);
        }


        // NormalFrog 상태로 변경
        transformController.ChangeState(new NormalState(transformController));
    }


    // 능력 사용 횟수 소진 여부 확인
    public bool IsAbilityUsageDepleted()
    {
        return abilityUsageCount <= 0;
    }

}

