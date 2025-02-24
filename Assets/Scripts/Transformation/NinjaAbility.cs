using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
Ŭ���� ����:
�� ���� ���¿� ���� Ư���ɷ��� 
SpecialAbilityData�� ����Ͽ� ����
*/

[CreateAssetMenu(fileName = "NewNinjaAbility", menuName = "SpecialAbilities/Ninja")]

public class NinjaAbility : SpecialAbilityData
{
    //public GameObject hitEffectPrefab; // Ÿ�� ����Ʈ ������


    public override void ActivateAbility(Transform playerTransform)
    {
        if (GameManager.Instance == null || GameManager.Instance.tileManager == null)
            return;
        Debug.Log("GameManager, TileManager NULL");

        TestTileManager tileManager = GameManager.Instance.tileManager;
        List<Tile> monsterTiles = tileManager.GetMonsterTiles();


        // ���� ����� ���� Ÿ�� ã��
        Tile targetTile = FindClosestMonsterTile(playerTransform.position, monsterTiles);

        if (targetTile == null)
        {
            Debug.Log("����� ���� Ÿ�� ����");
            return;
        }

        Debug.Log($"���� ����� ���� Ÿ�� ��ġ: {targetTile.transform.position}");

        Monster targetMonster = targetTile.GetFirstMonster();

        if (targetMonster == null)
        {
            Debug.Log("Ÿ�Ͽ� ���Ͱ� �������� ����");
            return;
        }

        Debug.Log("���� Ȯ�� �Ϸ�, ������� �ִϸ��̼� ����");

        PlayerAnimationController playerAnimationController =
            playerTransform.GetComponent<PlayerAnimationController>();

        if (playerAnimationController != null)
        {
            // NinjaFrog�� ������� �ִϸ��̼� ����
            playerAnimationController.PlayDisappearAnimation();

            // ������� �ִϸ��̼��� ������ �̵� �� �ϻ� �ִϸ��̼� ����
            playerTransform.GetComponent<MonoBehaviour>().StartCoroutine(ExecuteAttackAfterDisappear(
                playerTransform, targetTile, targetMonster));
        }
    }

    private IEnumerator ExecuteAttackAfterDisappear(Transform playertransform, Tile targetTile,
        Monster targetMonster)
    {
        PlayerAnimationController playerAnimationController = playertransform.GetComponent<PlayerAnimationController>();

        float disappearTime = playerAnimationController.GetDisappearAnimationLength();

            // ������� �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(disappearTime);


        // �÷��̾ ���� Ÿ�� ��ġ�� �̵�
        playertransform.position = targetTile.transform.position;


        // ���� �ִϸ��̼� ����
        playerAnimationController.PlayAssassinationAnimation();
        float NinjaSpecialAttackTime = playerAnimationController.GetAssassinationAnimationLength();
        yield return new WaitForSeconds(NinjaSpecialAttackTime);


        // ���� óġ
        targetMonster.TakeDamage(targetMonster.health);
        Debug.Log("���� óġ �Ϸ�");

    }

    //�÷��̾ ����ġ ��ġ�� ���� �̵�

    // Ÿ�� ����Ʈ
    //if (hitEffectPrefab != null)
    //{
    //    Instantiate(hitEffectPrefab, targetTile.transform.position, Quaternion.identity);
    //}





    // ���� ����� ���� ã�� �޼���
    private Tile FindClosestMonsterTile(Vector2 playerPosition, List<Tile> monsterTiles)
    {
        Tile closestTile = null;
        float minDistance = Mathf.Infinity;

        foreach (Tile tile in monsterTiles)
        {
            float distance = Vector2.Distance(playerPosition, tile.transform.position);
            Debug.Log($"Ÿ�� ��ġ: {tile.transform.position}");

            if (distance < minDistance)
            {
                minDistance = distance;
                closestTile = tile;
            }
        }

        if (closestTile != null)
            Debug.Log($"���� ����� ���� Ÿ��: {closestTile.transform.position}");

        return closestTile;
    }
}
