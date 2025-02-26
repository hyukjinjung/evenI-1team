
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



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
    public GameObject CreditsPanel;


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
    public Button PauseBGMButton;
    public Button PauseSoundEffectButton;
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
    public Button SettingBGMOnButton;
    public Button SettingBGmOffButton;
    public Button CreditsReTurnButton;

    private bool isActive = true;

    public Image soundImage;
    private bool isSoundActive = true;

    private int score = 0;
    private int bestScore = 0;


    public PlayerInputController playerInputController;
    public SoundManager soundManager;


    // Start is called before the first frame update
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
        CreditsPanel.SetActive(false);

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
        CreditsReTurnButton.onClick.AddListener(OnSourceReTurnButtonClicked);

        playerAnimationController = FindObjectOfType<PlayerAnimationController>();

        soundManager = GetComponent<SoundManager>();
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void StartGame()
    {


        isGameOver = false;

        // ���� ���۰� ���ÿ� �÷��̾� TurnAround �ִϸ��̼� ����
        playerAnimationController.PlayGameStartAnimation();
        public PlayerAnimationController playerAnimationController;

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

        playerAnimationController.PlayGameStartAnimation();

       // UpdateBestScore(score);
    }


    public void RestartGame()// 
    {
        //SceneManager.LoadScene(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }




    

    void OnGameStartButtonClicked()
    {
        GameStartButton.interactable = false; // ��ư �ߺ� �Է� ����
        StartGame();
    }

    void OnSettingButtonClicked()
    {
        SettingPanel.SetActive(true);
        StartPanel.SetActive(false);
    }

    public void OnSettingBGMButtonClicked()
    {
        soundManager.SettingBGMButtonSoundState();
    }

    public void OnSettingSoundEffectButtonClicked()
    {
        soundManager.SettingSoundEffectButtonSoundState();
    }

  

    public void OnSourceReTurnButtonClicked()
    {
        SettingPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    void OnPauseButtonClicked()
    {
        PausePanel.SetActive(true);

        Time.timeScale = 0f; //Gamepause
    }

    void OnPauseMainHomeButtonClicked()
    {
        CautionPanel.SetActive(true);

        //SoundManager(); ���� �Ŵ��� ȣ�� 
    }

    void OnPauseContinueButtonClicked()
    {
        
        PlayingPanel.SetActive(true);
        PausePanel.SetActive(false);
        
        Time.timeScale = 1f;
    }


    public void OnPauseBGMButtonClicked()
    {
        soundManager.PauseBGMButtonSoundState();
    }

    public void OnPauseSoundEffectButtonClicked()
    {
        soundManager.PauseBGMButtonSoundState();
    }
   

    void OnCautionMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);

    }

    void OnCautionContineButtonClicked()
    {
        PlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    
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

    public void OnResultReStartButtonClicked()
    {
        PlayingPanel.SetActive(false);
        RestartGame();
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
        StoryPanel.SetActive(true);
    }



    
    




    /*public void AddScore(int vlaue)
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
    }*/

}
