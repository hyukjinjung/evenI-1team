using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
클래스 설명:


*/

public class Monster : MonoBehaviour
{
    public MonsterType monsterType;
    
    [SerializeField] private int health;
    //[SerializeField] private int socre;

    private bool isDying = false;

    //MonsterAnimationController animationController;

    private Dictionary<MonsterType, (int health, int score)> monsterData = new Dictionary<MonsterType, (int health, int score)>
    {
        { MonsterType.Fly, (1, 5) },
        { MonsterType.Spider, (2, 10) },
        { MonsterType.Butterfly, (3, 15) },
        { MonsterType.Dragonfly, (4, 20) },
        { MonsterType.Mantis, (5, 25) },
    };


    private void Awake()
    {
        //animationController = GetComponent<MonsterAnimationController>();
    }

    private void Start()
    {
        health = monsterData[monsterType].health;
    }

    public int GetScore()
    {
        return monsterData[monsterType].score;
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
        //animationController.StartDeath(this);

        GameManager.Instance.AddScore(GetScore());

        Destroy(gameObject);
        SoundManager.Instance.PlayClip(10);
    }

}
