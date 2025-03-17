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
    private PlayerMovement playerMovement;

    // [SerializeField] private int currentFloor = 0;

    private GameManager gameManager;


    private SpecialAbilityData specialAbilityData;

    [SerializeField] private GameObject attackEffectPrefab;


    private void Start()
    {
        gameManager = GameManager.Instance;

        if (gameManager == null)
            return;

        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        transformController = GetComponent<PlayerTransformationController>();
        playerMovement = GetComponent<PlayerMovement>();

        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;

    }

    public void PerformAttack()
    {
        if (FeverSystem.Instance !=null && FeverSystem.Instance.IsFeverActive) 
            return;

        if (playerMovement.isJumping) return;


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

        SpawnAttackEffect();
        StartCoroutine(ResetAttackFlag());

        //isAttacking = true;

    }



    public void SpawnAttackEffect()
    {
        if (attackEffectPrefab == null)
            return;

        Tile forwardTile = testTileManager.GetForwardTile(transform.position);

        bool attackLeft = forwardTile.TileOnLeft(transform);

        Vector3 spawnPosition = forwardTile.transform.position;
        Quaternion rotation = attackLeft ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;

        Instantiate(attackEffectPrefab, spawnPosition, rotation);
    }


    IEnumerator ResetAttackFlag()
    {
        float attackAnimationLength = playerAnimationController.GetAttackAniamtionLength();

        yield return new WaitForSeconds(attackAnimationLength);

        //isAttacking = false;
        Debug.Log("���� ���� �غ�");
    }

    public void ResetAttackState()
    {
        isAttacking = false;
    }
}


