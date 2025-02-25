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
        PlayerCollisionController playerCollisionController = playertransform.GetComponent<PlayerCollisionController>();
        Rigidbody2D rb = playertransform.GetComponent<Rigidbody2D>();
        Collider2D playerCollider = playertransform.GetComponent<Collider2D>();
        Collider2D monsterCollider = targetMonster.GetComponent<Collider2D>();


        // ������� �ִϸ��̼� ����
        float disappearTime = playerAnimationController.GetDisappearAnimationLength();

        // ������� �ִϸ��̼� ���̸�ŭ ���
        yield return new WaitForSeconds(disappearTime);
        
        // ������� �ִϸ��̼��� �������Ƿ� ��� �ϻ� �ִϸ��̼����� ��ȯ
        playerAnimationController.PlayDisappearAnimation();

        // ���Ϳ� �浹�� ���������� �����Ͽ� �浹 ���� ����
        if (playerCollider != null && monsterCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, monsterCollider, true);
        }

        // �÷��̾ ���� Ÿ�Ϻ��� ��¦ ���� �̵� (�浹 ����)
        Vector3 newPosition = targetTile.transform.position + new Vector3(0, 0.7f, 0);
        playertransform.position = newPosition;

        //// �浹�� ��� Ȱ��ȭ�Ͽ� Ÿ�Ͽ��� �������� ���� ����
        //playerCollisionController.EnableCollisionImmediately();
        
        // �߷� ������ �����Ͽ� ���� �̵� �� ���� ����
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0; // ���� �̵� �� �߷� ����
        }
    
        // �̵� �� �ϻ� �ִϸ��̼� ����
        playerAnimationController.PlayAssassinationAnimation();

        // �ϻ� �ִϸ��̼��� ���� ������ ���
        float assassinationTime = playerAnimationController.GetAssassinationAnimationLength();
        yield return new WaitForSeconds(assassinationTime);

        // ���� óġ
        targetMonster.TakeDamage(targetMonster.health);
        Debug.Log("���� óġ �Ϸ�");
        
        // ���Ϳ��� �浹�� �ٽ� Ȱ��ȭ

        // �߷� �ٽ� Ȱ��ȭ
        if (rb != null)
        {
            rb.gravityScale = 3f; // ���� �߷����� ����
        }
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
