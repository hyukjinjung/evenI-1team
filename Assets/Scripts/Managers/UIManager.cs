
using System.Collections;
using System.Collections.Generic;
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
    public GameObject SourcePanel;


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
    public Button SettingBGMButton;
    public Button CreditsReTurnButton;

 
    private bool isActive = true; 


    private int score = 0;
    private int bestScore = 0;

    

    public PlayerInputController playerInputController;
    public PlayerAnimationController playerAnimationController;
   


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
        SourcePanel.SetActive(false);

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
        
    }


    // Update is called once per frame
    void Update()
    {

    }
   
    public void StartGame()
    {


        isGameOver = false;

        
        playerAnimationController.PlayGameStartAnimation();

        StartPanel.SetActive(false);
        PlayingPanel.SetActive(true);
        SettingPanel.SetActive(false);
        GameOverPanel.SetActive(false);

    }

    public void GameOver()
    {
        isGameOver = true;

        GameOverPanel.SetActive(true);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);
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
        GameStartButton.interactable = false; 
        StartGame();
    }

    public void OnSettingButtonClicked()
    {
        SettingPanel.SetActive(true);
        StartPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void OnSourceReTurnButtonClicked()
    {
        SettingPanel.SetActive(true);
        SourcePanel.SetActive(false);
    }

     public void OnPauseButtonClicked()
    {
        PausePanel.SetActive(true);

        Time.timeScale = 0f; //Gamepause
    }

    public void OnPauseMainHomeButtonClicked()
    {
        CautionPanel.SetActive(true);
    }

    public void OnPauseContinueButtonClicked()
    {
        // SceneManager.LoadScene("");

        PlayingPanel.SetActive(true);
        PausePanel.SetActive(false);
        // SceneManager.LoadScene(SceneManager.GetActiveScene())
        Time.timeScale = 1f;

    }

    public void OnCautionMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
        PlayingPanel.SetActive(false);
        PausePanel.SetActive(false);
        CautionPanel.SetActive(false);

       
    }

    public void OnCautionContineButtonClicked()
    {
        PlayingPanel.SetActive(false);
        CautionPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    
    public void OnAdsComeBackButtonClicked()
    {
        PlayingPanel.SetActive(true);

    }
    

    public void OnSkipButtonClicked()
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
        PlayingPanel.SetActive(true);
        ResultPanel.SetActive(false); 
       
    }

    public void OnResultMainHomeButtonClicked()
    {
        StartPanel.SetActive(true);
        ResultPanel.SetActive(false);

    }
    

    public void OnStoryReTurnButtonClicked()
    {
        ResultPanel.SetActive(true);
    }

   public void OnStoryButtonClicked()
    {

    }


   
    
       
        


}
