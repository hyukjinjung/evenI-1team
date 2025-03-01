using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
   클래스 설명: 
   모든 특수 능력을 공통 인터페이스로 관리하고,
   개별 능력별로 상속하여 기능을 구현
*/

[CreateAssetMenu(fileName = "NewSpecialAbility", menuName = "SpecialAbilities/New Special Ability")]


public abstract class SpecialAbilityData : ScriptableObject
{
    public string abilityName;                  // 능력 이름
    public int effectRange;                     // 적용 칸 수
    public float effectValue;                  // 능력 적용 값 (공격력)
    public int maxUsageCount;                   // 특수 능력 사용 가z능 횟수

    //public AddScoreConditionType scoreCondition;   // 점수 추가 조건
    //public AddScoreTargetType scoreTarget;         // 점수 추가 대상
    //public float coreMultiplier;                   // 점수 추가 배율


    //public GameObject abilityEffectPrefab;      // 능력 사용 시 나타나는 이펙트


    // 능력 사용 시 나타나는 이펙트 프리펩


    public abstract void ActivateAbility(Transform playerTransform, TransformationData transformationData);

}
