using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
클래스 설명:
각 변신 형태에 따른 특수능력을 
SpecialAbilityData를 상속하여 구현
*/

[CreateAssetMenu(fileName = "NewNinjaAbility", menuName = "SpecialAbilities/Ninja")]

public class NinjaAbility : SpecialAbilityData
{
    public GameObject ninjaAttackEffect; // 타격 이펙트 프리팹


    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private PlayerCollisionController playerCollisionController;
    private TestTileManager testTileManager;
    private PlayerMovement playerMovement;


    private void Initialize(Transform playerTransform)
    {
        playerAnimationController = playerTransform.GetComponent<PlayerAnimationController>();
        playerTransformationController = playerTransform.GetComponent<PlayerTransformationController>();
        playerCollisionController = playerTransform.GetComponent<PlayerCollisionController>();
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        
        testTileManager = GameManager.Instance.tileManager;
    }

    public override void ActivateAbility(Transform playerTransform, TransformationData transformationData)
    {
        Initialize(playerTransform);


        if (GameManager.Instance == null)
        {
            Debug.Log("GameManager NULL");
            return;
        }

        if (playerCollisionController == null) return;

        playerCollisionController.EnableMonsterIgnore(transformationData.duration);
        Debug.Log("몬스터 충돌 무시 활성화");
        
        Tile monsterTile = testTileManager.GetNextMonsterTile(playerMovement.CurrentFloor);
        
        if (monsterTile == null)
        {
            return;
        }


        Monster targetMonster = monsterTile.MonsterOnTile;

        if (targetMonster == null)
        {
            Debug.Log("타일에 몬스터가 존재하지 않음");
            return;
        }
        
        playerTransformationController.StartCoroutine(ExecuteAttackAfterDisappear(
            playerTransform, monsterTile, targetMonster));

    }

    private IEnumerator ExecuteAttackAfterDisappear(Transform playertransform, Tile targetTile,
        Monster targetMonster)
    {
        playerAnimationController.PlayDisappearAnimation();
        float disappearTime = playerAnimationController.GetDisappearAnimationLength();
        yield return new WaitForSeconds(disappearTime);

        playerAnimationController.ResetTrigger("Disappear");

        Vector3 newPosition = targetTile.transform.position + new Vector3(0, 1, 0);
        playertransform.position = newPosition;

        SpawnAttackEffect(newPosition);

        playerAnimationController.PlayAssassinationAnimation();
        float assassinationTime = playerAnimationController.GetAssassinationAnimationLength();

        yield return new WaitForSeconds(assassinationTime);
        
        // targetMonster.TakeDamage((int)effectValue);
        
        playerAnimationController.StartRevertAnimation();
    }



    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;

        Vector3 effectPosition = position + new Vector3(0, -1.5f, 0);
        Instantiate(ninjaAttackEffect, effectPosition, Quaternion.identity);
    }
}
