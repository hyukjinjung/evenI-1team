using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("계단 설정")]
    public GameObject normalStairPrefab;
    public GameObject obstacleStairPrefab;
    public Transform stairParent;
    public int maxStairs = 20;
    public float spawnTime = 0.5f;
    public float obstacleChance = 0.3f; // 30% 확률로 방해 발판 생성
    private const int MAP_WIDTH = 19;
    private const float STAIR_WIDTH = 0.75f;

    private List<GameObject> stairs = new List<GameObject>();
    private Vector3 oldPosition;
    private bool isGenerating = false;

    void Start()
    {
        Instance = this;
        Init();

        if (!isGenerating)
        {
            StartCoroutine(GenerateStairs());
            isGenerating = true;
        }
    }

    private void Init()
    {
        oldPosition = Vector3.zero;
    }

    private IEnumerator GenerateStairs()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            CreateStair();

            if (stairs.Count > maxStairs)
            {
                Destroy(stairs[0]);
                stairs.RemoveAt(0);
            }
        }
    }

    private void CreateStair()
    {
        if (stairs.Count >= maxStairs) return;

        bool isObstacle = Random.value < obstacleChance;
        GameObject newStair = Instantiate(isObstacle ? obstacleStairPrefab : normalStairPrefab, stairParent);

        float nextX;
        if (stairs.Count == 0)
        {
            nextX = 0f;
        }
        else
        {
            float currentX = oldPosition.x;
            float randomDirection = Random.Range(0, 2) * 2 - 1;
            nextX = currentX + (STAIR_WIDTH * randomDirection);

            float maxX = (MAP_WIDTH - 1) * STAIR_WIDTH / 2;
            if (nextX > maxX) nextX = currentX - STAIR_WIDTH;
            else if (nextX < -maxX) nextX = currentX + STAIR_WIDTH;
        }

        Vector3 newPosition = new Vector3(nextX, oldPosition.y + 1f, 0);
        newStair.transform.position = newPosition;
        oldPosition = newPosition;
        stairs.Add(newStair);

        if (isObstacle)
        {
            ObstacleStair obstacle = newStair.GetComponent<ObstacleStair>();
            if (obstacle != null)
            {
                obstacle.AssignRandomObstacle(); // ⭐ 방해 발판 적용
            }
            else
            {
                Debug.LogError($"{newStair.name} - ObstacleStair 스크립트가 없음!");
            }
        }
    }

}
