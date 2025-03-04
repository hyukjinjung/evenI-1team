using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTransformation", menuName = "Transformation/New Transformation")]


// ���� Ÿ��, ���� ���� �ð�, �ɷ� ��� Ƚ�� ����
public class TransformationData : ScriptableObject
{
    public TransformationType transformationType;   // ���� ����
    public float duration;                          // ���� ���� �ð�

    public SpecialAbilityData specialAbility;       // ���� �� ����� Ư�� �ɷ�

    public GameObject transformationPrefab;         // ������ ĳ���� ������


    //public AddScoreConditionType addScoreCondition;    // �߰� ���� ����
    //public AddScoreTargetType addScoreTarget;          // �߰� ���� ���
    //public float addScoreMultiplier;                   // �߰� ���� ����


    //public GameObject transformationEffect;         // ���� �� ��Ÿ���� ����Ʈ

}
