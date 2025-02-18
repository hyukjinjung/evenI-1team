
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class UIManager : MonoBehaviour
{

    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject CautionPanel;
    public GameObject ResultPanel;
    public GameObject StoryPanel;


    public Animator playerAnimator;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI ResultScoreText;
    public TextMeshProUGUI ResultBestScoreText;
    public TextMeshProUGUI ResultPileUpScoreText;
    public TextMeshProUGUI ResultCoinScoreText;



    bool isGameOver = false;


    public Button GameStartButton;
    public Button AttackButton;
    public Button LeftButton;
    public Button RightButton;
    public Button PauseButton;
    public Button PauseMainHomeButton;
    public Button PauseContinueButton;
    public Button CautionMainHomeButton;
    public Button CautionContinueButton;
    public Button AdsComeBackButton;
    public Button SkipButton;
    public Button ResultScoreTwiceButton;
    public Button ResultReStartButton;
    public Button ResultMainHomeButton;
    public Button ResultCoinTwiceButton;
    public Button StoryReTurnButton;
    public Button StoryButton;




    private int score = 0;
    private int bestScore = 0;


    public PlayerInputController playerInputController;


    private void Awake()
    {
        // UIManager가 PlayerInputController를 저장할 수 있도록
        if (GameManager.Instance != null)
        {
            GameManager.Instance.uiManager = this; // GameManager에서 관리하도록 설정
        }
        else
        {
            Debug.Log("[UIManager] GameManager Instance NULL");
        }
    }


    void Start()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);
        ResultPanel.SetActive(false);
        StoryPanel.SetActive(false);

        GameStartButton.onClick.AddListener(OnGameStartButtonClicked);
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        PauseMainHomeButton.onClick.AddListener(OnPauseMainHomeButtonClicked);
        PauseContinueButton.onClick.AddListener(OnPauseContinueButtonClicked);
        CautionMainHomeButton.onClick.AddListener(OnCautionMainHomeButtonClicked);
        CautionContinueButton.onClick.AddListener(OnCautionContineButtonClicked);
        AdsComeBackButton.onClick.AddListener(OnAdsComeBackButtonClicked);
        SkipButton.onClick.AddListener(OnSkipButtonClicked);
        ResultCoinTwiceButton.onClick.AddListener(OnResultCoinTwiceButtonClicked);
        ResultScoreTwiceButton.onClick.AddListener(OnResultScoreTwiceButtonClicked);
        ResultReStartButton.onClick.AddListener(ResultReStartButtonClicked);
        ResultMainHomeButton.onClick.AddListener(ResultMainHomeButtonClicked);

    }




    


    public void UpdatePlayerInputController()
    {
        PlayerInputController newPlayerInput = FindObjectOfType<PlayerInputController>();

        if (newPlayerInput != null)
        {
            playerInputController = newPlayerInput;
            playerInputController.AssignButtons(); // 버튼 다시 연결
            playerInputController.AssignPlayerMovement(); // PlayerMovement 다시 연결
        }
    }


    public void StartGame()
    {
        isGameOver = false;

        PlayTurnAround();

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);


    }

    public void GameOver()
    {
        isGameOver = true;


        GameOverPanel.SetActive(true);

        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);


        UpdateBestScore(score);
    }


    public void RestartGame()// 
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // 애니메이션 길이만큼 대기
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

    }


    /*public class PanelManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Button startButton;
    public GameObject homePanel; // 홈 화면 패널
    public GameObject gamePanel; // 게임 화면 패널

    private void Start()
    {
        startButton.onClick.AddListener(OnGameStartClicked);

        // 초기 패널 상태 설정 (홈 화면)
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void OnGameStartClicked()
    {
        startButton.interactable = false; // 버튼 중복 입력 방지
        StartCoroutine(PlayTurnAround());
    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // 애니메이션 길이만큼 대기
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

        // 패널 활성화/ 비활성화
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}*/








    //버튼 클릭 

    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; // 버튼 중복 입력 방지
        StartCoroutine(PlayTurnAround());
    }



    void OnPauseButtonClicked()
    {
        PausePanel.SetActive(true);

        Time.timeScale = 0f; //Gamepause
    }

    void OnPauseMainHomeButtonClicked()
    {
        // SceneManager.LoadScene("CautionPanel");

        // PausePanel.SetActive(true);
        CautionPanel.SetActive(true);
    }

    void OnPauseContinueButtonClicked()
    {
        // SceneManager.LoadScene("");

        PlayingPanel.SetActive(true);
        PausePanel.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene())
        Time.timeScale = 1f;

    }

    void OnCautionMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);

        //게임을 나갔을 때 점수 합산 화면 기획서 확인해보기 
    }

    void OnCautionContineButtonClicked()
    {
        PlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    //게임에서 죽었을 때 게임 오버 패널 5초 이상 터치 없을 시 결과창으로 화면 전환 ??
    void OnAdsComeBackButtonClicked()
    {
        PlayingPanel.SetActive(true);

    }


    void OnSkipButtonClicked()
    {
        ResultPanel.SetActive(true);
        PlayingPanel.SetActive(false);
    }

    void OnResultCoinTwiceButtonClicked()
    {

    }

    void OnResultScoreTwiceButtonClicked()
    {

    }

    void ResultReStartButtonClicked()
    {
        // 입장권 하나 소실하면서 30분 타이머 시작과 동시에 게임 시작 
        PlayingPanel.SetActive(true);
    }

    void ResultMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
    }




    //점수 합산 이어서 짜야됨
    public void AddScore(int vlaue)
    {
        if (isGameOver) return;

        score += vlaue;
    }

    private void UpdateBestScore(int value)
    {
        if (bestScore < value)
        {
            bestScore = value;
        }
    }
}
