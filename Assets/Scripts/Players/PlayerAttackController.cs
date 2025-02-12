using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{
    public Animator animator; // ���� �ִϸ�����
    public Button attackButton; // ���� UI ��ư
    public LayerMask targetLayer; // Ÿ�� �� ���� ���̾�
    public int lastJumpDirection; // ������ ���� ����

    private bool isAttacking = false; // ���� ������ üũ

    private void Start()
    {
        // ���� ��ư Ŭ�� �� PerformAttack ȣ��
        attackButton.onClick.AddListener(PerformAttack);
        Debug.Log("Attack button listener added");
    }


    void PerformAttack()
    {
        // ���� ���� ���� �߰� ������ ����
        if (isAttacking)
        {
            Debug.Log("Attack already in progress");
            return;
        }


        // ���� �� ���� ����
        isAttacking = true;
        Debug.Log("Attack started");

        GameObject higherTile = FindNearestTile();
        if (higherTile != null)
        {
            if (higherTile.transform.position.x < transform.position.x)
            {
                animator.SetTrigger("AttackLeft");
            }
            else
            {
                animator.SetTrigger("AttackRight");
            }
        }

        else
        {
            // �⺻������ ������ ����
            animator.SetTrigger("AttackRight");
            Debug.Log("No tile detected above..");
        }


        // �ִϸ��̼��� ���� ������ ��ٸ���, �ٽ� ���� �����ϵ��� ����
        StartCoroutine(ResetAttackFlag());
    }


    GameObject FindNearestTile()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        GameObject targetTile = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject tile in tiles)
        {
            float tileX = tile.transform.position.x;
            float tileY = tile.transform.position.y;
            float playerX = transform.position.x;
            float playerY = transform.position.y;

            // Ÿ���� �÷��̾�� ���� ��ġ�� �ִ��� Ȯ��
            if (tileY > playerY)
            {
                float distance = Mathf.Abs(tileX - playerX);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetTile = tile;
                   
                }
            }
        }

        return targetTile;
    }



    // ���� �� ���¸� �����ϴ� �ڷ�ƾ
    IEnumerator ResetAttackFlag()
    {
        // ���� �ִϸ��̼� ���� ��ŭ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false; // ���� ���� ���·� ����
        Debug.Log("Attack ended. Ready for next Attack");
    }
}
