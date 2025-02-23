
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class UIManager : MonoBehaviour
{
    bool isGameOver = false; 


    public GameObject StartPanel;
    public GameObject PlayingPanel;
    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject CautionPanel;
    public GameObject ResultPanel;
    public GameObject StoryPanel;
    public GameObject SettingPanel;


    public Animator playerAnimator;


    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI ResultScoreText;
    public TextMeshProUGUI ResultBestScoreText;
    public TextMeshProUGUI ResultPileUpScoreText;
    public TextMeshProUGUI ResultCoinScoreText;






    public Button GameStartButton;
    public Button SettingButton;
    public Button AttackButton;
    public Button LeftButton;
    public Button RightButton;
    public Button PauseButton;
    public Button PauseMainHomeButton;
    public Button PauseContinueButton;
    public Button BGMButton;
    public Button SoundEffectButton;
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
    public Button SettingInstagrambutton;
    public Button SettingSourceButton;
    public Button SettingSoundEffectButton;




    private int score = 0;
    private int bestScore = 0;


    public PlayerInputController playerInputController;


    private void Awake()
    {
        // UIManager�� PlayerInputController�� ������ �� �ֵ���
        if (GameManager.Instance != null)
        {
            GameManager.Instance.uiManager = this; // GameManager���� �����ϵ��� ����
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
        SettingPanel.SetActive(false);

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
        ResultReStartButton.onClick.AddListener(OnResultReStartButtonClicked);
        ResultMainHomeButton.onClick.AddListener(OnResultMainHomeButtonClicked);
        StoryReTurnButton.onClick.AddListener(OnStoryReTurnButtonClicked);
        StoryButton.onClick.AddListener(OnStoryButtonClicked);
        SettingButton.onClick.AddListener(OnSettingButtonClicked);


    }


    // Update is called once per frame
    void Update()
    {
        PlayerInputController newPlayerInput = FindObjectOfType<PlayerInputController>();

        if (newPlayerInput != null)
        {
            playerInputController = newPlayerInput;
            playerInputController.AssignButtons(); // ��ư �ٽ� ����
            playerInputController.AssignPlayerMovement(); // PlayerMovement �ٽ� ����
        }
    }


    public void StartGame()
    {
        isGameOver = false;

        PlayTurnAround();

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
        SettingPanel.SetActive(false);

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

        // �ִϸ��̼� ���̸�ŭ ���
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

    }


    //��ư Ŭ�� 

    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; // ��ư �ߺ� �Է� ����
        StartCoroutine(PlayTurnAround());
    }

    void OnSettingButtonClicked()
    {
        SettingPanel.SetActive(true);
        StartPanel.SetActive(false);
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

        //������ ������ �� ���� �ջ� ȭ�� ��ȹ�� Ȯ���غ��� 
    }

    void OnCautionContineButtonClicked()
    {
        PlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    //���ӿ��� �׾��� �� ���� ���� �г� 5�� �̻� ��ġ ���� �� ���â���� ȭ�� ��ȯ ??
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

    void OnResultReStartButtonClicked()
    {
        // ����� �ϳ� �ҽ��ϸ鼭 30�� Ÿ�̸� ���۰� ���ÿ� ���� ���� 
        PlayingPanel.SetActive(true);
    }

    void OnResultMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
    }
    

    void OnStoryReTurnButtonClicked()
    {
        ResultPanel.SetActive(true);
    }

   void OnStoryButtonClicked()
    {

    }
    


    //���� �ջ� �̾ ¥�ߵ�
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

    /*public class PanelManager : MonoBehaviour
{
    public Animator playerAnimator;
    public Button startButton;
    public GameObject homePanel; // Ȩ ȭ�� �г�
    public GameObject gamePanel; // ���� ȭ�� �г�

    private void Start()
    {
        startButton.onClick.AddListener(OnGameStartClicked);

        // �ʱ� �г� ���� ���� (Ȩ ȭ��)
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    void OnGameStartClicked()
    {
        startButton.interactable = false; // ��ư �ߺ� �Է� ����
        StartCoroutine(PlayTurnAround());
    }

    IEnumerator PlayTurnAround()
    {
        playerAnimator.SetTrigger("GameStart");

        // �ִϸ��̼� ���̸�ŭ ���
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        yield return new WaitForSeconds(animationDuration);

        // �г� Ȱ��ȭ/ ��Ȱ��ȭ
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
    }
}*/
}
