using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAttackEffect : MonoBehaviour
{
    public int damage = 999; // �⺻ ���ݷ�
    public float lifetime = 0.35f; // Ÿ�� ����Ʈ ���� �ð�

    private void Start()
    {
        Destroy(gameObject, lifetime); // ���� �ð� �� �ڵ� ����
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
