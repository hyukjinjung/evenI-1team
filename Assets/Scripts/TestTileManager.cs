using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTileManager : MonoBehaviour
{
    [Header("=== Tile Prefabs ===")]
    [SerializeField] private GameObject testTilePrefab;       // 기본 타일
    [SerializeField] private GameObject ReverseControlPrefab;
    [SerializeField] private GameObject InvisibleTilePrefab;
    [SerializeField] private GameObject StickyPrefab;
    [SerializeField] private GameObject HideNextPrefab;
    [SerializeField] private GameObject MonsterTilePrefab;    // 몬스터 전용 타일
    [SerializeField] private GameObject ItemTilePrefab;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private GameObject shieldItemPrefab;
    [SerializeField] private GameObject coinItemPrefab;

    // [★추가] 온오프 모드에서 사용할 전용 타일 프리팹 (없다면 새로 만들어 Inspector에서 연결)
    [SerializeField] private GameObject OnOffTilePrefab;

    [Header("=== Monster Prefabs (5종) ===")]
    // [★추가] 몬스터 모드에서 사용할 5종류 몬스터 프리팹들
    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private GameObject flyPrefab;
    [SerializeField] private GameObject dragonflyPrefab;
    [SerializeField] private GameObject butterflyPrefab;
    [SerializeField] private GameObject mantisPrefab;

    // ======================================
    private List<Tile> tiles = new List<Tile>();
    private int currentX = 0;
    private int currentY = 0;

    // 기존 확률들
    [Header("=== Tile Spawn Probability ===")]
    [SerializeField] private float obstacleSpawnChance = 0.15f;        // 장애물 스폰 확률
    [SerializeField] private float monsterTileSpawnChance = 0.3f;
    [SerializeField] private float itemTileSpawnChance = 0.15f;
    [SerializeField] private float InvisibleTileSpawnChance = 0.15f;

    [SerializeField] private int startTileCount = 20;
    [SerializeField] private int maxTiles = 20;

    [Header("Item Spawn Rate")]
    [SerializeField] private float coinSpawnChance = 0.8f;
    [SerializeField] private float transformItemChance = 0.2f; // 할당만 되고 사용 안 하면 경고가 날 수 있음

    private PlayerInputController playerInputController;
    private GameManager gameManager;
    private int createTileIndex = 0;

    // 게임 모드 매니저 참조
    private GameModeManager gameModeManager;

    private void Start()
    {
        // [★유지] 기존 로직
        gameModeManager = GameModeManager.Instance;

        GenerateDefaultTile(); // 첫 번째 기본 타일 생성
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile(); // 나머지 타일 생성
        }

        if (startTileCount > maxTiles) maxTiles = startTileCount;

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

    private void GenerateDefaultTile()
    {
        // 첫 번째 타일은 무조건 기본 타일
        GenerateTile(testTilePrefab, out Tile tile);
    }

    private void OnJumpEvent(bool isLeft)
    {
        // 플레이어가 점프할 때마다 새 타일 생성 (기존 로직)
        GenerateTile();
    }

    /// <summary>
    /// 타일 1개 생성(어떤 모드인지에 따라 분기)
    /// </summary>
    private void GenerateTile()
    {
        // 게임 모드 확인
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
                    // 혹시 모드가 정의되지 않았다면 무한 모드로 처리
                    GenerateInfiniteTile();
                    break;
            }
        }
        else
        {
            // 게임모드매니저가 없으면 기존 무한 모드 로직 사용
            GenerateInfiniteTile();
        }
    }

    public Tile GetForwardTile(Vector3 playerPosition)
    {
        Tile forwardTile = null;
        float minDistance = Mathf.Infinity;

        // tiles는 TestTileManager가 관리하는 타일 리스트라고 가정
        // 예: private List<Tile> tiles = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            // 플레이어보다 위에 있는 타일만 대상으로
            if (tile.transform.position.y > playerPosition.y)
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
    // ============ 무한 모드 ============
    private void GenerateInfiniteTile()
    {
        float randomValue = Random.value;
        GameObject tilePrefab = CheckTilePrefab(randomValue);
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
        AfterCreateTile(tilePrefab, tileComponent);
    }

    // ============ 스토리 모드 ============
    private void GenerateStoryTile()
    {
        int currentLevel = gameModeManager.CurrentStoryLevel;

        // 레벨별로 확률 가중치 조정
        float monsterChance = monsterTileSpawnChance;
        float itemChance = itemTileSpawnChance;
        float invisibleChance = InvisibleTileSpawnChance;

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
                // 필요한 만큼 case 추가
        }

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

    // ============ 도전(Challenge) 모드 ============
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

    // ============ 스피드 모드 ============
    private void GenerateSpeedModeTile()
    {
        // 기존 무한 모드 로직 + 추가
        float randomValue = Random.value;
        GameObject tilePrefab = CheckTilePrefab(randomValue);
        GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
        AfterCreateTile(tilePrefab, tileComponent);

        // 필요 시 간격/속도 조정
        // currentY += 0.8f; 
    }

    // ============ 온오프 모드 (20% 확률) ============
    private void GenerateOnOffModeTile()
    {
        // [★수정] 20% 확률로 OnOffTile, 80% 확률로 일반 타일
        float onOffChance = 0.2f;
        GameObject tilePrefab;

        if (Random.value < onOffChance)
        {
            // OnOff Tile
            tilePrefab = OnOffTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            // 온오프 타일 전용 로직(필요 시)
            // ex) CreateOnOffTile(tileComponent);
        }
        else
        {
            // 일반 타일
            tilePrefab = testTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            // AfterCreateTile 호출 없이 기본 타일만 배치할 수도 있지만
            // 필요하다면 AfterCreateTile(tilePrefab, tileComponent); 호출 가능
        }
    }

    // ============ 몬스터 모드 (20% 확률로 몬스터 타일 + 5종 랜덤) ============
    private void GenerateMonsterModeTile()
    {
        // [★수정] 20% 몬스터 타일, 80% 일반 타일
        float monsterChance = 0.2f;
        GameObject tilePrefab;

        if (Random.value < monsterChance)
        {
            // 몬스터 타일
            tilePrefab = MonsterTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            // 5종류 중 랜덤 배치
            CreateMonsterOnTile(tileComponent);
        }
        else
        {
            // 일반 타일
            tilePrefab = testTilePrefab;
            GameObject tileObject = GenerateTile(tilePrefab, out Tile tileComponent);
            // 아이템/몬스터 없음
        }
    }

    /// <summary>
    /// 무작위(기존 확률 기반) 타일 Prefab 선택
    /// </summary>
    private GameObject CheckTilePrefab(float randomValue)
    {
        // [★유지] 무한 모드 등에서 쓰는 기존 랜덤 로직
        GameObject tilePrefab;
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

    /// <summary>
    /// 타일 오브젝트 생성 + Tile 컴포넌트 반환
    /// </summary>
    private GameObject GenerateTile(GameObject tilePrefab, out Tile tileComponent)
    {
        GameObject tileObject = Instantiate(tilePrefab, transform);
        tileComponent = tileObject.GetComponent<Tile>();

        if (tileComponent == null)
        {
            tileComponent = tileObject.AddComponent<Tile>(); // 없으면 추가
        }

        tileObject.transform.localPosition = new Vector3(currentX, currentY, 0);
        tileObject.SetActive(true);

        tileComponent.Init(createTileIndex);
        createTileIndex++;

        tiles.Add(tileComponent);
        UpdateTilePosition();
        return tileObject;
    }

    /// <summary>
    /// 타일 생성 후, 몬스터/아이템/인비저블 등 부가 처리를 담당
    /// </summary>
    private void AfterCreateTile(GameObject tilePrefab, Tile tileComponent)
    {
        if (tilePrefab == MonsterTilePrefab)
        {
            CreateMonsterOnTile(tileComponent); // 몬스터 타일
        }
        else if (tilePrefab == ItemTilePrefab)
        {
            CreateItemOnTile(tileComponent);    // 아이템 타일
        }
        else if (tilePrefab == InvisibleTilePrefab)
        {
            CreateInvisibleTile(tileComponent); // 인비저블 타일
        }

        // 일반 타일이면 장애물 생성 (확률)
        if (tilePrefab == testTilePrefab && Random.value < obstacleSpawnChance)
        {
            CreateObstacleOnTile(tileComponent);
        }
    }

    // ============ 몬스터 생성(5종류 랜덤) ============
    private void CreateMonsterOnTile(Tile tile)
    {
        if (tile == null) return;

        // [★수정] 5종 몬스터 중 하나를 랜덤으로 선택
        float r = Random.value;
        GameObject chosenPrefab = null;

        if (r < 0.2f) chosenPrefab = spiderPrefab;
        else if (r < 0.4f) chosenPrefab = flyPrefab;
        else if (r < 0.6f) chosenPrefab = dragonflyPrefab;
        else if (r < 0.8f) chosenPrefab = butterflyPrefab;
        else chosenPrefab = mantisPrefab;

        if (chosenPrefab == null)
        {
            Debug.LogWarning("몬스터 프리팹이 설정되지 않았습니다!");
            return;
        }

        GameObject monster = Instantiate(chosenPrefab, tile.transform);
        monster.transform.localPosition = Vector3.zero; // 타일 중앙
        monster.SetActive(true);

        // 몬스터 컴포넌트 연결
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
        // 필요 시 togglePlatform.SetToggleInterval(3f); 등
    }

    private void CreateItemOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject selectedItemPrefab;

        if (Random.value < coinSpawnChance)
        {
            selectedItemPrefab = coinItemPrefab;
            Debug.Log("코인 아이템 생성");
        }
        else
        {
            selectedItemPrefab = ItemPrefab;
        }

        GameObject item = Instantiate(selectedItemPrefab, tile.transform);
        item.transform.localPosition = Vector3.zero;
        item.SetActive(true);

        tile.SetItem(item);
    }

    private void CreateObstacleOnTile(Tile tile)
    {
        if (tile == null) return;

        GameObject obstaclePrefab = null;
        int obstacleType = Random.Range(0, 4);

        switch (obstacleType)
        {
            // 필요한 경우 추가
            // case 0:
            //    obstaclePrefab = ReverseControlPrefab;
            //    break;
            // case 1:
            //    obstaclePrefab = StickyPrefab;
            //    break;
            case 2:
                obstaclePrefab = HideNextPrefab;
                break;
        }

        if (obstaclePrefab != null)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, tile.transform);
            obstacle.transform.localPosition = Vector3.zero;
            obstacle.SetActive(true);
            tile.SetObstacle(obstacle);
        }
    }

    // ============ 타일 위치 업데이트(랜덤 좌우 이동) ============
    private void UpdateTilePosition()
    {
        int randomDirection = (Random.Range(0, 2) == 0) ? -1 : 1; // -1 or +1

        float newX = currentX + randomDirection;

        // x좌표 제한
        if (newX < -8f)
        {
            newX = -7f;
        }
        else if (newX > 8f)
        {
            newX = 7f;
        }

        currentX = Mathf.RoundToInt(newX);
        currentY += 1; // 계속 위로 올라감
    }

    // ============ 기타 유틸 ============

    public Tile GetNextMonsterTile(int currentFloor)
    {
        for (int i = currentFloor; i < tiles.Count; i++)
        {
            if (tiles[i].MonsterOnTile != null)
                return tiles[i];
        }
        return null;
    }

    public Tile GetNextTile(int currentFloor)
    {
        int nextFloor = currentFloor + 1;
        if (nextFloor < 0 || currentFloor >= tiles.Count)
        {
            return null;
        }
        return tiles[nextFloor];
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

    // 닌자 효과 등으로 타일 건너뛰기 시
    public void GenerateTilesAfterNinjaEffect(int skippedTiles)
    {
        for (int i = 0; i < skippedTiles; i++)
        {
            GenerateTile();
        }
        Debug.Log($"닌자 효과: {skippedTiles}개의 새 타일이 생성되었습니다.");
    }

    private void OnEnable()
    {
        // 씬이 로드될 때마다 호출
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 기존 타일 정리
        foreach (var tile in tiles)
        {
            if (tile != null)
            {
                Destroy(tile.gameObject);
            }
        }
        tiles.Clear();
        currentX = 0;
        currentY = 0;
        createTileIndex = 0;

        // 첫 번째 타일
        GenerateDefaultTile();
        // 나머지 타일
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile();
        }

        // 다시 이벤트 구독
        gameManager = GameManager.Instance;
        if (gameManager != null && gameManager.PlayerInputController != null)
        {
            gameManager.PlayerInputController.OnJumpEvent -= OnJumpEvent;
            gameManager.PlayerInputController.OnJumpEvent += OnJumpEvent;
        }
    }

    // [★추가] SpeedMode에서 호출하기 위해 만든 예시 메서드
    public void AdjustSpeed(float speedFactor)
    {
        // 예) spawnInterval *= speedFactor;
        // obstacleSpawnChance *= speedFactor; 
        Debug.Log($"AdjustSpeed 호출됨: {speedFactor}");
    }

    public void ResetSettingsToDefault()
    {
        Debug.Log("ResetSettingsToDefault 호출됨");
        // spawnInterval = 기본값;
        // obstacleSpawnChance = 기본값;
        // ...
    }

    public void RestartGame()
    {
        Debug.Log("TestTileManager: RestartGame 호출됨");

        // 1. 기존 타일 제거
        foreach (var tile in tiles)
        {
            if (tile != null && tile.gameObject != null)
            {
                Destroy(tile.gameObject);
            }
        }

        // 모든 'Tile' 태그 오브젝트도 제거
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tileObj in allTiles)
        {
            Destroy(tileObj);
        }

        // 2. 리스트 초기화
        tiles.Clear();
        currentX = 0;
        currentY = 0;
        createTileIndex = 0;

        // 3. 첫 번째 타일
        GenerateDefaultTile();

        // 4. 나머지 타일
        for (int i = 0; i < startTileCount - 1; i++)
        {
            GenerateTile();
        }

        Debug.Log($"TestTileManager: 타일 초기화 완료 (총 타일 수: {tiles.Count})");
    }
    

    // OnOffMode에서 호출하는 메서드
    public void SetOnOffModeActive(bool active, float onOffTileProb = 0.2f, float normalTileProb = 0.8f)
    {
        // TODO: onOffTileProb(20%), normalTileProb(80%) 등을 저장해두고,
        // GenerateTile()에서 이 값을 참조하도록 만들 수도 있음.

        if (active)
        {
            Debug.Log($"[TestTileManager] OnOffMode 활성화: onOff={onOffTileProb * 100}%, normal={normalTileProb * 100}%");
        }
        else
        {
            Debug.Log("[TestTileManager] OnOffMode 비활성화");
        }
    }

    // MonsterMode에서 호출하는 메서드
    public void SetMonsterModeActive(bool active, float monsterTileProb = 0.2f, float normalTileProb = 0.8f)
    {
        // TODO: monsterTileProb(20%), normalTileProb(80%) 등을 저장 후,
        // GenerateTile()에서 이 값을 참조하도록 구현할 수 있음.

        if (active)
        {
            Debug.Log($"[TestTileManager] MonsterMode 활성화: monster={monsterTileProb * 100}%, normal={normalTileProb * 100}%");
        }
        else
        {
            Debug.Log("[TestTileManager] MonsterMode 비활성화");
        }
    }

}
