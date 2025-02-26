using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
클래스 설명:
변신 캐릭터 특수 기술 Test 용

*/

public class Monster : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(int damage)
    {
        Debug.Log($"몬스터가 {damage} 피해를 입음. 현재 체력: {health} - {damage}");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"몬스터 {gameObject.name} 삭제");
        Destroy(gameObject);
    }
}
