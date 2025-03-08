using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileManager : MonoBehaviour
{
    [SerializeField] private GameObject testTilePrefab;
    [SerializeField] private GameObject ReverseControlPrefab;
    [SerializeField] private GameObject InvisibleTilePrefab;
    [SerializeField] private GameObject StickyPrefab;
    [SerializeField] private GameObject HideNextPrefab;
    [SerializeField] private GameObject MonsterTilePrefab;
    [SerializeField] private GameObject MonsterPrefab;
    [SerializeField] private GameObject ItemTilePrefab;
    [SerializeField] private GameObject ItemPrefab;

    private List<Tile> tiles = new List<Tile>();
    // private List<Tile> monsterTiles = new List<Tile>();
    // private List<Tile> itemTiles = new List<Tile>();
    // private List<Tile> transparentTiles = new List<Tile>();

    private int currentX = 0;
    private int currentY = 0;
    private int direction = 1;

    // [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float obstacleSpawnChance = 0.2f;
    [SerializeField] private float monsterTileSpawnChance = 0.18f;
    [SerializeField] private float itemTileSpawnChance = 0.15f;
    [SerializeField] private float InvisibleTileSpawnChance = 0.15f;

    [SerializeField] private int startTileCount = 20;
    [SerializeField] private int maxTiles = 20;

    PlayerInputController playerInputController;
    private GameManager gameManager;
    private int createTileIndex = 0;

    private void GenerateDefaultTile()
    {
        // 첫 번째 타일은 기본 타일로 생성
        GenerateTile(testTilePrefab, out Tile tile);
        
        // 두 번째 타일에 무조건 아이템 생성하는 코드 제거
        // 이제 두 번째 타일부터는 Start() 메서드의 for 루프에서 GenerateTile()을 호출하여 랜덤하게 생성됨
    }

    void Start()
    {
        GenerateDefaultTile(); // ✅ 첫 번째 기본 타일 생성
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile(); // 나머지 타일은 랜덤하게 생성
        }

        if (startTileCount > maxTiles)
            maxTiles = startTileCount;

        gameManager = GameManager.Instance;
        gameManager.PlayerInputController.OnJumpEvent += OnJumpEvent;
    }

    void OnJumpEvent(bool isLeft)
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        float randomValue = Random.value;

        GameObject tilePrefab = CheckTilePrefab(randomValue);
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
        AfterCreateTile(tilePrefab, tileComponent);
    }


    private void AfterCreateTile(GameObject tilePrefab, Tile tileComponent)
    {
        if (tilePrefab == MonsterTilePrefab)
        {
            //monsterTiles.Add(tileComponent);
            CreateMonsterOnTile(tileComponent); // ✅ 몬스터 타일에는 몬스터만 생성
        }
        else if (tilePrefab == ItemTilePrefab)
        {
            //itemTiles.Add(tileComponent);
            CreateItemOnTile(tileComponent); // ✅ 아이템 타일에는 아이템만 생성
        }
        else if (tilePrefab == InvisibleTilePrefab)
        {
            //transparentTiles.Add(tileComponent);
            CreateInvisibleTile(tileComponent); // ✅ Invisible 타일에는 Invisible 기능만 추가
        }

        // 일반 타일에는 장애물만 생성하도록 수정
        if (tilePrefab == testTilePrefab && Random.value < obstacleSpawnChance)
        {
            CreateObstacleOnTile(tileComponent);
        }
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
        else if (randomValue < monsterTileSpawnChance + itemTileSpawnChance + InvisibleTileSpawnChance)
        {
            tilePrefab = InvisibleTilePrefab;
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

        tileComponent.Init(createTileIndex);
        createTileIndex++;
        
        tiles.Add(tileComponent);
        UpdateTilePosition();
        return tileObject;
    }

    public Tile GetNextMonsterTile(int currentFloor)
    {
        for (int i = currentFloor; i < tiles.Count; i++)
        {
            if (tiles[i].MonsterOnTile != null)
                return tiles[i];
        }
        
        return null;
    }


    // private void DestroyOldestTile()
    // {
    //     if (tiles.Count == 0) return;
    //
    //     Tile oldestTile = tiles[0];
    //     tiles.RemoveAt(0);
    //     Destroy(oldestTile.gameObject);
    // }

    // 타일 지그재그 생성
    //private void UpdateTilePosition()
    //{
    //    if (currentX <= -2)
    //    {
    //        direction = 1;
    //    }
    //    else if (currentX >= 2)
    //    {
    //        direction = -1;
    //    }
    //    currentX += direction;
    //    currentY += 1;
    //}

    //타일 랜덤생성
    private void UpdateTilePosition()
    {
        int randomDirection = (Random.Range(0, 2) == 0) ? -1 : 1; // -1 또는 1 선택 (왼쪽 or 오른쪽 이동)

        // 새로운 x 좌표 계산
        float newX = currentX + randomDirection;

        // x 좌표가 -8 ~ 8 범위를 넘지 않도록 제한
        if (newX < -8f)
        {
            newX = -8f + 1; // 왼쪽 경계를 넘으면 오른쪽으로 이동
        }
        else if (newX > 8f)
        {
            newX = 8f - 1; // 오른쪽 경계를 넘으면 왼쪽으로 이동
        }

        currentX = Mathf.RoundToInt(newX); // 정수형 좌표 유지
        currentY += 1; // Y 좌표는 항상 증가 (위로 이동)
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

    // public List<Tile> GetMonsterTiles()
    // {
    //     return monsterTiles;
    // }

    public Tile GetNextTile(int currentFloor)
    {
        int nextFloor = currentFloor + 1;

        if (nextFloor < 0 || currentFloor >= tiles.Count)
        {
            return null;
        }

        return tiles[nextFloor];
    }

    private void CreateMonsterOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject monster = Instantiate(MonsterPrefab, tile.transform);
        monster.transform.localPosition = Vector3.zero; // ✅ 타일 중앙에 배치
        monster.gameObject.SetActive(true);

        Monster monsterComponent = monster.GetComponent<Monster>();
        if (monsterComponent != null)
        {
            tile.SetMonster(monsterComponent);
        }
    }


    private void CreateInvisibleTile(Tile tile)
    {
        if (tile == null) return;

        TogglePlatform togglePlatform = tile.gameObject.AddComponent<TogglePlatform>();
        // 개별 간격 설정은 더 이상 필요 없음 (매니저가 모든 발판을 동기화)
        // togglePlatform.SetToggleInterval(3f);
    }



    private void CreateItemOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject item = Instantiate(ItemPrefab, tile.transform);
        item.transform.localPosition = Vector3.zero; // ✅ 타일 중앙에 배치
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
            //case 0:               
            //    obstaclePrefab = ReverseControlPrefab;
            //    break;
            //case 1:
            //    obstaclePrefab = StickyPrefab;
            //    break;
            case 2:
                obstaclePrefab = HideNextPrefab;
                break;
        }

        if (obstaclePrefab != null)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, tile.transform);
            obstacle.transform.localPosition = Vector3.zero; // ✅ 타일 중앙에 배치
            obstacle.gameObject.SetActive(true);
            tile.SetObstacle(obstacle);
        }
    }


 
    public int GetFloorByPosition(Vector3 playerPosition)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (Mathf.Abs(tiles[i].transform.position.y - playerPosition.y) < 0.1f)
            {
                return i;
            }
        }

        return -1;
    }

    // 닌자 효과로 건너뛴 타일 수만큼 새 타일 생성하는 메서드
    public void GenerateTilesAfterNinjaEffect(int skippedTiles)
    {
        // 건너뛴 타일 수만큼 새 타일 생성
        for (int i = 0; i < skippedTiles; i++)
        {
            GenerateTile();
        }
        
        Debug.Log($"닌자 효과: {skippedTiles}개의 새 타일이 생성되었습니다.");
    }
}
