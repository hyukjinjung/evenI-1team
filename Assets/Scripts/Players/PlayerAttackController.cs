using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{

    private bool isAttacking = false; // ���� ������ üũ

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;


    [SerializeField] private int currentFloor = 0;





    private void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        
        // �ߺ� ���� ����
        playerInputController.OnAttackEvent -= PerformAttack;
        playerInputController.OnAttackEvent += PerformAttack;


    }


    void PerformAttack(bool isleft)
    {
        

        // ���� ���� ���� �߰� ������ ����
        if (isAttacking)
        {
            Debug.Log("is Attacking");
            return;
        }


        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null)
        {
            Debug.Log("Ÿ�� null");
            return;
        }

        bool isLeft = tile.TileOnLeft(transform);

        // ���� �ִϸ��̼� Ʈ���� ����
        playerAnimationController.SetAttacking(isLeft);

        // ���� �� ���� ����
        isAttacking = true;
        Debug.Log("Attack started");

        StartCoroutine(ResetAttackFlag());

    }



    // ���� �� ���¸� �����ϴ� �ڷ�ƾ
    IEnumerator ResetAttackFlag()
    {
        // �ִϸ��̼� ���̸� ������
        float attackAnimationLength = playerAnimationController.GetAttackAniamtionLength();

        yield return new WaitForSeconds(attackAnimationLength);

        isAttacking = false; // ���� ���� ���·� ����
        Debug.Log("���� ���� �غ�");
    }
} 
