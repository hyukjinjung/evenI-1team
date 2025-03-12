using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
Ŭ���� ����:


*/

public class Monster : MonoBehaviour
{
    public MonsterType monsterType;
    
    [SerializeField] private int health;
    //[SerializeField] private int socre;

    // private bool isDying = false; // 사용되지 않는 필드 제거 또는 주석 처리

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
        Debug.Log($"���Ͱ� {damage} ���ظ� ����. ���� ü��: {health} - {damage}");
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        
        Debug.Log($"���� {gameObject.name} ���");
        //animationController.StartDeath(this);

        GameManager.Instance.AddScore(GetScore());

        Destroy(gameObject);
        SoundManager.Instance.PlayClip(10);
    }

}
