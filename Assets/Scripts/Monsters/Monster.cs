using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
클래스 설명:


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
        Debug.Log($"몬스터가 {damage} 피해를 입음. 현재 체력: {health} - {damage}");
        health -= damage;

        if (health <= 0)
        {
            isDying = true;
            Die();
        }
    }


    private void Die()
    {
        
        Debug.Log($"몬스터 {gameObject.name} 사망");
        animationController.StartDeath(this);
        Destroy(gameObject);
    }

}
