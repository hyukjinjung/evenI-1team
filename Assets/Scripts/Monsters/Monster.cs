using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Ŭ���� ����:
���� ĳ���� Ư�� ��� Test ��

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
