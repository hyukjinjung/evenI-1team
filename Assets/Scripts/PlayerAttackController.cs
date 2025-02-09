using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{
    public Animator animator; // 공격 애니메이터
    public Button attackButton; // 공격 UI 버튼
    public LayerMask targetLayer; // 타일 및 몬스터 레이어
    public float attackRange = 5f; // 공격 범위

    private bool isAttacking = false; // 공격 중인지 체크

    // 타일 체크를 담당하는 컴포넌트
    private TileChecker tileChecker;



    private void Start()
    {
        // 공격 버튼 클릭 시 PerformAttack 호출
        attackButton.onClick.AddListener(PerformAttack);

        // TileChecker 컴포넌트 가져오기
        tileChecker = GetComponent<TileChecker>();

        Debug.Log("Attack button listener added");
    }

    void PerformAttack()
    {
        // 공격 중일 때는 추가 공격을 막음
        if (isAttacking)
        {
            Debug.Log("Attack already in progress");
            return;
        }


        // 공격 중 상태 설정
        isAttacking = true;
        Debug.Log("Attack started");


        bool attackLeft = tileChecker.IsTileInFrontLeft();

        if (attackLeft)
        {
            animator.SetTrigger("AttackLeft");
            Debug.Log("Attack left animation triggered");
        }

        if (!attackLeft) // 왼쪽에 타일이 없으면 무조건 오른쪽 공격
        {
            animator.SetTrigger("AttackRight");
            Debug.Log("Attack right animation triggered");
        }

        // 애니메이션이 끝날 때까지 기다리고, 다시 공격 가능하도록 설정
        StartCoroutine(ResetAttackFlag());
    }



    // 공격 중 상태를 리셋하는 코루틴
    IEnumerator ResetAttackFlag()
    {
        // 현재 애니메이션 길이 만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);


        // 공격 중 상태를 리셋
        isAttacking = false;
        Debug.Log("Attack ended. Ready for next Attack");
    }
}
