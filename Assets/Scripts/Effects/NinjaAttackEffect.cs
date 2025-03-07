using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAttackEffect : MonoBehaviour
{
    public int damage = 999; 
    public float lifetime = 0.35f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"일반 공격 충돌 감지.");

        if (collision.CompareTag("Monster")) // 몬스터 태그를 가진 오브젝트와 충돌하면
        {
            Monster monster = collision.GetComponent<Monster>();

            if (monster != null)
            {
                Debug.Log($"몬스터에게 {damage} 적용");
                monster.TakeDamage(damage);
            }
        }
    }
}
