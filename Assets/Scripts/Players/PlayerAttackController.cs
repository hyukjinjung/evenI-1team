using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{

    private bool isAttacking = false; // 공격 중인지 체크

    private PlayerAnimationController playerAnimationController;
    private PlayerInputController playerInputController;
    [SerializeField] TestTileManager testTileManager;


    [SerializeField] private int currentFloor = 0;





    private void Start()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerInputController = GetComponent<PlayerInputController>();
        
        // 중복 실행 방지
        playerInputController.OnAttackEvent -= PerformAttack;
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

        // 공격 애니메이션 트리거 실행
        playerAnimationController.SetAttacking(isLeft);

        // 공격 중 상태 설정
        isAttacking = true;
        Debug.Log("Attack started");

        StartCoroutine(ResetAttackFlag());

    }



    // 공격 중 상태를 리셋하는 코루틴
    IEnumerator ResetAttackFlag()
    {
        // 애니메이션 길이를 가져옴
        float attackAnimationLength = playerAnimationController.GetAttackAniamtionLength();

        yield return new WaitForSeconds(attackAnimationLength);

        isAttacking = false; // 공격 가능 상태로 복구
        Debug.Log("다음 공격 준비");
    }
} 
