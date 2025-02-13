using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Normal,         // 일반 상태
    transformation, // 변신 상태
    Fever,          // 피버 모드 상태
    Invincible,     // 무적 상태
    // 투명 상태
    Dead            // 사망 상태
}

public enum TransformationType
{
    Normal,     // 기본 개구리
    Lotus,      // 연꽃 개구리
    Bull,       // 황소 개구리
    Ninja,      // 닌자 개구리
    Golden,     // 황금 개구리 (피버)
}

public enum TransformationItemType
{
    Coin,
    Transformation,
    Consumable
}

public class EnumData : MonoBehaviour
{

}

