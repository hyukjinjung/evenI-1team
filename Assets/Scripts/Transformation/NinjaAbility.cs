using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
클래스 설명:
각 변신 형태에 따른 특수능력을 
SpecialAbilityData를 상속하여 구현
*/

[CreateAssetMenu(fileName = "NewNinjaAbility", menuName = "SpecialAbilities/Ninja")]

public class NinjaAbility : SpecialAbilityData
{
    public GameObject hitEffectPrefab; // 타격 이펙트 프리팹


    public override void ActivateAbility(Transform playerTransform)
    {
        if (GameManager.Instance == null || GameManager.Instance.tileManager == null)
            return;

        TestTileManager tileManager = GameManager.Instance.tileManager;
        if (tileManager == null)
            return;

        // 가장 가까운 몬스터 타일 찾기
        Tile targetTile = FindClosestMonsterTile(playerTransform.position, tileManager.GetMonsterTiles());


        if (targetTile == null)
        {
            Debug.Log("가까운 적을 찾을 수 없음");
            return;
        }

        Debug.Log($"가장 가까운 몬스터 타일 위치: {targetTile.transform.position}");

        if (targetTile != null)
        {
            Debug.Log("몬스터 발견");
            Monster targetMonster = targetTile.GetFirstMonster();

            if (targetMonster != null)
            {
                //플레이어를 몬스터치 위치로 순간 이동
                playerTransform.position = targetTile.transform.position;

                // 타격 이펙트

                // 몬스터 처치
                targetMonster.TakeDamage(targetMonster.health);
                Debug.Log("몬스터 처치 완료");
            }
            else
            {
                Debug.Log("타일에 몬스터가 없음");
            }
        }
    }



    // 가장 가까운 적을 찾는 메서드
    private Tile FindClosestMonsterTile(Vector2 playerPosition, List<Tile> monsterTiles)
    {
        Tile closestTile = null;
        float minDistance = Mathf.Infinity;

        foreach (Tile tile in monsterTiles)
        {
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
