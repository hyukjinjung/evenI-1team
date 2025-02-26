using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Ŭ���� ����:
���� ĳ���� Ư�� ��� Test ��

*/

public class Monster : MonoBehaviour
{
    public int health = 1;

    public void TakeDamage(int damage)
    {
        Debug.Log($"���Ͱ� {damage} ���ظ� ����. ���� ü��: {health} - {damage}");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"���� {gameObject.name} ����");
        Destroy(gameObject);
    }
}
