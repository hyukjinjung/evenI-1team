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
    [SerializeField] private GameObject testTilePrefab;      // �׽�Ʈ Ÿ�� ������ �߰�
    [SerializeField] private GameObject ReverseControlPrefab; // ����Ű ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject TransparentPrefab; // ���� ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject StickyPrefab; // ������ ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject HideNextPrefab; // ���� ���� ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject MonsterTilePrefab; // ���� Ÿ�� ������ �߰�
    [SerializeField] private GameObject MonsterPrefab; // ���� ������ �߰�

    // Ÿ�� ���� ����
    private float xOffset = 1;
    private float yOffset = 1;

    private int direction = 1;

    // Inspector���� �� Ÿ�Ͽ� ���� �Ű������� ������ �� ����
    [SerializeField] private List<TileGenerationParam> tileParams = new List<TileGenerationParam>();

    // Ÿ�ϵ��� �����ϴ� ����Ʈ
    private List<Tile> tiles = new List<Tile>();

    // ����
    private List<Tile> monsterTiles = new List<Tile>(); // ���Ͱ� �ִ� Ÿ�ϸ� ����

    // x ��ǥ�� ���� ��
    private int currentX = 0;

    // y ��ǥ�� ���� ��
    private int currentY = 0;

    // Ÿ�� ���� ����
    [SerializeField] private float spawnInterval = 0.5f;

    // ���� ������Ʈ ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float obstacleSpawnChance = 0.3f;

    // ���� Ÿ�� ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float monsterTileSpawnChance = 0.25f;

    // �ִ� Ÿ�� ��
    [SerializeField] private int maxTiles = 20;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateTiles());
    }

    private IEnumerator GenerateTiles()
    {
        while (true)
        {
            GameObject tile;
            Tile tileComponent;

            // ���� Ȯ���� ���� Ÿ�� ����
            if (Random.value < monsterTileSpawnChance)
            {
                tile = Instantiate(MonsterTilePrefab, transform);
                tileComponent = tile.GetComponent<Tile>();
                CreateMonsterOnTile(tileComponent);
            }
            else
            {
                // �⺻ Ÿ�� ����
                tile = Instantiate(testTilePrefab, transform);
                tileComponent = tile.GetComponent<Tile>();

                // ���� Ȯ���� ���� ������Ʈ ����
                if (Random.value < obstacleSpawnChance)
                {
                    CreateObstacleOnTile(tileComponent);
                }
            }

            // Ÿ�� ��ġ ����
            tile.transform.localPosition = new Vector3(currentX, currentY, 0);
            tile.gameObject.SetActive(true);

            tiles.Add(tileComponent);

            // �ִ� Ÿ�� ���� �ʰ��ϸ� ���� ������ Ÿ�� ����
            if (tiles.Count > maxTiles)
            {
                Destroy(tiles[0].gameObject);
                tiles.RemoveAt(0);
            }

            // Ÿ�� �Ű������� �����Ǿ� �ִٸ� �̸� ����Ͽ� x ��ǥ ����
            if (tiles.Count < tileParams.Count)
            {
                TileGenerationParam param = tileParams[tiles.Count];

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

            // y ��ǥ ������Ʈ
            currentY += 1;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Ư�� Ÿ�� ���� ��ֹ��� �����ϴ� �Լ�
    private void CreateObstacleOnTile(Tile tile)
    {
        if (tile == null) return;

        // ��ֹ� Ÿ���� ���� ����
        int obstacleType = Random.Range(0, 4);
        GameObject obstaclePrefab = null;

        switch (obstacleType)
        {
            case 0:
                obstaclePrefab = ReverseControlPrefab;
                break;
            case 1:
                obstaclePrefab = TransparentPrefab;
                break;
            case 2:
                obstaclePrefab = StickyPrefab;
                break;
            case 3:
                obstaclePrefab = HideNextPrefab;
                break;
        }

        if (obstaclePrefab != null)
        {
            // ��ֹ� ���� �� Ÿ���� �ڽ����� ����
            GameObject obstacle = Instantiate(obstaclePrefab, tile.transform);

            // ��ֹ��� Ÿ���� �߾�, �ణ ���ʿ� ��ġ�ϰ� Z ��ǥ�� �����Ͽ� ���ʿ� ��ġ��Ŵ
            obstacle.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // Ÿ�Ϻ��� ��¦ ���� ��ġ�ϰ� �������� �̵�
            obstacle.gameObject.SetActive(true);
        }
    }

    // Ư�� Ÿ�� ���� ���͸� �����ϴ� �Լ�
    private void CreateMonsterOnTile(Tile tile)
    {
        if (tile == null) return;

        // ���� ���� �� Ÿ���� �ڽ����� ����
        GameObject monster = Instantiate(MonsterPrefab, tile.transform);

        // ���͸� Ÿ���� �߾�, �ణ ���ʿ� ��ġ�ϰ� Z ��ǥ�� �����Ͽ� ���ʿ� ��ġ��Ŵ
        monster.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // Ÿ�Ϻ��� ��¦ ���� ��ġ�ϰ� �������� �̵�
        monster.gameObject.SetActive(true);
    }

    // ���� ���� �ش��ϴ� Ÿ���� ��ȯ
    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // ���� Ȯ��
            return null;

        return tiles[currentFloor + 1];
    }
}
