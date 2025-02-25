using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool isCollisionDiabled = false; // 충돌 무효화 (기본 false)
    private bool canIgnoreMonster = false; // 패시브 효과로 몬스터 무시


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        
    }


    // 몬스터와 충돌을 무시하는 패시브 효과 적용
    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;
        Debug.Log("몬스터와 충돌 무시 활성화");
        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    // 일정 시간이 지나면 몬스터 충돌 다시 활성화
    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("몬스터 충돌 비활성화");

    }

    
    // 현재 몬스터 충돌 시 무시 상태인지 확인
    public bool CanIgnoreMonster()
    {
        return canIgnoreMonster;
    }
}
