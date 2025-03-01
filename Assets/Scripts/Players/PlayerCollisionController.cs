using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider2D playerCollider;
    private bool isCollisionDiabled = false;
    private bool canIgnoreMonster = false;


    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        
    }


    public void EnableMonsterIgnore(float duration)
    {
        canIgnoreMonster = true;
        Debug.Log("몬스터와 충돌 무시 활성화");
        StartCoroutine(DisableMonsterIgnoreAfterDelay(duration));
    }


    private IEnumerator DisableMonsterIgnoreAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        canIgnoreMonster = false;
        Debug.Log("몬스터 충돌 비활성화");

    }

    
    public bool CanIgnoreMonster()
    {
        return canIgnoreMonster;
    }
}
