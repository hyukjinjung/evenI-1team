using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIModePanel : MonoBehaviour
{
    [Header("��� ��ư")]
    [SerializeField] private Button infiniteModeButton;
    [SerializeField] private Button storyModeButton;
    [SerializeField] private Button challengeModeButton;

    [Header("���� ��� ��ư")]
    [SerializeField] private GameObject challengeSubPanel;
    [SerializeField] private Button speedModeButton;
    [SerializeField] private Button onOffModeButton;
    [SerializeField] private Button monsterModeButton;

    [Header("��Ÿ ��ư")]
    [SerializeField] private Button backButton;

    private UIManager uiManager;
    private GameModeManager gameModeManager;
    private bool isInitialized = false;

    private void Start()
    {
        if (!isInitialized)
        {
            Initialize(FindObjectOfType<UIManager>());
        }
    }

    public void Initialize(UIManager uiManager)
    {
        if (isInitialized) return;
        
        this.uiManager = uiManager;
        
        // 버튼 이벤트 등록
        if (storyModeButton != null)
        {
            storyModeButton.onClick.AddListener(OnStoryModeSelected);
        }
        else
        {
            Debug.LogError("storyModeButton이 할당되지 않았습니다!");
        }
        
        if (infiniteModeButton != null)
        {
            infiniteModeButton.onClick.AddListener(OnInfiniteModeSelected);
        }
        else
        {
            Debug.LogError("infiniteModeButton이 할당되지 않았습니다!");
        }
        
        if (challengeModeButton != null)
        {
            challengeModeButton.onClick.AddListener(OnChallengeModeSelected);
        }
        else
        {
            Debug.LogError("challengeModeButton이 할당되지 않았습니다!");
        }

        speedModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.Speed));
        onOffModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.OnOff));
        monsterModeButton.onClick.AddListener(() => OnChallengeModeTypeSelected(ChallengeType.Monster));

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
        else
        {
            Debug.LogError("backButton이 할당되지 않았습니다!");
        }

        // ʱ 
        challengeSubPanel.SetActive(false);

        // GameModeManager 참조 가져오기
        EnsureGameModeManager();
        
        isInitialized = true;
        Debug.Log("UIModePanel 초기화 완료");
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnInfiniteModeSelected()
    {
        try
        {
            // GameModeManager 확인
            EnsureGameModeManager();
            
            // 게임 모드 설정
            gameModeManager.SetGameMode(GameMode.Infinite);
            Debug.Log("무한 모드 선택됨");
            
            // 게임 시작
            StartGame();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"무한 모드 선택 중 오류 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    private void OnStoryModeSelected()
    {
        try
        {
            // GameModeManager 확인
            EnsureGameModeManager();
            
            // 게임 모드 설정
            gameModeManager.SetGameMode(GameMode.Story);
            Debug.Log("스토리 모드 선택됨");
            
            // 게임 시작
            StartGame();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"스토리 모드 선택 중 오류 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    private void OnChallengeModeSelected()
    {
        try
        {
            // GameModeManager 확인
            EnsureGameModeManager();
            
            // 게임 모드 설정
            gameModeManager.SetGameMode(GameMode.Challenge);
            Debug.Log("챌린지 모드 선택됨");
            
            // 게임 시작
            StartGame();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"챌린지 모드 선택 중 오류 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    private void OnChallengeModeTypeSelected(ChallengeType type)
    {
        gameModeManager.SetGameMode(GameMode.Challenge, type);
        uiManager.StartGame();
    }

    private void OnBackButtonClicked()
    {
        try
        {
            // UIManager 참조 가져오기
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogError("UIManager를 찾을 수 없습니다!");
                return;
            }
            
            // 모드 선택 패널 비활성화
            gameObject.SetActive(false);
            
            // 시작 패널 활성화
            if (uiManager.UIStartPanel != null)
            {
                uiManager.UIStartPanel.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("UIStartPanel이 null입니다!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"뒤로 가기 버튼 클릭 중 오류 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    private void StartGame()
    {
        try
        {
            // GameManager 참조 가져오기
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager를 찾을 수 없습니다!");
                return;
            }
            
            // UIManager 참조 가져오기
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogError("UIManager를 찾을 수 없습니다!");
                return;
            }
            
            // 모드 선택 패널 비활성화
            gameObject.SetActive(false);
            
            // 게임 시작
            gameManager.StartGame();
            Debug.Log("게임 시작됨");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"게임 시작 중 오류 발생: {e.Message}\n{e.StackTrace}");
        }
    }

    // GameModeManager가 존재하는지 확인하고, 없으면 생성
    private void EnsureGameModeManager()
    {
        // 먼저 씬에서 찾기
        gameModeManager = FindObjectOfType<GameModeManager>();
        
        // 찾지 못했다면 GameObject 이름으로 찾기
        if (gameModeManager == null)
        {
            GameObject gameModeObj = GameObject.Find("GameModeManager");
            if (gameModeObj != null)
            {
                gameModeManager = gameModeObj.GetComponent<GameModeManager>();
            }
        }
        
        // 여전히 찾지 못했다면 새로 생성
        if (gameModeManager == null)
        {
            Debug.LogWarning("GameModeManager를 찾을 수 없습니다. 새로 생성합니다.");
            
            // GameModeManager 생성
            GameObject gameModeManagerObj = new GameObject("GameModeManager");
            gameModeManager = gameModeManagerObj.AddComponent<GameModeManager>();
            
            // 씬 전환 시에도 유지되도록 설정
            DontDestroyOnLoad(gameModeManagerObj);
            
            Debug.Log("GameModeManager 생성 완료");
        }
    }
}