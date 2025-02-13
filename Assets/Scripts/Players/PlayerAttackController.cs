using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttackController : MonoBehaviour
{
    public Animator animator; // 공격 애니메이터
    public Button attackButton; // 공격 UI 버튼
    public LayerMask targetLayer; // 타일 및 몬스터 레이어
    public int lastJumpDirection; // 마지막 점프 방향

    private bool isAttacking = false; // 공격 중인지 체크

    private void Start()
    {
        // 공격 버튼 클릭 시 PerformAttack 호출
        attackButton.onClick.AddListener(PerformAttack);
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
            // 기본적으로 오른쪽 공격
            animator.SetTrigger("AttackRight");
            Debug.Log("No tile detected above..");
        }


        // 애니메이션이 끝날 때까지 기다리고, 다시 공격 가능하도록 설정
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

            // 타일이 플레이어보다 높은 위치에 있는지 확인
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



    // 공격 중 상태를 리셋하는 코루틴
    IEnumerator ResetAttackFlag()
    {
        // 현재 애니메이션 길이 만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false; // 공격 가능 상태로 복구
        Debug.Log("Attack ended. Ready for next Attack");
    }
}
