using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;
    [SerializeField] private GameObject ReverseControlPrefab;
    [SerializeField] private GameObject TransparentTilePrefab;
    [SerializeField] private GameObject StickyPrefab;
    [SerializeField] private GameObject HideNextPrefab;
    [SerializeField] private GameObject MonsterTilePrefab;
    [SerializeField] private GameObject MonsterPrefab;
    [SerializeField] private GameObject ItemTilePrefab;
    [SerializeField] private GameObject ItemPrefab;

    private List<Tile> tiles = new List<Tile>();
    private List<Tile> monsterTiles = new List<Tile>();
    private List<Tile> itemTiles = new List<Tile>();
    private List<Tile> transparentTiles = new List<Tile>();

    private int currentX = 0;
    private int currentY = 0;
    private int direction = 1;

    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float obstacleSpawnChance = 0.2f;
    [SerializeField] private float monsterTileSpawnChance = 0.18f;
    [SerializeField] private float itemTileSpawnChance = 0.15f;
    [SerializeField] private float transparentTileSpawnChance = 0.15f;
    
    [SerializeField] private int startTileCount = 20;
    [SerializeField] private int maxTiles = 20;
    
    PlayerInputController playerInputController;
    private GameManager gameManager;
    
    
    void Start()
    {
        GenerateDefaultTile();
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile();
        }
        
        if(startTileCount > maxTiles)
            maxTiles = startTileCount;
        
        gameManager = GameManager.Instance;
        gameManager.PlayerInputController.OnJumpEvent += OnJumpEvent;
        
        // StartCoroutine(GenerateTiles());
    }

    // 주기적 생성 -> 점프 때 생성
    // private IEnumerator GenerateTiles()
    // {
    //     while (true)
    //     {
    //         GenerateTile();
    //         yield return new WaitForSeconds(spawnInterval);
    //     }
    // }

    void OnJumpEvent(bool isLeft)
    {
        GenerateTile();
    }

    void GenerateDefaultTile()
    {
        // 플레이어가 위치하는 첫 번째 타일은 무조건 기본 타일 생성
        GameObject tileObject = GenerateTile(testTilePrefab, out Tile tileComponent);
    }
    
    private void GenerateTile()
    {
        float randomValue = Random.value;
        
        GameObject tilePrefab = CheckTilePrefab(randomValue);

        // 타일 생성
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);

        // 타일 유형별 추가 설정
        // TODO:: 타일이 타입을 알고 있게 하기
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
        else if (tilePrefab == TransparentTilePrefab)
        {
            TogglePlatform togglePlatform = tileObject.AddComponent<TogglePlatform>();
            togglePlatform.SetToggleInterval(3f);
        }

        if (Random.value < obstacleSpawnChance )
        {
            CreateObstacleOnTile(tileComponent);
        }

        // 삭제 기준을 좀더 명확히 정하고, 화면 밖으로 이탈로 잡을 것인지 타일 수가 몇 이상 됬을때 삭제 할것인지.
        // if (tiles.Count > maxTiles)
        // {
        //     DestroyOldestTile();
        // }

        UpdateTilePosition();
    }

    private GameObject CheckTilePrefab(float randomValue)
    {
        GameObject tilePrefab;
        // 랜덤한 타일 생성 (기존 로직 유지)
        if (randomValue < monsterTileSpawnChance)
        {
            tilePrefab = MonsterTilePrefab;
        }
        else if (randomValue < monsterTileSpawnChance + itemTileSpawnChance)
        {
            tilePrefab = ItemTilePrefab;
        }
        else if (randomValue < monsterTileSpawnChance + itemTileSpawnChance + transparentTileSpawnChance)
        {
            tilePrefab = TransparentTilePrefab;
        }
        else
        {
            tilePrefab = testTilePrefab;
        }

        return tilePrefab;
    }

    private GameObject GenerateTile(GameObject tilePrefab, out Tile tileComponent)
    {
        GameObject tileObject = Instantiate(tilePrefab, transform);
        tileComponent = tileObject.GetComponent<Tile>();

        if (tileComponent == null)
        {
            tileComponent = tileObject.AddComponent<Tile>(); // 없으면 추가
        }

        tileObject.transform.localPosition = new Vector3(currentX, currentY, 0);
        tileObject.gameObject.SetActive(true);
        tiles.Add(tileComponent);
        return tileObject;
    }


    private void DestroyOldestTile()
    {
        if (tiles.Count == 0) return;

        Tile oldestTile = tiles[0];
        tiles.RemoveAt(0);
        Destroy(oldestTile.gameObject);
    }

    private void UpdateTilePosition()
    {
        if (currentX <= -2)
        {
            direction = 1;
        }
        else if (currentX >= 2)
        {
            direction = -1;
        }
        currentX += direction;
        currentY += 1;
    }
    public Tile GetForwardTile(Vector3 playerPosition)
    {
        Tile forwardTile = null;
        float minDistance = Mathf.Infinity;

        foreach (Tile tile in tiles)
        {
            if (tile.transform.position.y > playerPosition.y) // 플레이어보다 위에 있는 타일 찾기
            {
                float distance = Vector3.Distance(playerPosition, tile.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    forwardTile = tile;
                }
            }
        }

        return forwardTile;
    }

    public List<Tile> GetMonsterTiles()
    {
        return monsterTiles;
    }

    public Tile GetTile(int currentFloor)
    {
        int nextFloor = currentFloor + 1;

        if (nextFloor < 0 || currentFloor >= tiles.Count)
        {
            return null; // 유효하지 않은 층이면 null 반환
        }

        return tiles[nextFloor]; // 다음 층의 타일 반환
    }

    private void CreateMonsterOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject monster = Instantiate(MonsterPrefab, tile.transform);
        monster.transform.localPosition = Vector3.zero; // ✅ 타일의 중앙에 배치
        monster.gameObject.SetActive(true);

        Monster monsterComponent = monster.GetComponent<Monster>();
        if (monsterComponent != null)
        {
            tile.SetMonster(monsterComponent);
        }
    }

    private void CreateItemOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject item = Instantiate(ItemPrefab, tile.transform);
        item.transform.localPosition = Vector3.zero; // ✅ 타일의 중앙에 배치
        item.gameObject.SetActive(true);

        tile.SetItem(item);
    }

    private void CreateObstacleOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject obstaclePrefab = null;
        int obstacleType = Random.Range(0, 4);

        switch (obstacleType)
        {
            case 0:
                obstaclePrefab = ReverseControlPrefab;
                break;
            case 1:
                obstaclePrefab = TransparentTilePrefab;
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
            GameObject obstacle = Instantiate(obstaclePrefab, tile.transform);
            obstacle.transform.localPosition = Vector3.zero; // ✅ 타일의 중앙에 배치
            obstacle.gameObject.SetActive(true);
            tile.SetObstacle(obstacle);
        }
    }
}
