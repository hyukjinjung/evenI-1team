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

    public CameraController cameraController;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Debug.Log("ChasingMonsterManager 积己凳.");
    }



    public void Initialize(Transform playerTransform, CameraController cameraController)
    {
        player = playerTransform;
        this.cameraController = cameraController;
        SpawnMonster();
    }



    public void SpawnMonster()
    {
        if (monsterPrefab == null || player == null)
            return;


        if (currentMonster != null)
            return;


        GameObject monsterObj = Instantiate(monsterPrefab);
        Vector3 spawnPosition = player.position - new Vector3(0, followDistance, 0);
        monsterObj.transform.position = spawnPosition;

        currentMonster = monsterObj.GetComponent<ChasingMonster>();
        if (currentMonster != null)
        {
            Debug.Log($"阁胶磐啊 积己凳. {spawnPosition}");
            currentMonster.Initialize(player);
        }

        if (cameraController != null)
        {
            cameraController.chasingMonster = monsterObj.transform;
        }
    }
}
