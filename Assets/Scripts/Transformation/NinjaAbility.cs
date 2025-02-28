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


    PlayerAnimationController playerAnimationController;
    PlayerTransformationController playerTransformationController;
    PlayerCollisionController playerCollisionController;
    Rigidbody2D rb;
    private TestTileManager testTileManager;


    private void Initialize(Transform playerTransform)
    {
        playerAnimationController = playerTransform.GetComponent<PlayerAnimationController>();
        playerTransformationController = playerTransform.GetComponent<PlayerTransformationController>();
        playerCollisionController = playerTransform.GetComponent<PlayerCollisionController>();
        rb = playerTransform.GetComponent<Rigidbody2D>();
        testTileManager= GameManager.Instance.tileManager;
    }

    public override void ActivateAbility(Transform playerTransform)
    {
        Initialize(playerTransform);


        if (GameManager.Instance == null)
        {
            Debug.Log("GameManager NULL");
            return;
        }

        List<Tile> monsterTiles = testTileManager.GetMonsterTiles();


        Tile targetTile = FindClosestMonsterTile(playerTransform.position, monsterTiles);

        if (targetTile == null)
        {
            return;
        }

        Monster targetMonster = targetTile.GetFirstMonster();

        if (targetMonster == null)
        {
            Debug.Log("타일에 몬스터가 존재하지 않음");
            return;
        }



        if (playerAnimationController != null)
        {

            // 사라지는 애니메이션이 끝나면 이동 및 암살 애니메이션 실행
            playerTransformationController.StartCoroutine(ExecuteAttackAfterDisappear(
                playerTransform, targetTile, targetMonster));
        }
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

        
        if (targetMonster != null)
        {

            targetMonster.TakeDamage((int)effectValue);

            if (targetMonster.health <= 0)
            {
                Debug.Log("몬스터 처치 완료");
            }
        }

        playerTransformationController.GetCurrentState().Deactivate();

    }

    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;


        // Y축을 약간 낮춰 이펙트가 플레이어보다 아래에서 생성되도록 설정
        Vector3 effectPosition = position + new Vector3(0, -1.5f, 0);

        Instantiate(ninjaAttackEffect, effectPosition, Quaternion.identity);

    }


    // 가장 가까운 적을 찾는 메서드
    private Tile FindClosestMonsterTile(Vector2 playerPosition, List<Tile> monsterTiles)
    {
        Tile closestTile = null;
        float minDistance = Mathf.Infinity;

        // 삭제된 타일을 제거하고 유효한 타일만 남기기
        monsterTiles.RemoveAll(tile => tile == null || tile.gameObject == null);

        foreach (Tile tile in monsterTiles) // 이미 삭제된 타일을 참조할 가능성이 있음
        {
            // 삭제된 타일 체크
            if (tile == null || tile.gameObject == null)
            {
                continue;
            }

            // 플레이어보다 아래쪽에 있는 몬스터는 무시
            if (tile.transform.position.y < playerPosition.y)
            {
                continue; // 아래에 있는 몬스터는 건너뜀
            }


            float distance = Vector2.Distance(playerPosition, tile.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestTile = tile;
            }
        }

        return closestTile;
    }
}
