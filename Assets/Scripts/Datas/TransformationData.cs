using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTransformation", menuName = "Transformation/New Transformation")]


// ���� Ÿ��, ���� ���� �ð�, �ɷ� ��� Ƚ�� ����
public class TransformationData : ScriptableObject
{
    public TransformationType transformationType;   // ���� ����
    public bool canTransform;                       // ���� ���� ����
    public float duration;                          // ���� ���� �ð�

    public SpecialAbilityData specialAbility;       // ���� �� ����� Ư�� �ɷ�
    public int abilityUsageLimit;                   // Ư�� �ɷ� ��� ���� Ƚ��

    public ScoreConditionType scoreCondition;       // ���� �߰� ����
    public ScoreTargetType scoreTarget;             // ���� �߰� ���
    public float scoreMultiplier;                   // ���� �߰� ����

    public GameObject transformationPrefab;         // ������ ĳ���� ������
    public GameObject transformationEffect;         // ���� �� ��Ÿ���� ����Ʈ

}
