using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{

    private bool isAttacking = false;

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;
    private PlayerTransformationController transformController;

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
        transformController = GetComponent<PlayerTransformationController>();

        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;

    }

    void PerformAttack()
    {
        if (isAttacking)
        {
            Debug.Log("���� ���� �� �߰� ���� �Ұ���");
            return;
        }
        
        if (transformController.IsTransformed())
        {
            TransformationData currentTransformationData = transformController.currentTransformationData;
            specialAbilityData = currentTransformationData.specialAbility;
            
            transformController.UseSpecialAbility();
        }
        else
        {
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


