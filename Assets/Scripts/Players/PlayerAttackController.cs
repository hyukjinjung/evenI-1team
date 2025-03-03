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
            Debug.Log("공격 중일 때 추가 공격 불가능");
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
        Debug.Log("공격 시작");
        StartCoroutine(ResetAttackFlag());

        SpawnAttackEffect(forwardTile, attackLeft);
    }



    void SpawnAttackEffect(Tile forwardTile, bool attackLeft)
    {
        if (attackEffectPrefab == null || forwardTile == null)
        {
            Debug.LogError("타격 이펙트 프리팹 생성 위치가 설정되지 않음");
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
        Debug.Log("다음 공격 준비");
    }

    public void ResetAttackState()
    {
        isAttacking = false ;
        Debug.Log("공격 상태 초기화 완료. 기본 공격 가능");
    }
}


