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

    private Transform playerTransform; // �÷��̾� ��ġ ����



    public TransformationState(PlayerTransformationController transformController, 
        TransformationType transformType, float duration, int specialAbilityUses)
    {
        this.transformController = transformController;
        this.transformType = transformType;
        this.duration = duration;
        this.specialAbilityUses = specialAbilityUses;
        this.playerTransform = transformController.transform; // �÷��̾� Transform ����
    }



    public void Activate()
    {
        // ���� ���� �ð� Ÿ�̸� ����
        transformController.StartTransformationTimer(this);

        // ���� �ִϸ��̼� ����
    }



    public void UseSpecialAbility()
    {
        if (specialAbilityUses > 0)
        {
            specialAbilityUses--;

            // ���� Ÿ�Կ� ���� �ٸ� Ư�� �ɷ�
            switch (transformType)
            {
                case TransformationType.NinjaFrog:
                    PerformNinjaAbility();
                    break;
            }

            if (specialAbilityUses == 0)
            {
                Debug.Log($"{transformType} Ư�� �ɷ� Ƚ�� ����. ���� ����");

                Deactivate();
            }
        }
    }




    public void Deactivate()
    {
        // ���� ���� �ִϸ��̼� ����

        // �⺻ ���·� ����
        transformController.ChangeState(new NormalState(transformController));
    }


    // ���� ����� ���� ã��, ���������� ������ �ٰ��� ��� ����
    private void PerformNinjaAbility()
    {
        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy == null)
            return;

        // �÷��̾ ���� ��ġ�� �����̵�
        playerTransform.position = nearestEnemy.transform.position;

        // ���� �ִϸ��̼� ����

        // ������ ������ ����
    }

    // ���� ����� ���� ã�� �޼���
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

