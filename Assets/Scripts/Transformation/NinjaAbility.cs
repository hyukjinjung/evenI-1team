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


    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private PlayerCollisionController playerCollisionController;
    private Rigidbody2D rb;
    private TestTileManager testTileManager;


    private void Initialize(Transform playerTransform)
    {
        playerAnimationController = playerTransform.GetComponent<PlayerAnimationController>();
        playerTransformationController = playerTransform.GetComponent<PlayerTransformationController>();
        playerCollisionController = playerTransform.GetComponent<PlayerCollisionController>();
        rb = playerTransform.GetComponent<Rigidbody2D>();
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
        Debug.Log("���� �浹 ���� Ȱ��ȭ");


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

        playerAnimationController.PlayDisappearAnimation();
        playerTransformationController.StartCoroutine(ExecuteAttackAfterDisappear(
            playerTransform, targetTile, targetMonster));

    }

    private IEnumerator ExecuteAttackAfterDisappear(Transform playertransform, Tile targetTile,
        Monster targetMonster)
    {

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

        Debug.Log("�ϻ� �ִϸ��̼� ����.");

        playerAnimationController.StartRevertAnimation();

    }



    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;



        Vector3 effectPosition = position + new Vector3(0, -1.5f, 0);

        Instantiate(ninjaAttackEffect, effectPosition, Quaternion.identity);

    }



    private Tile FindClosestMonsterTile(Vector2 playerPosition, List<Tile> monsterTiles)
    {
        Tile closestTile = null;
        float minDistance = Mathf.Infinity;


        monsterTiles.RemoveAll(tile => tile == null || tile.gameObject == null);

        foreach (Tile tile in monsterTiles)
        {

            if (tile == null || tile.gameObject == null)
            {
                continue;
            }


            if (tile.transform.position.y < playerPosition.y)
            {
                continue;
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
