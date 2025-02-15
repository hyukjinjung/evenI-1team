using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{
    public Animator animator; // ���� �ִϸ�����
    public Button attackButton; // ���� UI ��ư

    private bool isAttacking = false; // ���� ������ üũ

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;


    [SerializeField] private int currentFloor = 0;





    private void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();

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

        if (isLeft)
        {
            playerAnimationController.SetAttacking(isLeft);
        }
        

        // ���� �� ���� ����
        isAttacking = true;
        Debug.Log("Attack started");
        StartCoroutine(ResetAttackFlag());

    }



    //void Jump(bool isleft)
    //{
    //    if (isGameOver) return;

    //    Debug.Log("���� ��" + currentFloor);

    //    Tile tile = testTileManager.GetTile(currentFloor);

    //    if (tile == null)
    //    {
    //        Debug.Log("Ÿ�� null");
    //        return;
    //    }

    //    bool isLeft = tile.TileOnLeft(transform);

    //    Jumping(isLeft ? -1 : 1);

    //}


    // ���� �� ���¸� �����ϴ� �ڷ�ƾ
    IEnumerator ResetAttackFlag()
    {
        // ���� �ִϸ��̼� ���� ��ŭ ���
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false; // ���� ���� ���·� ����
        Debug.Log("Attack ended. Ready for next Attack");
    }
} 
