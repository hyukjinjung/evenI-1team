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
    LotusFrog,      // 연꽃 개구리
    BullFrog,       // 황소 개구리
    NinjaFrog,      // 닌자 개구리
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

public class EnumData : MonoBehaviour
{

}

