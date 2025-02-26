using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public struct TileGenerationParam
//{
//    // 0 ~ 100 사이의 확률값
//    public int probability;

//    // 기본 방향: 1은 오른쪽, -1은 왼쪽
//    public int defaultDirection;
//}

public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;      // 테스트 타일 프리팹 추가
    [SerializeField] private GameObject ReverseControlPrefab; // 방향키 반전 오브젝트 프리팹 추가
    [SerializeField] private GameObject TransparentPrefab; // 투명 발판 오브젝트 프리팹 추가
    [SerializeField] private GameObject StickyPrefab; // 끈끈이 발판 오브젝트 프리팹 추가
    [SerializeField] private GameObject HideNextPrefab; // 다음 발판 숨김 오브젝트 프리팹 추가
    [SerializeField] private GameObject MonsterTilePrefab; // 몬스터 타일 프리팹 추가
    [SerializeField] private GameObject MonsterPrefab; // 몬스터 프리팹 추가
    [SerializeField] private GameObject ItemTilePrefab; // 아이템 타일 프리팹 추가
    [SerializeField] private GameObject ItemPrefab; // 아이템 프리팹 추가

    // 타일 간의 간격
    private float xOffset = 1;
    private float yOffset = 1;

    private int direction = 1;

    // 타일들을 저장하는 리스트
    private List<Tile> tiles = new List<Tile>();
    private List<Tile> monsterTiles = new List<Tile>();
    private List<Tile> itemTiles = new List<Tile>();

    // x 좌표의 현재 값
    private int currentX = 0;

    // y 좌표의 현재 값
    private int currentY = 0;

    // 타일 생성 간격
    [SerializeField] private float spawnInterval = 0.5f;

    // 방해 오브젝트 생성 확률 (0 ~ 1)
    [SerializeField] private float obstacleSpawnChance = 0.2f;

    // 몬스터 타일 생성 확률 (0 ~ 1)
    [SerializeField] private float monsterTileSpawnChance = 0.18f;

    // 아이템 타일 생성 확률 (0 ~ 1)
    [SerializeField] private float itemTileSpawnChance = 0.15f;

    // 최대 타일 수
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

        if (tilePrefab == testTilePrefab || tilePrefab == MonsterPrefab)
        {
            if (Random.value < obstacleSpawnChance)
            {
                CreateObstacleOnTile(tileComponent);
            }
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

        // 장애물 타입을 랜덤 지정
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
            // 장애물 생성 후 타일의 자식으로 설정
            GameObject obstacle = Instantiate(obstaclePrefab, tile.transform);

            // 장애물을 타일의 중앙, 약간 위쪽에 배치하고 Z 좌표를 조정하여 앞쪽에 위치시킴
            obstacle.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // 타일보다 살짝 위에 위치하고 앞쪽으로 이동
            obstacle.gameObject.SetActive(true);
            tile.SetObstacle(obstacle);
        }
    }

    private void CreateMonsterOnTile(Tile tile)
    {
        if (tile == null)
        {
            Debug.Log("몬스터 타일 NULL");
            return;
        }

        // 몬스터 생성 후 타일의 자식으로 설정
        GameObject monster = Instantiate(MonsterPrefab, tile.transform);

        // 몬스터를 타일의 중앙, 약간 위쪽에 배치하고 Z 좌표를 조정하여 앞쪽에 위치시킴
        monster.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // 타일보다 살짝 위에 위치하고 앞쪽으로 이동
        monster.gameObject.SetActive(true);

        Monster monsterComponent = monster.GetComponent<Monster>();
        if (monsterComponent == null)
            return;

        tile.SetMonster(monsterComponent);
        Debug.Log($"몬스터 생성 완료. 위치: {monster.transform.position}");
    }

    public List<Tile> GetMonsterTiles()
    {
        Debug.Log($"몬스터 타일 개수: {monsterTiles.Count}");
        return monsterTiles;
    }

    public Tile GetTile(int currentFloor)
    {
        if (currentFloor < 0 || currentFloor >= tiles.Count - 1) // 범위 확인
            return null;

        return tiles[currentFloor + 1];
    }

    private void CreateItemOnTile(Tile tile)
    {
        if (tile == null) return;

        // 아이템 생성 후 타일의 자식으로 설정
        GameObject item = Instantiate(ItemPrefab, tile.transform);

        // 아이템을 타일의 중앙, 약간 위쪽에 배치하고 Z 좌표를 조정하여 앞쪽에 위치시킴
        item.transform.localPosition = new Vector3(0, 0.1f, -0.2f); // 타일보다 살짝 위에 위치하고 앞쪽으로 이동
        item.gameObject.SetActive(true);
        tile.SetItem(item);
    }
}
