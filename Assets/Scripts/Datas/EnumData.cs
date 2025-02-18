using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Normal,         // 일반 상태
    Transformation, // 변신 상태
    Fever,          // 피버 모드 상태
    Invincible,     // 무적 상태
}

public enum TransformationType
{
    NormalFrog,     // 기본 개구리
    NinjaFrog,      // 닌자 개구리
    BullFrog,       // 황소 개구리
    LotusFrog,      // 연꽃 개구리
    FlyingFrog,     // 비행 개구리
    MusicianFrog,   // 악사 개구리
    MagicianFrog,   // 마술 개구리
    GoldenFrog,     // 황금 개구리 (피버)
}

public enum ItemType
{
    Coin,
    Transformation,
    Consumable
}

public enum TransformationItemType
{
    Normal,
    Lotus,
    Bull,
    Ninja,
    Golden

}

public enum AddScoreConditionType
{
    OnKill,
    OnTransformEnd,
    OnSpecialAbilityUse
}

public enum AddScoreTargetType
{
    None = 0,               // 없음
    Item = 1,               // 아이템
    Monster = 2,            // 몬스터
    ObstacleItem = 3,       // 방해 아이템
    Coin = 4,               // 코인
    ObstaclePattern = 5,    // 방해 패턴
    Movement = 6,           // 이동
    SpecialKeyUse = 7,      // 특수키 사용
    Fall = 8,               // 추락
    TrackingMonster = 9     // 추적 몬스터
}



public class EnumData : MonoBehaviour
{

}

