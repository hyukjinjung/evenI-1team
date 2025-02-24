using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
클래스 설명:
변신 캐릭터 특수 기술 Test 용

*/

public class Monster : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
