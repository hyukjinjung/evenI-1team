using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewSpecialAbility", menuName = "SpecialAbilities/New Special Ability")]


public abstract class SpecialAbilityData : ScriptableObject
{
    public string abilityName;                  // 능력 이름
    public int effectRange;                     // 적용 칸 수
    public float efffectValue;                  // 능력 적용 값 (공격력)
    public int maxUsageCount;                   // 특수 능력 사용 가능 횟수

    //public AddScoreConditionType scoreCondition;   // 점수 추가 조건
    //public AddScoreTargetType scoreTarget;         // 점수 추가 대상
    //public float coreMultiplier;                   // 점수 추가 배율


    //public GameObject abilityEffectPrefab;      // 능력 사용 시 나타나는 이펙트
    



    // 능력 사용 시 나타나는 이펙트 프리펩


    public abstract void ActivateAbility(Transform playerTransform);

}
