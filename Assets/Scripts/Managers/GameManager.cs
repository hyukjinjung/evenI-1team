using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private bool isGodMode = false;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                Debug.Log("GameManager NULL");
            }

            return _instance;
        }
    }

    public bool IsGameStarted { get; private set; } = false;
    public float gameStartTime { get; private set; }


    public UIManager uiManager;

    UIPlayingPanel uiPlayingPanel;
    UIResultPanel uiResultPanel;
    UIGameOverPanel uiGameOverPanel;

    public TestTileManager tileManager;
    public FeverSystem feverSystem;
    public FeverBackGroundManager feverBackGroundManager;
    public ChasingMonsterManager chasingMonsterManager;
    public CameraController cameraController;
    public UIChasingMonsterGauge uiChasingMonsterGauge;

    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;

    // -------------------------- player
    public GameObject player;
    private PlayerMovement playerMovement;
    private PlayerInputController playerInputController;
    private PlayerAttackController playerAttackController;
    private PlayerAnimationController playerAnimationController;
    private PlayerTransformationController playerTransformationController;
    
    public PlayerMovement PlayerMovement{get{return playerMovement;}}
    public PlayerAttackController PlayerAttackController { get { return playerAttackController; } }
    public PlayerInputController PlayerInputController{get{return playerInputController;}}
    public PlayerAnimationController PlayerAnimationController{get{return playerAnimationController;}}
    public PlayerTransformationController PlayerTransformationController{get{return playerTransformationController;}}

    private int score = 0;
    private int bestScore = 0;
    private int resultScore = 0;
    private int resultBestScore = 0;

    
    public int Score {get{return score;}}
    public int BestScore {get{return bestScore;}}
    public int ResultScore {get{return resultScore;}}
    public int ResultBestScore {get{return resultBestScore;}}

    [SerializeField] private int totalCoins = 0;
    public int TotalCoins => totalCoins;



    private bool isGameOver = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        uiManager = FindObjectOfType<UIManager>();
        feverSystem = FindAnyObjectByType<FeverSystem>();
        feverBackGroundManager = FindObjectOfType<FeverBackGroundManager>();
        chasingMonsterManager = FindObjectOfType<ChasingMonsterManager>();
        cameraController = FindObjectOfType<CameraController>();

        uiChasingMonsterGauge = FindObjectOfType<UIChasingMonsterGauge>(true);


        feverSystem.OnFeverStart += HandleFeverStart;
        feverSystem.OnFeverEnd += HandleFeverEnd;
        


        if (player == null)
        {
            Debug.Log("unassigned player");
            return;
        }

        playerMovement = player.GetComponent<PlayerMovement>();
        playerAttackController = player.GetComponent<PlayerAttackController>();
        playerInputController = player.GetComponent<PlayerInputController>();
        playerAnimationController = player.GetComponent<PlayerAnimationController>();
        playerTransformationController = player.GetComponent<PlayerTransformationController>();
    }


    private void Start()
    {
        uiGameOverPanel = uiManager.UIGameOverPanel;
        uiPlayingPanel = uiManager.UIPlayingPanel;
        uiResultPanel = uiManager.UIResultPanel;

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        uiResultPanel.UpdateBestScore(bestScore);


        if (ChasingMonsterManager.Instance != null)
        {
            ChasingMonsterManager.Instance.Initialize(player.transform, cameraController,
                uiChasingMonsterGauge);
            //SoundManager.Instance.PlayClip(5);
        }

        if (cameraController != null) 
        {
            cameraController.StartCameraSequnece();
        }
    }


    public void StartGame()
    {
        Debug.Log("게임 시작");
        IsGameStarted = true;
        gameStartTime = Time.time;

        playerMovement.enabled = true;
        playerInputController.enabled = true;

        Time.timeScale = 1f;

        isGameOver = false;


        AddScore(0);
        UpdateBestScore(0);

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);



        //uiResultPanel.UpdateBestScore(0);

        //uiResultPanel.UpdateBestScore(bestScore);
    }


    public void GameOver()
    {
        Debug.Log("게임 오버 발생");
        if (isGameOver) return;

        // 화면 가림 효과 비활성화
        if (DarkOverlayController.Instance != null)
        {
            DarkOverlayController.Instance.DeactivateDarkness();
        }

        //if (isGodMode) return;

        if (FeverSystem.Instance != null && FeverSystem.Instance.IsFeverActive)
        {
            FeverSystem.Instance.EndFever();
        }

        if (FeverSystem.Instance != null)
        {
            FeverSystem.Instance.FeverScore = 0;
            Debug.Log($"피버 점수 초기화. {feverSystem.FeverScore}");
        }

        GameOverPanel.SetActive(true);

        isGameOver = true;


        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttackController>().enabled = false;

            PlayerAnimationController.PlayGameOverAnimation();
            player.GetComponentInChildren<Animator>().Play("Die");
        }
   
        StartCoroutine(FreezeGameAfterDelay());

        UpdateBestScore(score);

        SoundManager.Instance.PlayClip(4);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0f;
    }


    public void AddCoins(int amount)
    {
        totalCoins += amount;

        Debug.Log($"코인 획득 {totalCoins}");

        // UI

        SaveCoins();
    }


    private void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }


    private void LoadCoins()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
    }


    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        
        uiPlayingPanel.UpdateScore(score);
        uiGameOverPanel.UpdateScore(score);
        uiResultPanel.UpdateScore(score);

        Debug.Log(Score);
    }



    public void UpdateBestScore(int value)
    {
        if (bestScore < value)
        {
            bestScore = value;

            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();

            uiResultPanel.UpdateBestScore(bestScore);

            SoundManager.Instance.PlayClip(23);
        }
    }



    public void HandleFeverStart()
    {
        Debug.Log("피버 모드 시작");
    }



    public void HandleFeverEnd()
    {
        Debug.Log("피버 모드 종료");
    }
}

