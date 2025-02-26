using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public struct TileGenerationParam
//{
//    // 0 ~ 100 ������ Ȯ����
//    public int probability;

//    // �⺻ ����: 1�� ������, -1�� ����
//    public int defaultDirection;
//}

public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;      // �׽�Ʈ Ÿ�� ������ �߰�
    [SerializeField] private GameObject ReverseControlPrefab; // ����Ű ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject TransparentPrefab; // ���� ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject StickyPrefab; // ������ ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject HideNextPrefab; // ���� ���� ���� ������Ʈ ������ �߰�
    [SerializeField] private GameObject MonsterTilePrefab; // ���� Ÿ�� ������ �߰�
    [SerializeField] private GameObject MonsterPrefab; // ���� ������ �߰�
    [SerializeField] private GameObject ItemTilePrefab; // ������ Ÿ�� ������ �߰�
    [SerializeField] private GameObject ItemPrefab; // ������ ������ �߰�

    // Ÿ�� ���� ����
    private float xOffset = 1;
    private float yOffset = 1;

    private int direction = 1;

    // Ÿ�ϵ��� �����ϴ� ����Ʈ
    private List<Tile> tiles = new List<Tile>();
    private List<Tile> monsterTiles = new List<Tile>();
    private List<Tile> itemTiles = new List<Tile>();

    // x ��ǥ�� ���� ��
    private int currentX = 0;

    // y ��ǥ�� ���� ��
    private int currentY = 0;

    // Ÿ�� ���� ����
    [SerializeField] private float spawnInterval = 0.5f;

    // ���� ������Ʈ ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float obstacleSpawnChance = 0.2f;

    // ���� Ÿ�� ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float monsterTileSpawnChance = 0.18f;

    // ������ Ÿ�� ���� Ȯ�� (0 ~ 1)
    [SerializeField] private float itemTileSpawnChance = 0.15f;

    // �ִ� Ÿ�� ��
    [SerializeField] private int maxTiles = 20;

    void Start()
    {
        StartCoroutine(GenerateTiles());
    }

    private IEnumerator GenerateTiles()
    {
        while (true)
        {
            GenerateTile();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void GenerateTile()
    {
        GameObject tilePrefab;
        float randomValue = Random.value;

        if (randomValue < monsterTileSpawnChance)
        {
            tilePrefab = MonsterTilePrefab;
        }
        else if (randomValue < monsterTileSpawnChance + itemTileSpawnChance)
        {
            tilePrefab = ItemTilePrefab;
        }
        else
        {
            tilePrefab = testTilePrefab;
        }

        GameObject tileObject = Instantiate(tilePrefab, transform);
        Tile tileComponent = tileObject.GetComponent<Tile>();

        tileObject.transform.localPosition = new Vector3(currentX, currentY, 0);
        tileObject.gameObject.SetActive(true);
        tiles.Add(tileComponent);

        if (tilePrefab == MonsterTilePrefab)
        {
            monsterTiles.Add(tileComponent);
            CreateMonsterOnTile(tileComponent);
        }
        else if (tilePrefab == ItemTilePrefab)
        {
            itemTiles.Add(tileComponent);
            CreateItemOnTile(tileComponent);
        }

        //if (tilePrefab == MonsterPrefab)
        //{
        //    if (Random.value < obstacleSpawnChance)
        //    {
        //        CreateObstacleOnTile(tileComponent);
        //    }
        //}

        if (tilePrefab == MonsterTilePrefab)
        {
            monsterTiles.Add(tileComponent);
            CreateMonsterOnTile(tileComponent);
        }
        else if (Random.value < obstacleSpawnChance)
        {
            CreateObstacleOnTile(tileComponent);
        }

        if (tiles.Count > maxTiles)
        {
            Tile oldestTile = tiles[0];
            tiles.RemoveAt(0);
            if (monsterTiles.Contains(oldestTile))
            {
                monsterTiles.Remove(oldestTile);
            }
            if (itemTiles.Contains(oldestTile))
            {
                itemTiles.Remove(oldestTile);
            }
            Destroy(oldestTile.gameObject);
        }

        UpdateTilePosition();
    }


    private void UpdateTilePosition()
    {
        int randomDirection = Random.Range(0, 2) * 2 - 1;
        currentX += randomDirection;
        currentY += 1;
    }

    private void CreateObstacleOnTile(Tile tile)
    {
        if (tile == null) return;

        // ��ֹ� Ÿ���� ���� ����
        GameObject obstaclePrefab = null;
        int obstacleType = Random.Range(0, 4);

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
            tile.SetObstacle(obstacle);
        }
    }

    private void CreateMonsterOnTile(Tile tile)
    {
        if (tile == null)
        {
            return;
        }

        // ���� ���� �� Ÿ���� �ڽ����� ����
        GameObject monster = Instantiate(MonsterPrefab, tile.transform);

        // ���͸� Ÿ���� �߾�, �ణ ���ʿ� ��ġ�ϰ� Z ��ǥ�� �����Ͽ� ���ʿ� ��ġ��Ŵ
        monster.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // Ÿ�Ϻ��� ��¦ ���� ��ġ�ϰ� �������� �̵�
        monster.gameObject.SetActive(true);

        Monster monsterComponent = monster.GetComponent<Monster>();
        if (monsterComponent == null)
            return;

        tile.SetMonster(monsterComponent);
    }

    public List<Tile> GetMonsterTiles()
    {
        return monsterTiles;
    }

    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // ���� Ȯ��
            return null;

        return tiles[currentFloor + 1];
    }

    private void CreateItemOnTile(Tile tile)
    {
        if (tile == null) return;

        // ������ ���� �� Ÿ���� �ڽ����� ����
        GameObject item = Instantiate(ItemPrefab, tile.transform);

        // �������� Ÿ���� �߾�, �ణ ���ʿ� ��ġ�ϰ� Z ��ǥ�� �����Ͽ� ���ʿ� ��ġ��Ŵ
        item.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // Ÿ�Ϻ��� ��¦ ���� ��ġ�ϰ� �������� �̵�
        item.gameObject.SetActive(true);
        tile.SetItem(item);
    }
}
