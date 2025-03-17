using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "NewNinjaAbility", menuName = "SpecialAbilities/Bull")]


public class BullAbility : SpecialAbilityData
{

    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    private TestTileManager testTileManager;
    private PlayerMovement playerMovement;
    private PlayerAttackController playerAttackController;

    private bool hasSwallowed = false;
    private bool canSpitOut = false;
    private GameObject swallowedObject;


    private void Initialize(Transform playerTransform)
    {
        playerAnimationController = playerTransform.GetComponent<PlayerAnimationController>();
        playerTransformationController = playerTransform.GetComponent<PlayerTransformationController>();
        playerMovement = playerTransform.GetComponent<PlayerMovement>();
        playerAttackController = playerTransform.GetComponent<PlayerAttackController>();

        testTileManager = GameManager.Instance.tileManager;
    }



    public override void ActivateAbility(Transform playerTransform, TransformationData transformationData)
    {
        Initialize(playerTransform);

        Tile forwardTile = testTileManager.GetForwardTile(playerTransform.position);

        if (forwardTile != null)
        {
            if (forwardTile.HasMonster()) 
            {
                Monster monster = forwardTile.MonsterOnTile;
                if (monster != null)
                {
                    TrySwallow(monster.gameObject);
                    forwardTile.RemoveMonster();
                    return;
                }
            }

            if (forwardTile.HasObstacle())
            {
                GameObject obstacle = forwardTile.GetObstacle();
                if (obstacle != null)
                {
                    TrySwallow(obstacle.gameObject);
                    forwardTile.RemoveObstacle();
                    return;
                }
            }

            if (forwardTile.HasItem())
            {
                GameObject item = forwardTile.GetItem();
                if (item != null)
                {
                    TrySwallow(item.gameObject);
                    forwardTile.RemoveItem();
                    return;
                }
            }
        }

        if (hasSwallowed)
        {
            GameManager.Instance.PlayerInputController.SetInputActive(true);
        }
    }


    public void TrySwallow(GameObject target)
    {
        if (hasSwallowed) return;

        if (target != null)
        {
            hasSwallowed = true;
            canSpitOut = true;
            swallowedObject = target;
            target.SetActive(false);
            Debug.Log("¸Ô±â ¿Ï·á");

            if (playerAttackController != null)
            {
                playerAttackController.ResetAttackState();
            }

            GameManager.Instance.PlayerInputController.SetInputActive(false);
        }
    }


    public void SpitOut(Transform playerTransform)
    {
        if (!hasSwallowed || swallowedObject == null) return;

        swallowedObject.transform.position = playerTransform.position + Vector3.up * 3;
        swallowedObject.SetActive(true);
        hasSwallowed = false ;
        canSpitOut = false ;
        swallowedObject = null;
        Debug.Log("¹ñ±â ¿Ï·á");

        GameManager.Instance.PlayerInputController.SetInputActive(true);
    }


    public bool CanSpitOut()
    {
        return hasSwallowed;
    }
}
