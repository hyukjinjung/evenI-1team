using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TransformationState : ITransformation
{
    private PlayerTransformationController transformController;
    private TransformationType transformType;
    private float duration;
    private int specialAbilityUses;
    private Coroutine transformationCoroutine;

    private Transform playerTransform; // 플레이어 위치 참조



    public TransformationState(PlayerTransformationController transformController, 
        TransformationType transformType, float duration, int specialAbilityUses)
    {
        this.transformController = transformController;
        this.transformType = transformType;
        this.duration = duration;
        this.specialAbilityUses = specialAbilityUses;
        this.playerTransform = transformController.transform; // 플레이어 Transform 저장
    }



    public void Activate()
    {
        // 변신 지속 시간 타이머 시작
        transformController.StartTransformationTimer(this);

        // 변신 애니메이션 실행
    }



    public void UseSpecialAbility()
    {
        if (specialAbilityUses > 0)
        {
            specialAbilityUses--;

            // 변신 타입에 따라 다른 특수 능력
            switch (transformType)
            {
                case TransformationType.NinjaFrog:
                    PerformNinjaAbility();
                    break;
            }

            if (specialAbilityUses == 0)
            {
                Debug.Log($"{transformType} 특수 능력 횟수 소진. 변신 해제");

                Deactivate();
            }
        }
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
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy == null)
            return;

        // 플레이어를 적의 위치로 순간이동
        playerTransform.position = nearestEnemy.transform.position;

        // 공격 애니메이션 실행

        // 적에게 데미지 적용
    }

    // 가장 가까운 적을 찾는 메서드
    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");

        if (enemies.Length == 0) return null;

        GameObject nearestEnemy = null;
        float minDistance = float.MaxValue;
        Vector2 playerPosition = playerTransform.position;

        foreach (GameObject enemy in enemies)
        {
            float distanceSqr = (playerPosition - (Vector2)enemy.transform.position).sqrMagnitude;

            if (distanceSqr < minDistance)
            {
                minDistance = distanceSqr;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}

