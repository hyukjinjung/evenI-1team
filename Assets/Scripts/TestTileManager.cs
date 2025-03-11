using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject shieldItemPrefab;

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

    // 게임 모드 매니저 참조 추가
    private GameModeManager gameModeManager;

    private void GenerateDefaultTile()
    {
        // 첫 번째 타일은 기본 타일로 생성
        GenerateTile(testTilePrefab, out Tile tile);
        
        // 두 번째 타일에 무조건 아이템 생성하는 코드 제거
        // 이제 두 번째 타일부터는 Start() 메서드의 for 루프에서 GenerateTile()을 호출하여 랜덤하게 생성됨
    }

    void Start()
    {
        // 게임 모드 매니저 참조 가져오기
        gameModeManager = GameModeManager.Instance;
        
        GenerateDefaultTile(); // ✅ 첫 번째 기본 타일 생성
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile(); // 나머지 타일은 랜덤하게 생성
        }

        if (startTileCount > maxTiles)
            maxTiles = startTileCount;

        gameManager = GameManager.Instance;
        if (gameManager != null && gameManager.PlayerInputController != null)
        {
            // 이벤트 중복 구독 방지
            gameManager.PlayerInputController.OnJumpEvent -= OnJumpEvent;
            gameManager.PlayerInputController.OnJumpEvent += OnJumpEvent;
            Debug.Log("TestTileManager Start: OnJumpEvent 이벤트 구독 완료");
        }
        else
        {
            Debug.LogError("TestTileManager Start: GameManager 또는 PlayerInputController가 null입니다");
        }
    }

    void OnJumpEvent(bool isLeft)
    {
        GenerateTile();
    }

    private void GenerateTile()
    {
        // 게임 모드에 따라 다른 타일 생성 로직 적용
        if (gameModeManager != null)
        {
            switch (gameModeManager.CurrentGameMode)
            {
                case GameMode.Infinite:
                    GenerateInfiniteTile();
                    break;
                case GameMode.Story:
                    GenerateStoryTile();
                    break;
                case GameMode.Challenge:
                    GenerateChallengeTile();
                    break;
                default:
                    GenerateInfiniteTile();
                    break;
            }
        }
        else
        {
            // 게임 모드 매니저가 없으면 기존 로직 사용
            GenerateInfiniteTile();
        }
    }

    // 무한 모드 타일 생성 (기존 로직)
    private void GenerateInfiniteTile()
    {
        float randomValue = Random.value;
        GameObject tilePrefab = CheckTilePrefab(randomValue);
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
        AfterCreateTile(tilePrefab, tileComponent);
    }

    // 스토리 모드 타일 생성
    private void GenerateStoryTile()
    {
        // 스토리 레벨에 따른 타일 생성 로직
        int currentLevel = gameModeManager.CurrentStoryLevel;
        
        // 레벨에 따라 다른 확률 적용
        float monsterChance = monsterTileSpawnChance;
        float itemChance = itemTileSpawnChance;
        float invisibleChance = InvisibleTileSpawnChance;
        
        // 레벨별 난이도 조정
        switch (currentLevel)
        {
            case 1: // 튜토리얼
                monsterChance = 0.05f;
                invisibleChance = 0f;
                break;
            case 2: // 쉬운 레벨
                monsterChance = 0.1f;
                invisibleChance = 0.05f;
                break;
            // 추가 레벨...
        }
        
        // 조정된 확률로 타일 생성
        float randomValue = Random.value;
        GameObject tilePrefab;
        
        if (randomValue < monsterChance)
        {
            tilePrefab = MonsterTilePrefab;
        }
        else if (randomValue < monsterChance + itemChance)
        {
            tilePrefab = ItemTilePrefab;
        }
        else if (randomValue < monsterChance + itemChance + invisibleChance)
        {
            tilePrefab = InvisibleTilePrefab;
        }
        else
        {
            tilePrefab = testTilePrefab;
        }
        
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
        AfterCreateTile(tilePrefab, tileComponent);
    }

    // 도전 모드 타일 생성
    private void GenerateChallengeTile()
    {
        switch (gameModeManager.CurrentChallengeType)
        {
            case ChallengeType.Speed:
                GenerateSpeedModeTile();
                break;
            case ChallengeType.OnOff:
                GenerateOnOffModeTile();
                break;
            case ChallengeType.Monster:
                GenerateMonsterModeTile();
                break;
            default:
                GenerateInfiniteTile();
                break;
        }
    }

    // 스피드 모드 타일 생성
    private void GenerateSpeedModeTile()
    {
        // 기본 타일 생성 로직과 유사하지만 더 빠르게 생성
        float randomValue = Random.value;
        GameObject tilePrefab = CheckTilePrefab(randomValue);
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
        AfterCreateTile(tilePrefab, tileComponent);
        
        // 타일 간격을 더 좁게 설정 (선택적)
        // currentY += 0.8f; // 기본값보다 작게 설정
    }

    // 온오프 모드 타일 생성
    private void GenerateOnOffModeTile()
    {
        GameObject tilePrefab;
        float invisibleTileChance = 0.15f; // 온오프 타일 생성 확률 15%
        
        // 15% 확률로 온오프 타일 생성, 나머지는 일반 타일
        if (Random.value < invisibleTileChance)
        {
            tilePrefab = InvisibleTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            CreateInvisibleTile(tileComponent);
        }
        else
        {
            // 나머지 85%는 일반 타일만 생성 (몬스터, 아이템 없음)
            tilePrefab = testTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            // AfterCreateTile 호출하지 않음 (몬스터, 아이템 생성 방지)
        }
    }

    // 몬스터 모드 타일 생성
    private void GenerateMonsterModeTile()
    {
        float monsterChance = 0.20f; // 몬스터 타일 생성 확률 20%
        GameObject tilePrefab;
        
        if (Random.value < monsterChance)
        {
            // 20% 확률로 몬스터 타일 생성
            tilePrefab = MonsterTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            CreateMonsterOnTile(tileComponent);
        }
        else
        {
            // 나머지 80%는 기본 타일만 생성 (아이템 없음)
            tilePrefab = testTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            // AfterCreateTile 호출하지 않음 (아이템 생성 방지)
        }
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

        // 아이템 종류 결정 (50:50 확률)
        GameObject selectedItemPrefab;
        if (Random.value < 0.5f)
        {
            selectedItemPrefab = ItemPrefab;        // 기존 아이템 사용
            Debug.Log("닌자 아이템 생성");
        }
        else
        {
            selectedItemPrefab = shieldItemPrefab;  // 새로운 쉴드 아이템
            Debug.Log("쉴드 아이템 생성");
        }

        GameObject item = Instantiate(selectedItemPrefab, tile.transform);
        item.transform.localPosition = Vector3.zero; // 타일 중앙에 배치
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

    public Tile GetTileByPosition(Vector2 playerPosition)
    {
        int floorIndex = GetFloorByPosition(playerPosition);

        if (floorIndex > 0 && floorIndex < tiles.Count)
        {
            return tiles[floorIndex];
        }

        return null;
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

    private void OnEnable()
    {
        // 씬이 로드될 때마다 호출되는 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 기존 타일 모두 제거
        foreach (var tile in tiles)
        {
            if (tile != null)
            {
                Destroy(tile.gameObject);
            }
        }
        
        // 타일 리스트 초기화
        tiles.Clear();
        currentX = 0;
        currentY = 0;
        direction = 1;
        createTileIndex = 0;
        
        // 첫 번째 타일 생성
        GenerateDefaultTile();
        
        // 나머지 타일 생성
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile();
        }
        
        // 게임 매니저 참조 업데이트
        gameManager = GameManager.Instance;
        if (gameManager != null && gameManager.PlayerInputController != null)
        {
            // 이벤트 중복 구독 방지
            gameManager.PlayerInputController.OnJumpEvent -= OnJumpEvent;
            gameManager.PlayerInputController.OnJumpEvent += OnJumpEvent;
        }
    }

    public void RestartGame()
    {
        Debug.Log("TestTileManager: RestartGame 호출됨");
        
        // 1. 기존 타일 모두 제거
        foreach (var tile in tiles)
        {
            if (tile != null && tile.gameObject != null)
            {
                Destroy(tile.gameObject);
            }
        }
        
        // 또는 더 확실하게 모든 타일 찾아서 제거
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tileObj in allTiles)
        {
            Destroy(tileObj);
        }
        
        // 2. 타일 리스트 초기화
        tiles.Clear();
        currentX = 0;
        currentY = 0;
        direction = 1;
        createTileIndex = 0;
        
        // 3. 첫 번째 타일 생성
        GenerateDefaultTile();
        
        // 4. 나머지 타일 생성
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile();
        }
        
        Debug.Log($"TestTileManager: 타일 초기화 완료 (총 타일 수: {tiles.Count})");
    }
}
