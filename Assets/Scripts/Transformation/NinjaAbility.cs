using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

/*
Ŭ���� ����:
�� ���� ���¿� ���� Ư���ɷ��� 
SpecialAbilityData�� ����Ͽ� ����
*/

[CreateAssetMenu(fileName = "NewNinjaAbility", menuName = "SpecialAbilities/Ninja")]

public class NinjaAbility : SpecialAbilityData
{
    public GameObject ninjaAttackEffect;

    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private TestTileManager testTileManager;
    private PlayerMovement playerMovement;



    private void Initialize(Transform playerTransform)
    {
        playerAnimationController = playerTransform.GetComponent<PlayerAnimationController>();
        playerTransformationController = playerTransform.GetComponent<PlayerTransformationController>();
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        
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

        if (playerMovement == null) return;

        playerMovement.EnableMonsterIgnore(transformationData.duration);
        Debug.Log("���� �浹 ���� Ȱ��ȭ");
        
        Tile monsterTile = testTileManager.GetNextMonsterTile(playerMovement.CurrentFloor);
        
        if (monsterTile == null)
        {
            return;
        }


        Monster targetMonster = monsterTile.MonsterOnTile;

        if (targetMonster == null)
        {
            Debug.Log("Ÿ�Ͽ� ���Ͱ� �������� ����");
            return;
        }

        playerTransformationController.StartCoroutine(ExecuteAttackAfterDisappear(
            playerTransform, monsterTile, targetMonster));
    }



    private IEnumerator ExecuteAttackAfterDisappear(Transform playertransform, Tile targetTile,
        Monster targetMonster)
    {
        SoundManager.Instance.PlayClip(24, 0.5f);

        playerAnimationController.PlayDisappearAnimation();
        float disappearTime = playerAnimationController.GetDisappearAnimationLength();
        yield return new WaitForSeconds(disappearTime);

        Vector3 newPosition = targetTile.transform.position + new Vector3(0, 0.8f, 0);
        playertransform.position = newPosition;

        SpawnAttackEffect(newPosition);

        playerAnimationController.PlayAssassinationAnimation();
        float assassinationTime = playerAnimationController.GetAssassinationAnimationLength();
        yield return new WaitForSeconds(assassinationTime);


        int previousFloor = playerTransformationController.GetNinjaPreviousFloor();
        playerMovement.UpdateCurrentFloor();

        int skippedTiles = Mathf.Max(0, playerMovement.CurrentFloor - previousFloor);

        if (skippedTiles > 0)
        {
            GameManager.Instance.AddScore(skippedTiles);
            Debug.Log($"���� �ϻ� ����. ���� +{skippedTiles}");

            for (int i = 0; i < skippedTiles; i++)
            {
                FeverSystem.Instance.AddFeverScore(FeverScoreType.Movement);
            }

            TestTileManager tileManager = FindObjectOfType<TestTileManager>();
            if (tileManager != null)
            {
                tileManager.GenerateTilesAfterNinjaEffect(skippedTiles);
            }
        }
    }



    private void SpawnAttackEffect(Vector3 position)
    {
        if (ninjaAttackEffect == null) return;

        Vector3 effectPosition = position + new Vector3(0, -0.7f, 0);
        Instantiate(ninjaAttackEffect, effectPosition, Quaternion.identity);
    }
}
