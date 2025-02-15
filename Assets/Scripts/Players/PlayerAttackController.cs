using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{
    public Animator animator; // 공격 애니메이터
    public Button attackButton; // 공격 UI 버튼

    private bool isAttacking = false; // 공격 중인지 체크

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

        // 공격 중일 때는 추가 공격을 막음
        if (isAttacking)
        {
            Debug.Log("is Attacking");
            return;
        }


        Tile tile = testTileManager.GetTile(currentFloor);

        if (tile == null)
        {
            Debug.Log("타일 null");
            return;
        }

        bool isLeft = tile.TileOnLeft(transform);

        if (isLeft)
        {
            playerAnimationController.SetAttacking(isLeft);
        }
        

        // 공격 중 상태 설정
        isAttacking = true;
        Debug.Log("Attack started");
        StartCoroutine(ResetAttackFlag());

    }



    //void Jump(bool isleft)
    //{
    //    if (isGameOver) return;

    //    Debug.Log("현재 층" + currentFloor);

    //    Tile tile = testTileManager.GetTile(currentFloor);

    //    if (tile == null)
    //    {
    //        Debug.Log("타일 null");
    //        return;
    //    }

    //    bool isLeft = tile.TileOnLeft(transform);

    //    Jumping(isLeft ? -1 : 1);

    //}


    // 공격 중 상태를 리셋하는 코루틴
    IEnumerator ResetAttackFlag()
    {
        // 현재 애니메이션 길이 만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false; // 공격 가능 상태로 복구
        Debug.Log("Attack ended. Ready for next Attack");
    }
} 
