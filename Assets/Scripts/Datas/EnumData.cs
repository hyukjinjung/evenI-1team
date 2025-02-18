using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Normal,         // �Ϲ� ����
    Transformation, // ���� ����
    Fever,          // �ǹ� ��� ����
    Invincible,     // ���� ����
}

public enum TransformationType
{
    NormalFrog,     // �⺻ ������
    NinjaFrog,      // ���� ������
    BullFrog,       // Ȳ�� ������
    LotusFrog,      // ���� ������
    FlyingFrog,     // ���� ������
    MusicianFrog,   // �ǻ� ������
    MagicianFrog,   // ���� ������
    GoldenFrog,     // Ȳ�� ������ (�ǹ�)
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
    None = 0,               // ����
    Item = 1,               // ������
    Monster = 2,            // ����
    ObstacleItem = 3,       // ���� ������
    Coin = 4,               // ����
    ObstaclePattern = 5,    // ���� ����
    Movement = 6,           // �̵�
    SpecialKeyUse = 7,      // Ư��Ű ���
    Fall = 8,               // �߶�
    TrackingMonster = 9     // ���� ����
}



public class EnumData : MonoBehaviour
{

}

