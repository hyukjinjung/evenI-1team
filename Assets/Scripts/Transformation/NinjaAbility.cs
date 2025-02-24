using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Ŭ���� ����:
�� ���� ���¿� ���� Ư���ɷ��� 
SpecialAbilityData�� ����Ͽ� ����
*/

[CreateAssetMenu(fileName = "NewNinjaAbility", menuName = "SpecialAbilities/Ninja")]

public class NinjaAbility : SpecialAbilityData
{
    public GameObject hitEffectPrefab; // Ÿ�� ����Ʈ ������


    public override void ActivateAbility(Transform playerTransform)
    {
        if (GameManager.Instance == null || GameManager.Instance.tileManager == null)
            return;

        TestTileManager tileManager = GameManager.Instance.tileManager;
        if (tileManager == null)
            return;

        // ���� ����� ���� Ÿ�� ã��
        Tile targetTile = FindClosestMonsterTile(playerTransform.position, tileManager.GetMonsterTiles());


        if (targetTile == null)
        {
            Debug.Log("����� ���� ã�� �� ����");
            return;
        }

        Debug.Log($"���� ����� ���� Ÿ�� ��ġ: {targetTile.transform.position}");

        if (targetTile != null)
        {
            Debug.Log("���� �߰�");
            Monster targetMonster = targetTile.GetFirstMonster();

            if (targetMonster != null)
            {
                //�÷��̾ ����ġ ��ġ�� ���� �̵�
                playerTransform.position = targetTile.transform.position;

                // Ÿ�� ����Ʈ

                // ���� óġ
                targetMonster.TakeDamage(targetMonster.health);
                Debug.Log("���� óġ �Ϸ�");
            }
            else
            {
                Debug.Log("Ÿ�Ͽ� ���Ͱ� ����");
            }
        }
    }



    // ���� ����� ���� ã�� �޼���
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
