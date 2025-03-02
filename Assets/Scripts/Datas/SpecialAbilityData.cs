using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   Ŭ���� ����: 
   ��� Ư�� �ɷ��� ���� �������̽��� �����ϰ�,
   ���� �ɷº��� ����Ͽ� ����� ����
*/

[CreateAssetMenu(fileName = "NewSpecialAbility", menuName = "SpecialAbilities/New Special Ability")]


public abstract class SpecialAbilityData : ScriptableObject
{
    public string abilityName;                  // �ɷ� �̸�
    public int effectRange;                     // ���� ĭ ��
    public float effectValue;                  // �ɷ� ���� �� (���ݷ�)
    public int maxUsageCount;                   // Ư�� �ɷ� ��� ��z�� Ƚ��

    //public AddScoreConditionType scoreCondition;   // ���� �߰� ����
    //public AddScoreTargetType scoreTarget;         // ���� �߰� ���
    //public float coreMultiplier;                   // ���� �߰� ����


    //public GameObject abilityEffectPrefab;      // �ɷ� ��� �� ��Ÿ���� ����Ʈ


    // �ɷ� ��� �� ��Ÿ���� ����Ʈ ������


    public abstract void ActivateAbility(Transform playerTransform, TransformationData transformationData);

}
