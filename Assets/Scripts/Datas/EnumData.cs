using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Normal,         // �Ϲ� ����
    transformation, // ���� ����
    Fever,          // �ǹ� ��� ����
    Invincible,     // ���� ����
    // ���� ����
    Dead            // ��� ����
}

public enum TransformationType
{
    Normal,     // �⺻ ������
    Lotus,      // ���� ������
    Bull,       // Ȳ�� ������
    Ninja,      // ���� ������
    Golden,     // Ȳ�� ������ (�ǹ�)
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

