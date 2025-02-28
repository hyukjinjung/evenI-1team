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



    public override void ActivateAbility(Transform playerTransform)
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("GameManager NULL");
            return;
        }

        TestTileManager tileManager = GameManager.Instance.tileManager;
        List<Tile> monsterTiles = tileManager.GetMonsterTiles();


        // ���� ����� ���� Ÿ�� ã��
        Tile targetTile = FindClosestMonsterTile(playerTransform.position, monsterTiles);

        if (targetTile == null)
        {
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
        PlayerTransformationController playerTransformationController = playertransform.GetComponent<PlayerTransformationController>();
        Rigidbody2D rb = playertransform.GetComponent<Rigidbody2D>();

        // 1. ������� �ִϸ��̼� ���� (NInjaFrog)
        playerAnimationController.PlayDisappearAnimation();

        // 2. ������� �ִϸ��̼� ���̸�ŭ ���
        float disappearTime = playerAnimationController.GetDisappearAnimationLength();
        yield return new WaitForSeconds(disappearTime);


        // 3. Disappear �ִϸ��̼� Ʈ���� �ʱ�ȭ
        playerAnimationController.ResetTrigger("Disappear");

        // �߷� ������ �����Ͽ� ���� �̵� �� ���� ����
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0; // ���� �̵� �� �߷� ����
        }


        // 4. �÷��̾ ���� Ÿ�Ϸ� ���� �̵�
        Vector3 newPosition = targetTile.transform.position + new Vector3(0, 0.7f, 0);
        playertransform.position = newPosition;

        // NinajaFrog - Assassination ����Ʈ ����
        SpawnAttackEffect(newPosition);

        // 5. �̵� �� ��� �ϻ� �ִϸ��̼� ����
        playerAnimationController.PlayAssassinationAnimation();


        // 6. �ϻ� �ִϸ��̼��� ���� ������ ���
        yield return new WaitForSeconds(playerAnimationController.GetAssassinationAnimationLength());


        // 7. ���� óġ
        if (targetMonster != null)
        {

            targetMonster.TakeDamage((int)effectValue);

            if (targetMonster.health <= 0)
            {
                Debug.Log("���� óġ �Ϸ�");
            }
        }


        // 8. ���� ���� ����
        if (playerCollisionController != null)
        {
            Debug.Log("���� ���� �õ� ��");
            playerTransformationController.GetCurrentState().Deactivate(); // ���� ���� �ִϸ��̼� ����
        }

        if (targetTile != null)
        {
            targetTile.RemoveMonster(); // ���͸� �����ϰ� Ÿ���� ����
        }

        // �߷� �ٽ� Ȱ��ȭ
        if (rb != null)
        {
            rb.gravityScale = 3f; // ���� �߷����� ����
        }
    }

    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;


        // Y���� �ణ ���� ����Ʈ�� �÷��̾�� �Ʒ����� �����ǵ��� ����
        Vector3 effectPosition = position + new Vector3(0, -1f, 0);

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
