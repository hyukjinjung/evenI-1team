using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Ŭ���� ����:


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
