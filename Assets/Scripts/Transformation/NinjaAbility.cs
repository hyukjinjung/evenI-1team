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
    //public GameObject hitEffectPrefab; // 타격 이펙트 프리팹


    public override void ActivateAbility(Transform playerTransform)
    {
        if (GameManager.Instance == null || GameManager.Instance.tileManager == null)
            return;
        Debug.Log("GameManager, TileManager NULL");

        TestTileManager tileManager = GameManager.Instance.tileManager;
        List<Tile> monsterTiles = tileManager.GetMonsterTiles();


        // 가장 가까운 몬스터 타일 찾기
        Tile targetTile = FindClosestMonsterTile(playerTransform.position, monsterTiles);

        if (targetTile == null)
        {
            Debug.Log("가까운 몬스터 타일 없음");
            return;
        }

        Debug.Log($"가장 가까운 몬스터 타일 위치: {targetTile.transform.position}");

        Monster targetMonster = targetTile.GetFirstMonster();

        if (targetMonster == null)
        {
            Debug.Log("타일에 몬스터가 존재하지 않음");
            return;
        }

        Debug.Log("몬스터 확인 완료, 사라지는 애니메이션 시작");

        PlayerAnimationController playerAnimationController =
            playerTransform.GetComponent<PlayerAnimationController>();

        if (playerAnimationController != null)
        {
            // NinjaFrog가 사라지는 애니메이션 실행
            playerAnimationController.PlayDisappearAnimation();

            // 사라지는 애니메이션이 끝나면 이동 및 암살 애니메이션 실행
            playerTransform.GetComponent<MonoBehaviour>().StartCoroutine(ExecuteAttackAfterDisappear(
                playerTransform, targetTile, targetMonster));
        }
    }

    private IEnumerator ExecuteAttackAfterDisappear(Transform playertransform, Tile targetTile,
        Monster targetMonster)
    {
        PlayerAnimationController playerAnimationController = playertransform.GetComponent<PlayerAnimationController>();
        PlayerCollisionController playerCollisionController = playertransform.GetComponent<PlayerCollisionController>();
        Rigidbody2D rb = playertransform.GetComponent<Rigidbody2D>();
        Collider2D playerCollider = playertransform.GetComponent<Collider2D>();
        Collider2D monsterCollider = targetMonster.GetComponent<Collider2D>();


        // 사라지는 애니메이션 길이
        float disappearTime = playerAnimationController.GetDisappearAnimationLength();

        // 사라지는 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(disappearTime);
        
        // 사라지는 애니메이션이 끝났으므로 즉시 암살 애니메이션으로 전환
        playerAnimationController.PlayDisappearAnimation();

        // 몬스터와 충돌을 순간적으로 무시하여 충돌 문제 방지
        if (playerCollider != null && monsterCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, monsterCollider, true);
        }

        // 플레이어를 몬스터 타일보다 살짝 위로 이동 (충돌 방지)
        Vector3 newPosition = targetTile.transform.position + new Vector3(0, 0.7f, 0);
        playertransform.position = newPosition;

        //// 충돌을 즉시 활성화하여 타일에서 떨어지는 문제 방지
        //playerCollisionController.EnableCollisionImmediately();
        
        // 중력 영향을 제거하여 순간 이동 시 낙하 방지
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0; // 순간 이동 중 중력 제거
        }
    
        // 이동 후 암살 애니메이션 실행
        playerAnimationController.PlayAssassinationAnimation();

        // 암살 애니메이션이 끝날 때까지 대기
        float assassinationTime = playerAnimationController.GetAssassinationAnimationLength();
        yield return new WaitForSeconds(assassinationTime);

        // 몬스터 처치
        targetMonster.TakeDamage(targetMonster.health);
        Debug.Log("몬스터 처치 완료");
        
        // 몬스터와의 충돌을 다시 활성화

        // 중력 다시 활성화
        if (rb != null)
        {
            rb.gravityScale = 3f; // 원래 중력으로 복구
        }
    }

    //플레이어를 몬스터치 위치로 순간 이동

    // 타격 이펙트
    //if (hitEffectPrefab != null)
    //{
    //    Instantiate(hitEffectPrefab, targetTile.transform.position, Quaternion.identity);
    //}





    // 가장 가까운 적을 찾는 메서드
    private Tile FindClosestMonsterTile(Vector2 playerPosition, List<Tile> monsterTiles)
    {
        Tile closestTile = null;
        float minDistance = Mathf.Infinity;

        foreach (Tile tile in monsterTiles)
        {
            float distance = Vector2.Distance(playerPosition, tile.transform.position);
            Debug.Log($"타일 위치: {tile.transform.position}");

            if (distance < minDistance)
            {
                minDistance = distance;
                closestTile = tile;
            }
        }

        if (closestTile != null)
            Debug.Log($"가장 가까운 몬스터 타일: {closestTile.transform.position}");

        return closestTile;
    }
}
