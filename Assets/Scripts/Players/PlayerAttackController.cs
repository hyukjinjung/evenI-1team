using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{

    private bool isAttacking = false;
    private bool isTransformed = false;

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;
    private PlayerTransformationController playerTransformationController;

    [SerializeField] private int currentFloor = 0;

    private GameManager gameManager;


    public SpecialAbilityData specialAbilityData;

    [SerializeField] private GameObject attackEffectPrefab;


    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
            return;

        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        playerTransformationController = GetComponent<PlayerTransformationController>();

        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;

    }


    public void SetTransformedState(bool transformed, SpecialAbilityData ability)
    {

        isTransformed = transformed;
        specialAbilityData = ability;
        Debug.Log($"���� ���� ���� - ���� ����: {transformed}, Ư�� �ɷ�: {(ability != null ? "������" : "NULL")}");


    }


    void PerformAttack()
    {
        if (isAttacking)
        {
            Debug.Log("���� ���� �� �߰� ���� �Ұ���");
            return;
        }


        if (isTransformed && specialAbilityData != null)
        {
            TransformationData currentTransformationData =
            playerTransformationController.GetCurrentTransformationData();

            if (currentTransformationData == null)
            {
                NormalAttack();
                return;
            }

            TransformationState currentState = 
                playerTransformationController.GetCurrentState() as TransformationState;

            if (currentState != null)
            {
                if (currentState.GetRemainingAbilityUses() > 0)
                {
                    currentState.UseSpecialAbility();
                    Debug.Log($"�ɷ� ��� �� ���� Ƚ�� {currentState.GetRemainingAbilityUses()}");
                }
                else
                {
                    Debug.Log("Ư�� �ɷ� Ƚ�� 0. �⺻ ���� ����");
                    NormalAttack();
                }
            }

            else
            {
                Debug.Log("Ư�� �ɷ� ���� ����. �⺻ ���� ����");
                NormalAttack();
            }
        }
        else
        {
            Debug.Log("���� ���� �ƴ�. �⺻ ���� ����");
            NormalAttack();

        }
    }


    void NormalAttack()
    {
        Tile forwardTile = testTileManager.GetForwardTile(transform.position);
        if (forwardTile == null) return;


        bool attackLeft = forwardTile.TileOnLeft(transform);

        playerAnimationController.SetAttacking(attackLeft);

        isAttacking = true;
        Debug.Log("���� ����");
        StartCoroutine(ResetAttackFlag());

        SpawnAttackEffect(forwardTile, attackLeft);

    }


    void SpawnAttackEffect(Tile forwardTile, bool attackLeft)
    {
        if (attackEffectPrefab == null || forwardTile == null)
        {
            Debug.LogError("Ÿ�� ����Ʈ ������ ���� ��ġ�� �������� ����");
            return;
        }

        Vector3 spawnPosition = forwardTile.transform.position;

        Quaternion rotation = attackLeft ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;

        GameObject effect = Instantiate(attackEffectPrefab, spawnPosition, rotation);
    }


    IEnumerator ResetAttackFlag()
    {
        float attackAnimationLength = playerAnimationController.GetAttackAniamtionLength();

        yield return new WaitForSeconds(attackAnimationLength);

        isAttacking = false;
        Debug.Log("���� ���� �غ�");
    }

    public void ResetAttackState()
    {
        isAttacking = false ;
        Debug.Log("���� ���� �ʱ�ȭ �Ϸ�. �⺻ ���� ����");
    }
}


