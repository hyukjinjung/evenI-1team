using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameMode
{
    Infinite,    // 무한모드
    Story,       // 스토리모드
    Challenge    // 도전모드
}

public enum ChallengeType
{
    None,        // 기본값
    Speed,       // 스피드모드
    OnOff,       // 온오프모드
    Monster      // 몬스터모드
}

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance;
    
    public GameMode CurrentGameMode { get; private set; }
    public ChallengeType CurrentChallengeType { get; private set; }
    
    // 게임 모드별 설정값
    [Header("스피드모드 설정")]
    [SerializeField] private float speedModeMultiplier = 1.5f;
    public float SpeedModeMultiplier => speedModeMultiplier;
    
    [Header("온오프모드 설정")]
    [SerializeField] private float onOffModeSwitchInterval = 2f;
    public float OnOffModeSwitchInterval => onOffModeSwitchInterval;
    
    [Header("몬스터모드 설정")]
    [SerializeField] private float monsterModeSpawnRate = 0.5f;
    public float MonsterModeSpawnRate => monsterModeSpawnRate;
    
    [Header("스토리모드 설정")]
    [SerializeField] private int currentStoryLevel = 1;
    [SerializeField] private int maxStoryLevel = 10;
    public int CurrentStoryLevel => currentStoryLevel;
    
    // 참조
    private TestTileManager tileManager;
    private GameManager gameManager;
    private UIManager uiManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        gameManager = GameManager.Instance;
        tileManager = FindObjectOfType<TestTileManager>();
        uiManager = FindObjectOfType<UIManager>();
        
        // 기본값으로 무한모드 설정
        SetGameMode(GameMode.Infinite);
    }
    
    public void SetGameMode(GameMode mode, ChallengeType challengeType = ChallengeType.None)
    {
        CurrentGameMode = mode;
        CurrentChallengeType = challengeType;
        
        Debug.Log($"게임 모드 변경: {mode}, 도전 타입: {challengeType}");
        
        // 게임 모드에 따른 초기화 작업
        InitializeGameMode();
    }
    
    private void InitializeGameMode()
    {
        // 게임 모드별 초기화 로직
        switch (CurrentGameMode)
        {
            case GameMode.Infinite:
                SetupInfiniteMode();
                break;
            case GameMode.Story:
                SetupStoryMode();
                break;
            case GameMode.Challenge:
                SetupChallengeMode();
                break;
        }
    }
    
    private void SetupInfiniteMode()
    {
        // 무한모드 설정
        Debug.Log("무한모드 설정 완료");
        
        // 기본 타일 생성 확률 설정
        if (tileManager != null)
        {
            // 기본 설정으로 복원
            ResetTileManagerSettings();
        }
    }
    
    private void SetupStoryMode()
    {
        Debug.Log($"스토리모드 설정 완료 - 레벨 {currentStoryLevel}");
        
        // 스토리 레벨에 따른 설정
        switch (currentStoryLevel)
        {
            case 1:
                // 튜토리얼 레벨
                SetupTutorialLevel();
                break;
            case 2:
                // 쉬운 레벨
                SetupEasyLevel();
                break;
            // 추가 레벨 설정...
            default:
                // 기본 설정
                ResetTileManagerSettings();
                break;
        }
    }
    
    private void SetupChallengeMode()
    {
        Debug.Log($"도전모드 설정 완료 - 타입: {CurrentChallengeType}");
        
        switch (CurrentChallengeType)
        {
            case ChallengeType.Speed:
                SetupSpeedMode();
                break;
            case ChallengeType.OnOff:
                SetupOnOffMode();
                break;
            case ChallengeType.Monster:
                SetupMonsterMode();
                break;
            default:
                ResetTileManagerSettings();
                break;
        }
    }
    
    private void SetupTutorialLevel()
    {
        // 튜토리얼 레벨 설정
        if (tileManager != null)
        {
            // 몬스터 없음, 장애물 없음
            // tileManager.monsterTileSpawnChance = 0f;
            // tileManager.obstacleSpawnChance = 0f;
        }
    }
    
    private void SetupEasyLevel()
    {
        // 쉬운 레벨 설정
        if (tileManager != null)
        {
            // 몬스터 적게, 장애물 적게
            // tileManager.monsterTileSpawnChance = 0.1f;
            // tileManager.obstacleSpawnChance = 0.1f;
        }
    }
    
    private void SetupSpeedMode()
    {
        // 스피드모드 설정
        if (tileManager != null)
        {
            // 기본 설정으로 복원 후 속도 증가
            ResetTileManagerSettings();
            
            // 플레이어 이동 속도 증가 로직 추가
            // 타일 생성 속도 증가 로직 추가
        }
        
        // 시간 제한 설정
        StartCoroutine(SpeedModeTimer(60f)); // 60초 제한
    }
    
    private void SetupOnOffMode()
    {
        // 온오프모드 설정
        if (tileManager != null)
        {
            ResetTileManagerSettings();
            
            // 모든 타일에 TogglePlatform 컴포넌트 추가 로직
            // tileManager.InvisibleTileSpawnChance = 1.0f; // 모든 타일이 깜빡이도록
        }
        
        // 타일 깜빡임 간격 설정
        // TogglePlatformManager.Instance.SetGlobalToggleInterval(onOffModeSwitchInterval);
    }
    
    private void SetupMonsterMode()
    {
        // 몬스터모드 설정
        if (tileManager != null)
        {
            ResetTileManagerSettings();
            
            // 몬스터 생성 확률 증가
            // tileManager.monsterTileSpawnChance = monsterModeSpawnRate;
        }
    }
    
    private void ResetTileManagerSettings()
    {
        // 타일 매니저 설정을 기본값으로 복원
        if (tileManager != null)
        {
            // 기본 설정값으로 복원
            // tileManager.monsterTileSpawnChance = 0.18f; // 기본값
            // tileManager.itemTileSpawnChance = 0.15f;    // 기본값
            // tileManager.obstacleSpawnChance = 0.2f;     // 기본값
            // tileManager.InvisibleTileSpawnChance = 0.15f; // 기본값
        }
    }
    
    private IEnumerator SpeedModeTimer(float duration)
    {
        float timer = duration;
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            
            // UI에 남은 시간 표시 - 오류 수정
            if (uiManager != null && uiManager.UIPlayingPanel != null)
            {
                // UpdateTimer 메서드가 없으므로 주석 처리하거나 다른 방식으로 구현
                // uiManager.UIPlayingPanel.UpdateTimer(timer);
                
                // 예: 직접 텍스트 업데이트
                TextMeshProUGUI timerText = uiManager.UIPlayingPanel.GetComponentInChildren<TextMeshProUGUI>();
                if (timerText != null)
                {
                    timerText.text = Mathf.CeilToInt(timer).ToString();
                }
            }
            
            yield return null;
        }
        
        // 시간 종료 시 게임 종료
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
    }
    
    public void AdvanceStoryLevel()
    {
        currentStoryLevel++;
        if (currentStoryLevel > maxStoryLevel)
        {
            currentStoryLevel = 1; // 스토리 완료 후 처음으로
        }
        
        // 스토리 레벨 저장
        SaveStoryProgress();
    }
    
    private void SaveStoryProgress()
    {
        // PlayerPrefs를 사용하여 스토리 진행 저장
        PlayerPrefs.SetInt("StoryLevel", currentStoryLevel);
        PlayerPrefs.Save();
    }
    
    private void LoadStoryProgress()
    {
        // 저장된 스토리 진행 불러오기
        if (PlayerPrefs.HasKey("StoryLevel"))
        {
            currentStoryLevel = PlayerPrefs.GetInt("StoryLevel");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
