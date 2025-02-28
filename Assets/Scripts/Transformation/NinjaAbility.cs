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



    public override void ActivateAbility(Transform playerTransform)
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("GameManager NULL");
            return;
        }

        TestTileManager tileManager = GameManager.Instance.tileManager;
        List<Tile> monsterTiles = tileManager.GetMonsterTiles();


        // 가장 가까운 몬스터 타일 찾기
        Tile targetTile = FindClosestMonsterTile(playerTransform.position, monsterTiles);

        if (targetTile == null)
        {
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
        PlayerTransformationController playerTransformationController = playertransform.GetComponent<PlayerTransformationController>();
        Rigidbody2D rb = playertransform.GetComponent<Rigidbody2D>();

        // 1. 사라지는 애니메이션 실행 (NInjaFrog)
        playerAnimationController.PlayDisappearAnimation();

        // 2. 사라지는 애니메이션 길이만큼 대기
        float disappearTime = playerAnimationController.GetDisappearAnimationLength();
        yield return new WaitForSeconds(disappearTime);


        // 3. Disappear 애니메이션 트리거 초기화
        playerAnimationController.ResetTrigger("Disappear");

        // 중력 영향을 제거하여 순간 이동 시 낙하 방지
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0; // 순간 이동 중 중력 제거
        }


        // 4. 플레이어를 몬스터 타일로 순간 이동
        Vector3 newPosition = targetTile.transform.position + new Vector3(0, 0.7f, 0);
        playertransform.position = newPosition;

        // NinajaFrog - Assassination 이펙트 생성
        SpawnAttackEffect(newPosition);

        // 5. 이동 후 즉시 암살 애니메이션 실행
        playerAnimationController.PlayAssassinationAnimation();


        // 6. 암살 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(playerAnimationController.GetAssassinationAnimationLength());


        // 7. 몬스터 처치
        if (targetMonster != null)
        {

            targetMonster.TakeDamage((int)effectValue);

            if (targetMonster.health <= 0)
            {
                Debug.Log("몬스터 처치 완료");
            }
        }


        // 8. 변신 해제 실행
        if (playerCollisionController != null)
        {
            Debug.Log("변신 해제 시도 중");
            playerTransformationController.GetCurrentState().Deactivate(); // 변신 해제 애니메이션 실행
        }

        if (targetTile != null)
        {
            targetTile.RemoveMonster(); // 몬스터만 삭제하고 타일은 유지
        }

        // 중력 다시 활성화
        if (rb != null)
        {
            rb.gravityScale = 3f; // 원래 중력으로 복구
        }
    }

    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;


        // Y축을 약간 낮춰 이펙트가 플레이어보다 아래에서 생성되도록 설정
        Vector3 effectPosition = position + new Vector3(0, -1f, 0);

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
