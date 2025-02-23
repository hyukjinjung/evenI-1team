using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TransformationState : ITransformation
{
    private PlayerTransformationController transformController;
    private PlayerAnimationController PlayerAnimationController;

    private TransformationData transformationData;

    private GameManager gameManager;

    

    public TransformationState(PlayerTransformationController transformController, 
        TransformationData transformationData)
    {
        this.transformController = transformController;
        this.transformationData = transformationData;
        this.gameManager = GameManager.Instance; // Start()에서 한 번만 할당
        this.PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();

        // GameManager 초기화
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }

        PlayerAnimationController = transformController.GetComponent<PlayerAnimationController>();
    }



    public void Activate()
    {
        if (gameManager == null)
            return;


        // 변신 지속 시간 타이머 시작
        transformController.StartTransformationTimer(this, transformationData.duration);

    }



    public void UseSpecialAbility()
    {
        if (gameManager == null) // GameManager가 NULL이면 종료
            return;

        // 횟수 차감 후 변신 해제
        transformController.ChangeState(new NormalState(transformController));
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

