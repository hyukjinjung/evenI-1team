using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChasingMonsterManager : MonoBehaviour
{
    public static ChasingMonsterManager Instance { get; private set; }

    public GameObject monsterPrefab;
    public Transform player;
    private ChasingMonster currentMonster;

    public float followDistance = 100f;
    


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }



    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
        Debug.Log("플레이어 트랜스폼 초기화 완료");

        SpawnMonster();
    }



    void Update()
    {
     
    }



    public void SpawnMonster()
    {
        if (monsterPrefab == null || player == null) return;

        if (currentMonster != null)
        {
            Debug.Log("현재 몬스터가 이미 존재함");
            return;
        }


        GameObject monsterObj = Instantiate(monsterPrefab);

        Vector3 spawnPosition = player.position - new Vector3(0,followDistance,0);
        monsterObj.transform.position = spawnPosition;

        currentMonster = monsterObj.GetComponent<ChasingMonster>();
        if (currentMonster != null)
        {
            currentMonster.Initialize(player);
        }
    }
}
