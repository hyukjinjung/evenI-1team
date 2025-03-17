using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public GameMode CurrentGameMode { get; private set; } = GameMode.Infinite;
    public ChallengeType CurrentChallengeType { get; private set; } = ChallengeType.None;

    // [★추가] 스토리 레벨
    public int CurrentStoryLevel { get; private set; } = 1;

    private IGameMode currentModeHandler;
    private TestTileManager tileManager;
    private UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            tileManager = FindObjectOfType<TestTileManager>();
            uiManager = FindObjectOfType<UIManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameMode(GameMode mode, ChallengeType challenge = ChallengeType.None)
    {
        currentModeHandler?.ExitMode();
        CurrentGameMode = mode;
        CurrentChallengeType = challenge;

        switch (mode)
        {
            case GameMode.Infinite:
                currentModeHandler = new InfiniteMode(tileManager);
                break;
            case GameMode.Story:
                currentModeHandler = new StoryMode(tileManager, uiManager);
                break;
            case GameMode.Challenge:
                // Challenge 모드는 따로 분기
                SetChallengeMode(challenge);
                return;
        }
        currentModeHandler?.InitializeMode();
    }

    private void SetChallengeMode(ChallengeType challengeType)
    {
        switch (challengeType)
        {
            case ChallengeType.Speed:
                currentModeHandler = new SpeedMode(tileManager, uiManager, 60f);
                break;
            case ChallengeType.OnOff:
                currentModeHandler = new OnOffMode(tileManager);
                break;
            case ChallengeType.Monster:
                currentModeHandler = new MonsterMode(tileManager);
                break;
            default:
                Debug.LogError("알 수 없는 도전 모드입니다.");
                currentModeHandler = null;
                return;
        }
        currentModeHandler.InitializeMode();
    }

    // [★추가] UI에서 직접 "SetChallengeType"을 호출할 때를 위한 편의 메서드
    public void SetChallengeType(ChallengeType type)
    {
        SetGameMode(GameMode.Challenge, type);
    }

    private void Update()
    {
        currentModeHandler?.UpdateMode();
    }
}
