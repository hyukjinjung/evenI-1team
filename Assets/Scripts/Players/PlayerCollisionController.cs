using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool isCollisionDiabled = false;
    private bool canIgnoreMonster = false;

    private PlayerTransformationController playerTransformationController;


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerTransformationController = GetComponent<PlayerTransformationController>();


    }


    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;
        Debug.Log($"���Ϳ� �浹 ���� Ȱ��ȭ. ���� �ð� {duration}");

        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("���� �浹 ��Ȱ��ȭ");

    }

    
    public bool CanIgnoreMonster()
    {
        if (playerTransformationController.GetCurrentTransformation() ==
            TransformationType.NinjaFrog)
        {
            return true;
        }

        return canIgnoreMonster;
    }
}
