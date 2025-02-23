using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TileGenerationParam
{
    // 0 ~ 100 ������ Ȯ����
    public int probability;

    // �⺻ ����: 1�� ������, -1�� ����
    public int defaultDirection;
}


public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;
    [SerializeField] private GameObject obstacleStairPrefab; // ���� ������Ʈ ������ �߰�

    // Ÿ�� ���� ����
    private float xOffset = 1;
    private float yOffset = 1;

    private int direction = 1;

    // Inspector���� �� Ÿ�Ͽ� ���� �Ű������� ������ �� ����
    [SerializeField] private List<TileGenerationParam> tileParams = new List<TileGenerationParam>();

    // Ÿ�ϵ��� �����ϴ� ����Ʈ
    private List<Tile> tiles = new List<Tile>();

    // x ��ǥ�� ���� ��
    private int currentX = 0;

    // Ÿ�� ���� ����
    [SerializeField] private float spawnInterval = 0.5f;

    // ���� ������Ʈ ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float obstacleSpawnChance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateTiles());
    }

    private IEnumerator GenerateTiles()
    {
        while (true)
        {
            GameObject testTile = Instantiate(testTilePrefab);
            testTile.transform.SetParent(transform);

            testTile.transform.localPosition = new Vector3(currentX, yOffset * tiles.Count, 0);
            testTile.gameObject.SetActive(true);

            tiles.Add(testTile.GetComponent<Tile>());

            // ���� Ȯ���� ���� ������Ʈ ����
            if (Random.value < obstacleSpawnChance)
            {
                GameObject obstacle = Instantiate(obstacleStairPrefab);
                obstacle.transform.SetParent(transform);
                obstacle.transform.localPosition = new Vector3(currentX, yOffset * tiles.Count, 0);
                obstacle.gameObject.SetActive(true);
            }

            // Ÿ�� �Ű������� �����Ǿ� �ִٸ� �̸� ����Ͽ� x ��ǥ ����
            if (tiles.Count - 1 < tileParams.Count)
            {
                TileGenerationParam param = tileParams[tiles.Count - 1];

                // 0~100 ������ ���� ���� ����
                int randValue = Random.Range(0, 101);

                int chosenDirection;

                // ���� ���� �Ű������� Ȯ������ ������ �⺻ ����, �׷��� ������ �ݴ� ���� ����
                if (randValue < param.probability)
                    chosenDirection = param.defaultDirection;
                else
                    chosenDirection = -param.defaultDirection;

                // ���õ� ���⿡ ���� x ��ǥ ������Ʈ
                currentX += chosenDirection;
            }
            else
            {
                // �Ű������� ���ٸ� �����ϰ� ���� ����
                int randomDirection = Random.Range(0, 2) * 2 - 1; // -1 �Ǵ� 1
                currentX += randomDirection;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // ���� ���� �ش��ϴ� Ÿ���� ��ȯ
    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // ���� Ȯ��
            return null;

        return tiles[currentFloor + 1];
    }
}
