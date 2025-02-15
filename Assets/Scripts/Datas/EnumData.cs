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
    LotusFrog,      // ���� ������
    BullFrog,       // Ȳ�� ������
    NinjaFrog,      // ���� ������
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

public class EnumData : MonoBehaviour
{

}

