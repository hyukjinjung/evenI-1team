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
        Debug.Log($"�Ϲ� ���� �浹 ����.");

        if (collision.CompareTag("Monster")) // ���� �±׸� ���� ������Ʈ�� �浹�ϸ�
        {
            Monster monster = collision.GetComponent<Monster>();

            if (monster != null)
            {
                Debug.Log($"���Ϳ��� {damage} ����");
                monster.TakeDamage(damage);
            }
        }
    }
}
