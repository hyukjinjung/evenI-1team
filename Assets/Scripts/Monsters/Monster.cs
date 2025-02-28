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

    private bool isDying = false;

    MonsterAnimationController animationController;

    private void Awake()
    {
        animationController = GetComponent<MonsterAnimationController>();

    }

    private void Start()
    {
        
    }


    public void TakeDamage(int damage)
    {
        Debug.Log($"���Ͱ� {damage} ���ظ� ����. ���� ü��: {health} - {damage}");
        health -= damage;

        if (health <= 0)
        {
            isDying = true;
            Die();
        }
    }


    private void Die()
    {
        
        Debug.Log($"���� {gameObject.name} ���");
        animationController.StartDeath(this);
        Destroy(gameObject);
    }

}
