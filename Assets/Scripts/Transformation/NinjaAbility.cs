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
    public GameObject ninjaAttackEffect; // Ÿ�� ����Ʈ ������


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
            Debug.Log("Ÿ�Ͽ� ���Ͱ� �������� ����");
            return;
        }



        if (playerAnimationController != null)
        {

            // ������� �ִϸ��̼��� ������ �̵� �� �ϻ� �ִϸ��̼� ����
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
                Debug.Log("���� óġ �Ϸ�");
            }
        }

        playerTransformationController.GetCurrentState().Deactivate();

    }

    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;


        // Y���� �ణ ���� ����Ʈ�� �÷��̾�� �Ʒ����� �����ǵ��� ����
        Vector3 effectPosition = position + new Vector3(0, -1.5f, 0);

        Instantiate(ninjaAttackEffect, effectPosition, Quaternion.identity);

    }


    // ���� ����� ���� ã�� �޼���
    private Tile FindClosestMonsterTile(Vector2 playerPosition, List<Tile> monsterTiles)
    {
        Tile closestTile = null;
        float minDistance = Mathf.Infinity;

        // ������ Ÿ���� �����ϰ� ��ȿ�� Ÿ�ϸ� �����
        monsterTiles.RemoveAll(tile => tile == null || tile.gameObject == null);

        foreach (Tile tile in monsterTiles) // �̹� ������ Ÿ���� ������ ���ɼ��� ����
        {
            // ������ Ÿ�� üũ
            if (tile == null || tile.gameObject == null)
            {
                continue;
            }

            // �÷��̾�� �Ʒ��ʿ� �ִ� ���ʹ� ����
            if (tile.transform.position.y < playerPosition.y)
            {
                continue; // �Ʒ��� �ִ� ���ʹ� �ǳʶ�
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
