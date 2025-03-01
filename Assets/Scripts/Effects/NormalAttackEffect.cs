using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackEffect : MonoBehaviour
{
    public int damage = 1; // �⺻ ���ݷ�
    public float lifetime = 0.2f; // Ÿ�� ����Ʈ ���� �ð�

    private void Start()
    {
        Destroy(gameObject, lifetime); // ���� �ð� �� �ڵ� ����
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Monster")) // ���� �±׸� ���� ������Ʈ�� �浹�ϸ�
        {
            Monster monster = collision.GetComponent<Monster>();

            if(monster != null )
            {
                Debug.Log($"���Ϳ��� {damage} ����");
                monster.TakeDamage(damage);
            }
        }
    }
}
