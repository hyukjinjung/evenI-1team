using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTransformation", menuName = "Transformation/New Transformation")]


// 변신 타입, 변신 지속 시간, 능력 사용 횟수 관리
public class TransformationData : ScriptableObject
{
    public TransformationType transformationType;   // 변신 유형
    public bool canTransform;                       // 변신 가능 여부
    public float duration;                          // 변신 지속 시간

    public SpecialAbilityData specialAbility;       // 변신 시 사용할 특수 능력
    public int abilityUsageLimit;                   // 특수 능력 사용 가능 횟수

    public ScoreConditionType scoreCondition;       // 점수 추가 조건
    public ScoreTargetType scoreTarget;             // 점수 추가 대상
    public float scoreMultiplier;                   // 점수 추가 배율

    public GameObject transformationPrefab;         // 변신할 캐릭터 프리팹
    public GameObject transformationEffect;         // 변신 시 나타나는 이펙트

}
